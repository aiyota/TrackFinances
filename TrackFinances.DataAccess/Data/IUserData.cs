using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackFinances.DataAccess.Models;

namespace TrackFinances.DataAccess.Data;

public interface IUserData
{
    Task<User?> GetAsync(string id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUserNameAsync(string username);
    Task<User> CreateAsync(UserCreate user);
    Task<bool> UpdateAsync(UserUpdate user);
    Task<bool> DeleteAsync(string id);
}
