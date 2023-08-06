using Identity.Api.Models;

namespace Identity.Api.Interfaces
{
    public interface IIdentityRepository
    {
        public Task<User> ValidateUser(string username, string password);
        public Task<string> GetToken(string username, string password);

        //public string GenerateJwtToken<T>(T client);
    }
}
