using AutoMapper;
using AutoMapper.QueryableExtensions;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using UserService.Api.Interfaces;
using UserService.Api.Models;

namespace UserService.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(IMapper mapper, ApplicationDbContext context, ILogger<UserRepository> logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }
        public async Task<UserResponse> CreateUser(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            var addedUser = await _context.Users.SingleOrDefaultAsync(u => u.UserId == user.UserId);
            return _mapper.Map<UserResponse>(addedUser);
        }

        public async Task<UserResponse> UpdateUser(UserUpdateRequest user)
        {
            var userInDb = await _context.Users.SingleOrDefaultAsync(u => u.UserId == user.UserId);
            if (userInDb != null) 
            {
                _mapper.Map(user, userInDb);
                await _context.SaveChangesAsync();
                var updatedUserInDb = await _context.Users.SingleOrDefaultAsync(u => u.UserId == user.UserId);
                return _mapper.Map<UserResponse>(updatedUserInDb);
            }
            return _mapper.Map<UserResponse>(userInDb); ;
        }

        public async Task<User> DeleteUser(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserId == id);
            if (user != null) 
            { 
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return user!;
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            var userResponses = _mapper.Map<List<UserResponse>>(users);
            return userResponses;
        }
        public async Task<IEnumerable<User>> GetAllUsersDetails()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<UserResponse> GetUserById(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserId == id);
            var userResponse = _mapper.Map<UserResponse>(user);
            return userResponse;
        }
        public async Task<User> GetUserByUsernameOrEmail(string username, string email)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username || u.Email == email);
            return user!;
        }

        public async Task<List<Group>> GetUserGroups(int id)
        {
            var userGroups = await _context.Groups
                                   .Where(g => g.Users.Any(u => u.UserId == id))
                                   .Select(group => new Group
                                   {
                                       GroupId = group.GroupId,
                                       GroupName = group.GroupName,
                                       CreatorId = group.CreatorId,
                                       TotalExpenses = group.TotalExpenses,
                                       Creator = new User
                                       {
                                           UserId = group.Creator.UserId,
                                           Username = group.Creator.Username
                                       },
                                       // Other properties as needed
                                   })
                                   .ToListAsync();
            return userGroups ?? new List<Group>() { };
        }

        public async Task<User> GetUserDetailsById(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserId == id);
            return user!;
        }
    }
}
