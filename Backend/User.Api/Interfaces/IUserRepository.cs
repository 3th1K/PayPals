using Data.Models;
using UserService.Api.Models;

namespace UserService.Api.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<UserResponse>> GetAllUsers();
        public Task<IEnumerable<User>> GetAllUsersDetails();
        public Task<UserResponse> GetUserById(int id);
        public Task<List<Group>> GetUserGroups(int id);
        public Task<User> GetUserDetailsById(int id);
        public Task<User> GetUserByUsernameOrEmail(string username, string email);
        public Task<UserResponse> CreateUser(User user);
        public Task<UserResponse> UpdateUser(UserUpdateRequest user);
        public Task<User?> DeleteUser(int id);
    }
}
