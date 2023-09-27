using Data.DTOs.ExpenseDTOs;
using Data.DTOs.UserDTOs;
using Data.Models;

namespace UserService.Api.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<UserResponse>> GetAllUsers();
        public Task<IEnumerable<UserDetailsResponse>> GetAllUsersDetails();
        public Task<UserResponse> GetUserById(int id);
        public Task<List<Group>> GetUserGroups(int id);
        public Task<List<ExpenseResponse>> GetUserExpenses(int id);
        public Task<UserDetailsResponse> GetUserDetailsById(int id);
        public Task<User> GetUserByUsernameOrEmail(string username, string email);
        public Task<UserResponse> CreateUser(User user);
        public Task<UserResponse> UpdateUser(UserUpdateRequest user);
        public Task<User?> DeleteUser(int id);
    }
}
