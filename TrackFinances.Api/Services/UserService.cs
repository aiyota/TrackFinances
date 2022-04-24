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
    private readonly IMapper _mapper;

    public UserService(IUserData userData,
                       ILogger<UserService> logger,
                       IMapper mapper)
    {
        _userData = userData;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<UserResponse?> CreateAsync(UserCreateRequest user)
    {
        try
        {
            var mappedUser = _mapper.Map<UserCreate>(user);

            // TODO: implement pw hashing
            mappedUser.PasswordHash = FakeHashPassword(user.Password);

            var retrievedUser = await _userData.CreateAsync(mappedUser);
            return _mapper.Map<UserResponse>(retrievedUser);
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

    public async Task<UserResponse?> GetAsync(string id)
    {
        try
        {
            var retrievedUser = await _userData.GetAsync(id);
            return _mapper.Map<UserResponse>(retrievedUser);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    } 

    public async Task<UserResponse?> GetByEmailAsync(string email)
    {
        try
        {
            var retrievedUser = await _userData.GetByEmailAsync(email);
            return _mapper.Map<UserResponse>(retrievedUser);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }

    public async Task<UserResponse?> GetByUserNameAsync(string username)
    {
        try
        {
            var retrivedUser = await _userData.GetByUserNameAsync(username);
            return _mapper.Map<UserResponse>(retrivedUser);
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

            // TODO: implement pw hashing
            if (!string.IsNullOrEmpty(user.Password))
                mappedUser.PasswordHash = FakeHashPassword(user.Password);

            return await _userData.UpdateAsync(mappedUser);  
        } 
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return false;
        }
    }

    private string FakeHashPassword(string password)
    {
        return "fake-hash-cjo/PnNJczwtQzMiIy9kQGFR";
    }
}
