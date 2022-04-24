using TrackFinances.Api.Contracts.V1;
using TrackFinances.Api.Contracts.V1.Requests;
using TrackFinances.Api.Contracts.V1.Responses;
using TrackFinances.DataAccess.Models;

namespace TrackFinances.Api.Services;

public interface IUserService
{
    Task<UserResponse?> GetAsync(string id);
    Task<UserResponse?> GetByEmailAsync(string email);
    Task<UserResponse?> GetByUserNameAsync(string username);
    Task<UserResponse?> CreateAsync(UserCreateRequest user);
    Task<bool> UpdateAsync(UserUpdateRequest user);
    Task<bool> DeleteAsync(string id);
}
