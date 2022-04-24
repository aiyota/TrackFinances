using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackFinances.DataAccess.Data;
using TrackFinances.DataAccess.Models;
using Xunit;

namespace TrackFinances.DataAccess.Tests.Integration;
public class ExpenseDataAccessTests : IAsyncLifetime
{
    private readonly IExpenseData _expenseData;
    private readonly IUserData _userData;
    private readonly ICategoryData _categoryData;
    private readonly List<int> _createdExpenseIds = new();
    private readonly List<int> _createdCategoryIds = new();
    private readonly List<string> _createdUserIds = new();

    public ExpenseDataAccessTests(IExpenseData expenseData,
                                  IUserData userData,
                                  ICategoryData categoryData)
    {
        _expenseData = expenseData;
        _userData = userData;
        _categoryData = categoryData;
    }

    [Fact]
    public async Task CreateExpense_CreatesExpense_WhenDataIsCorrect()
    {
        var user = await CreateUser();

        var category = await CreateCategory();

        var expenseCreateModel = new ExpenseCreate
        {
            UserId = user.Id,
            Name = "Test Expense",
            Charge = 19.22m,
            CategoryId = category.Id,
        };

        var expense = await _expenseData.CreateAsync(expenseCreateModel);
        _createdExpenseIds.Add(expense.Id);

        expense.UserId.Should().Be(expenseCreateModel.UserId);
        expense.Name.Should().Be(expenseCreateModel.Name);
        expense.Charge.Should().Be(expenseCreateModel.Charge);
        expense.CategoryId.Should().Be(category.Id);
    }

    [Fact]
    public async Task CreateExpense_ThrowSqlException_WhenNameIsNull()
    {
        var user = await CreateUser();

        var category = await CreateCategory();

        var expenseCreateModel = new ExpenseCreate
        {
            UserId = user.Id,
            Name = null!,
            CategoryId = category.Id,
        };

        await _expenseData
                .Invoking(x => 
                    x.CreateAsync(expenseCreateModel))
                .Should()
                .ThrowAsync<SqlException>();
    }

    private async Task<Category> CreateCategory()
    {
        var createCategoryModel = new CategoryCreate { Name = "TestCategory555" };
        var category = await _categoryData.CreateAsync(createCategoryModel);
        _createdCategoryIds.Add(category.Id);
        return category;
    }

    private async Task<User> CreateUser()
    {
        var createUserModel = new UserCreate
        {
            UserName = "TestUser555",
            Email = "Test5555@Test.com",
            PasswordHash = "$fake-hash"
        };
        var user = await _userData.CreateAsync(createUserModel);
        _createdUserIds.Add(user.Id.ToString());
        return user;
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        try
        {
            foreach (var id in _createdExpenseIds)
                await _expenseData.DeleteAsync(id);

            foreach (var userId in _createdUserIds)
                await _userData.DeleteAsync(userId);

            foreach (var categoryId in _createdCategoryIds)
                await _categoryData.DeleteAsync(categoryId);
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }
    }

}
