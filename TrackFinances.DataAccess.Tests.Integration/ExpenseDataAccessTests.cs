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
    public async Task CreateExpense_ThrowsSqlException_WhenNameIsNull()
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

    [Fact]
    public async Task GetExpense_ReturnsExpense_WhenExpenseExists()
    {
        var user = await CreateUser();
        var category = await CreateCategory();
        var expenseCreateModel = new ExpenseCreate
        {
            UserId = user.Id,
            Name = "Test Expense2",
            Charge = 19.22m,
            CategoryId = category.Id,
        };

        var expense = await _expenseData.CreateAsync(expenseCreateModel);
        _createdExpenseIds.Add(expense.Id);

        var retrievedExpense = await _expenseData.Get(expense.Id);

        retrievedExpense.Should().NotBeNull();
    }

    [Fact]
    public async Task GetExpense_ReturnsNull_WhenExpenseDoesNotExist()
    {
        var retrievedExpense = await _expenseData.Get(int.MinValue);
        retrievedExpense.Should().BeNull(); 
    }

    [Fact]
    public async Task GetExpenseByUser_ReturnsIEnumerableWithExpenses_IfUserExists()
    {
        var user = await CreateUser();
        var category = await CreateCategory();
        var expenseCreateModel = new ExpenseCreate
        {
            UserId = user.Id,
            Name = "Test Expense3",
            Charge = 19.22m,
            CategoryId = category.Id,
        };
        var expense = await _expenseData.CreateAsync(expenseCreateModel);
        _createdExpenseIds.Add(expense.Id);

        var retrievedExpenses = await _expenseData.GetByUserIdAsync(user.Id.ToString());
        retrievedExpenses.Count().Should().Be(1);
    }

    [Fact]
    public async Task GetExpenseByUser_ReturnsIEnumerableWithoutExpenses_IfUserDoesNotExist()
    {
        var retrievedExpenses = await _expenseData.GetByUserIdAsync(Guid.Empty.ToString());
        retrievedExpenses.Count().Should().Be(0);
    }

    [Fact]
    public async Task UpdateExpense_ReturnsTrue_IfDataIsCorrect()
    {
        var user = await CreateUser();
        var category = await CreateCategory();
        var expenseCreateModel = new ExpenseCreate
        {
            UserId = user.Id,
            Name = "Test Expense4",
            Charge = 19.22m,
            CategoryId = category.Id,
        };
        var expense = await _expenseData.CreateAsync(expenseCreateModel);
        _createdExpenseIds.Add(expense.Id);

        var updateModel = new ExpenseUpdate { Id = expense.Id, Name = "UpdatedExpense4", Charge = 33.22m };
        var isUpdated = await _expenseData.UpdateAsync(updateModel);
        isUpdated.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateExpense_ReturnsFalse_IfExpenseDoesNotExist()
    {
        var updateModel = new ExpenseUpdate { Id = int.MinValue, Name = "UpdatedExpense4", Charge = 33.22m };
        var isUpdated = await _expenseData.UpdateAsync(updateModel);
        isUpdated.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteExpense_ReturnsTrue_WhenExpenseExists()
    {
        var user = await CreateUser();
        var category = await CreateCategory();
        var expenseCreateModel = new ExpenseCreate
        {
            UserId = user.Id,
            Name = "Test Expense5",
            Charge = 19.22m,
            CategoryId = category.Id,
        };
        var expense = await _expenseData.CreateAsync(expenseCreateModel);
        _createdExpenseIds.Add(expense.Id);

        var isDeleted = await _expenseData.DeleteAsync(expense.Id);
        isDeleted.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteExpense_ReturnsFalse_WhenExpenseDoesNotExist()
    {
        var isDeleted = await _expenseData.DeleteAsync(int.MinValue);
        isDeleted.Should().BeFalse();
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
