using Isopoh.Cryptography.Argon2;
using TrackFinances.DataAccess.Data;
using TrackFinances.DataAccess.Models;

namespace TrackFinances.Api.Services;

public class UserService : IUserService
{
    private readonly IUserData _userData;
    private readonly IHashingService _hashingService;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserData userData,
                       IHashingService hashingService,
                       ILogger<UserService> logger)
    {
        _userData = userData;
        _hashingService = hashingService;
        _logger = logger;
    }

    public async Task<User?> CreateAsync(UserCreate user, string password)
    {
        try
        {
            user.PasswordHash = _hashingService.HashPassword(password);
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
            user.PasswordHash = _hashingService.HashPassword(password);
            return await _userData.UpdateAsync(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return false;
        }
    }

    public async Task<bool> Login(string userName, string password)
    {
        var user = await GetByUserNameAsync(userName);
        if (user is null)
            return false;
        
        return _hashingService.VerifyPassword(user.PasswordHash, password);
    }
}
