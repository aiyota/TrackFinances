using AutoMapper;
using TrackFinances.Api.Contracts.V1;
using TrackFinances.DataAccess.Data;
using TrackFinances.DataAccess.Models;

namespace TrackFinances.Api.Services;

public class UserService : IUserService
{
    private readonly IUserData _userData;
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;

    public UserService(IUserData userData,
                       ILogger<UserService> logger,
                       IMapper mapper)
    {
        _userData = userData;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<User?> CreateAsync(UserCreateRequest user)
    {
        try
        {
            var mappedUser = _mapper.Map<UserCreate>(user);  
            return await _userData.CreateAsync(mappedUser);
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

    public async Task<bool> UpdateAsync(UserUpdateRequest user)
    {
        try
        {
            var mappedUser = _mapper.Map<UserUpdate>(user);
            return await _userData.UpdateAsync(mappedUser);  
        } 
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return false;
        }
    }
}
