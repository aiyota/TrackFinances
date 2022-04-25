using AutoMapper;
using TrackFinances.Api.Contracts.V1;
using TrackFinances.Api.Contracts.V1.Requests;
using TrackFinances.Api.Contracts.V1.Responses;
using TrackFinances.DataAccess.Data;
using TrackFinances.DataAccess.Models;

namespace TrackFinances.Api.Services;

public class UserService : IUserService
{
    private readonly IUserData _userData;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserData userData,
                       ILogger<UserService> logger)
    {
        _userData = userData;
        _logger = logger;
    }

    public async Task<User?> CreateAsync(UserCreate user, string password)
    {
        try
        {
            // TODO: Implement password hashing
            user.PasswordHash = HashPassword(password);
            return await _userData.CreateAsync(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }

    public async Task<bool> DeleteAsync(string id)
    {
        try
        {
          return await _userData.DeleteAsync(id);
        } catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return false;
        }
    }

    public async Task<User?> GetAsync(string id)
    {
        try
        {
            return await _userData.GetAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    } 

    public async Task<User?> GetByEmailAsync(string email)
    {
        try
        {
            return await _userData.GetByEmailAsync(email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }

    public async Task<User?> GetByUserNameAsync(string username)
    {
        try
        {
            return await _userData.GetByUserNameAsync(username);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }

    public async Task<bool> UpdateAsync(UserUpdate user)
    {
        try
        {
            return await _userData.UpdateAsync(user);  
        } 
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return false;
        }
    }

    public async Task<bool> UpdateAsync(UserUpdate user, string password)
    {
        try
        {
            user.PasswordHash = HashPassword(password);
            return await _userData.UpdateAsync(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return false;
        }
    }

    private static string HashPassword(string password)
    {
        return "fake-hash-cjo/PnNJczwtQzMiIy9kQGFR";
    }
}
