using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TrackFinances.DataAccess.Data;
using TrackFinances.DataAccess.Models;
using Xunit;

namespace TrackFinances.Tests.Integration;

public class UserDataAccessTests : IAsyncLifetime
{
    private readonly IUserData _userData;
    private readonly List<string> _createdUserIds = new();

    public UserDataAccessTests(IUserData userData)
    {
        _userData = userData;
    }

    [Fact]
    public async Task CreateUser_CreatesUser_WhenDataIsCorrect()
    {
        var userCreateModel = MakeCreateUserModel("TestUser", "test@test.com", "$faux-hash");

        var newUser = await _userData.CreateAsync(userCreateModel);
        _createdUserIds.Add(newUser.Id.ToString());

        newUser.Id.Should().NotBe(Guid.Empty);
        newUser.UserName.Should().Be(userCreateModel.UserName);
        newUser.Email.Should().Be(userCreateModel.Email);
    }

    [Fact]
    public async Task CreateUser_ThrowSqlException_WhenDataIsNull()
    {
        var userCreateModel = MakeCreateUserModel(null, null, null);

        await _userData
                .Invoking(async x => await x.CreateAsync(userCreateModel))
                .Should()
                .ThrowAsync<SqlException>();
    }

    [Fact]
    public async Task CreateUser_ThrowSqlException_WhenDataIsDuplicate()
    {
        var userCreateModel = MakeCreateUserModel("TestUser", "test@test.com", "$faux-hash");

        var newUser = await _userData.CreateAsync(userCreateModel);
        _createdUserIds.Add(newUser.Id.ToString());
        await _userData
                .Invoking(async x => await x.CreateAsync(userCreateModel))
                .Should()
                .ThrowAsync<SqlException>();
    }

    [Fact]
    public async Task UpdateUser_ReturnsTrue_WhenDataIsCorrect()
    {
        var userCreateModel = MakeCreateUserModel("TestUser", "test@test.com", "$faux-hash");
        var user = await _userData.CreateAsync(userCreateModel);
        _createdUserIds.Add(user.Id.ToString());

        var userUpdateModel = MakeUpdateUserModel(user.Id.ToString(), "UpdatedUser", "updated-test@test.com", "$faux-hash");

        var isUpdated = await _userData.UpdateAsync(userUpdateModel);

        isUpdated.Should().Be(true);
    }

    [Fact]
    public async Task UpdateUser_ThrowSqlException_WhenDataIsDuplicate()
    {
        var userCreateModel = MakeCreateUserModel("TestUser", "test@test.com", "$faux-hash");
        var userCreateModel2 = MakeCreateUserModel("TestUser2", "test2@test.com", "$faux-hash");
        var user = await _userData.CreateAsync(userCreateModel);
        var user2 = await _userData.CreateAsync(userCreateModel2);
        _createdUserIds.Add(user.Id.ToString());
        _createdUserIds.Add(user2.Id.ToString());

        var userUpdateModel = MakeUpdateUserModel(user.Id.ToString(), "TestUser2", "updated-test@test.com", "$faux-hash");
        await _userData
                .Invoking(async x => await x.UpdateAsync(userUpdateModel))
                .Should()
                .ThrowAsync<SqlException>();
    }

    [Fact]
    public async Task GetUser_ReturnsUser_WhenUserExists()
    {
        var userCreateModel = MakeCreateUserModel("TestUser", "test@test.com", "$faux-hash");
        var createdUser = await _userData.CreateAsync(userCreateModel);
        _createdUserIds.Add(createdUser.Id.ToString());

        var retrievedUser = await _userData.GetAsync(createdUser.Id.ToString());
        retrievedUser.Should().BeEquivalentTo(createdUser);
    }

    [Fact]
    public async Task GetUser_ReturnsNull_WhenUserDoesNotExists()
    {

        var retrievedUser = await _userData.GetAsync(Guid.Empty.ToString());
        retrievedUser.Should().BeNull();
    }

    [Fact]
    public async Task GetUserByEmail_ReturnsUser_WhenUserExists()
    {
        var userCreateModel = MakeCreateUserModel("TestUser", "themaintestemail@test.com", "$faux-hash");
        var createdUser = await _userData.CreateAsync(userCreateModel);
        _createdUserIds.Add(createdUser.Id.ToString());

        var retrievedUser = await _userData.GetByEmailAsync("themaintestemail@test.com");
        retrievedUser.Should().BeEquivalentTo(createdUser);
    }

    [Fact]
    public async Task GetUserByEmail_ReturnsNull_WhenUserDoesNotExists()
    {

        var retrievedUser = await _userData.GetByEmailAsync("moretesting@test.com");
        retrievedUser.Should().BeNull();
    }

    [Fact]
    public async Task GetUserByUserName_ReturnsUser_WhenUserExists()
    {
        var userCreateModel = MakeCreateUserModel("TestUser", "themaintestemail@test.com", "$faux-hash");
        var createdUser = await _userData.CreateAsync(userCreateModel);
        _createdUserIds.Add(createdUser.Id.ToString());

        var retrievedUser = await _userData.GetByUserNameAsync("TestUser");
        retrievedUser.Should().BeEquivalentTo(createdUser);
    }

    [Fact]
    public async Task GetUserByUserName_ReturnsNull_WhenUserDoesNotExists()
    {

        var retrievedUser = await _userData.GetByUserNameAsync("TestUser9999");
        retrievedUser.Should().BeNull();
    }

    [Fact]
    public async Task Delete_ReturnsTrue_WhenUserExists()
    {
        var userCreateModel = MakeCreateUserModel("TestUser", "themaintestemail@test.com", "$faux-hash");
        var createdUser = await _userData.CreateAsync(userCreateModel);
        _createdUserIds.Add(createdUser.Id.ToString());

        var isDeleted = await _userData.DeleteAsync(createdUser.Id.ToString());
        isDeleted.Should().BeTrue();
    }

    [Fact]
    public async Task Delete_ReturnsFalse_WhenUserDoestNotExist()
    {
        var isDeleted = await _userData.DeleteAsync(Guid.Empty.ToString());
        isDeleted.Should().BeFalse();
    }

    private static UserCreate MakeCreateUserModel(string? userName,
                                                  string? email,
                                                  string? passwordHash)
    {
        return new UserCreate
        {
            UserName = userName!,
            Email = email!,
            PasswordHash = passwordHash!,
        };
    }

    private static UserUpdate MakeUpdateUserModel(string id,
                                                  string? userName,
                                                  string? email,
                                                  string? passwordHash)
    {
        return new UserUpdate 
        {
            Id = id,
            UserName = userName,
            Email = email,
            PasswordHash = passwordHash,
        };
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        foreach (var id in _createdUserIds)
        {
            try
            {
                await _userData.DeleteAsync(id);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);  
            }

        }
    }
}
