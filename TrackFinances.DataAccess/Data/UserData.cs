﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackFinances.DataAccess.DbAccess;
using TrackFinances.DataAccess.Models;

namespace TrackFinances.DataAccess.Data;
public class UserData : IUserData
{
    private readonly ISqlDataAccess _database;

    public UserData(ISqlDataAccess database)
    {
        _database = database;
    }

    public async Task<User?> GetAsync(string id)
    {
        return await _database.QueryFirstOrDefaultAsync<User, dynamic>("spUser_Get", new { Id = id });
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _database.QueryFirstOrDefaultAsync<User, dynamic>("spUser_GetByEmail", new { Email = email });
    }

    public async Task<User?> GetByUserNameAsync(string username)
    {
        return await _database.QueryFirstOrDefaultAsync<User, dynamic>("spUser_GetByUserName", new { UserName = username });
    }

    public async Task<User> CreateAsync(UserCreate user)
    {
        return await _database
                        .QueryFirstOrDefaultAsync<User, UserCreate>("spUser_Create", user);
    }

    public async Task<bool> UpdateAsync(UserUpdate user)
    {
        return await _database.ExecuteAsync("spUser_Update", user);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _database.ExecuteAsync<dynamic>("spUser_Delete", new { Id = id });
    }
}
