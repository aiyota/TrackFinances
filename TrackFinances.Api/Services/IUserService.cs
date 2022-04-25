using TrackFinances.DataAccess.Models;

namespace TrackFinances.Api.Services;

public interface IUserService
{
    Task<User?> GetAsync(string id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUserNameAsync(string username);
    Task<User?> CreateAsync(UserCreate user, string password);
    Task<bool> UpdateAsync(UserUpdate user);
    Task<bool> DeleteAsync(string id);
    Task<bool> Login(string userId, string password);
}
