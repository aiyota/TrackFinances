using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackFinances.DataAccess.Data;
using TrackFinances.DataAccess.DbAccess;
using TrackFinances.DataAccess.Models;
using Xunit;

namespace TrackFinances.Tests.Integration;

public class UserDataAccessTests : IAsyncLifetime
{
    private readonly IUserData _userData;
    private readonly List<string> _createdUserIds = new();

    public UserDataAccessTests()
    {
        var config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
        var sqlDataAccess = new SqlDataAccess(config);
        _userData = new UserData(sqlDataAccess);
    }

    [Fact]
    public async Task CreateUser_CreatesUser_WhenDataIsCorrect()
    {
        var userCreateModel = CreateUser();
        var newUser = await _userData.CreateAsync(userCreateModel);
        _createdUserIds.Add(newUser.Id.ToString());

        newUser.Id.Should().NotBe(Guid.Empty);
        newUser.UserName.Should().Be(userCreateModel.UserName);
        newUser.Email.Should().Be(userCreateModel.Email);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        foreach (var id in _createdUserIds)
            await _userData.DeleteAsync(id);
    }

    private UserCreate CreateUser(string userName = "TestUser",
                            string email = "test@email.com",
                            string passwordHash = "$2a$10$Yga0pe1YaYoJfX.dWp/JfuDozZvUOEwzRjITwUBRfux80jyE.f3E6")
    {
        return new UserCreate
        {
            UserName = userName,
            Email = email,
            PasswordHash = passwordHash
        };
    }
}
