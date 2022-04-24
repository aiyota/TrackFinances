using TrackFinances.Api.Contracts.V1;
using TrackFinances.DataAccess.Models;

namespace TrackFinances.Api.Services;

public interface IUserService
{
    Task<User?> GetAsync(string id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUserNameAsync(string username);
    Task<User?> CreateAsync(UserCreateRequest user);
    Task<bool> UpdateAsync(UserUpdateRequest user);
    Task<bool> DeleteAsync(string id);
}
