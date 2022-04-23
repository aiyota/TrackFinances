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
public class CategoryDataAccessTests : IAsyncLifetime
{
    private readonly ICategoryData _categoryData;
    private readonly List<int> _createdCategoryIds = new();

    public CategoryDataAccessTests(ICategoryData categoryData)
    {
        _categoryData = categoryData;
    }

    [Fact]
    public async Task CreateCategory_CreatesCategory_WhenDataIsCorrect()
    {
        var category = new CategoryCreate { Name = "TestCategory" };
        var createdCategory = await _categoryData.CreateAsync(category);
        _createdCategoryIds.Add(createdCategory.Id);

        createdCategory.Id.Should().NotBe(0);
        createdCategory.Name.Should().Be(category.Name);
    }

    [Fact]
    public async Task CreateCategory_ThrowsSqlException_WhenDataIsNull()
    {
        await _categoryData
                .Invoking(async x => 
                    await x.CreateAsync(new CategoryCreate { Name = null! }))
                .Should()
                .ThrowAsync<SqlException>();
    }

    [Fact]
    public async Task CreateCategory_ThrowsSqlException_WhenDataIsDuplicate()
    {
        var createdCategory = await _categoryData.CreateAsync(new CategoryCreate { Name = "TestCategory2" });
        _createdCategoryIds.Add(createdCategory.Id);

        await _categoryData
                .Invoking(async x => 
                    await x.CreateAsync(new CategoryCreate { Name = "TestCategory2" }))
                .Should()
                .ThrowAsync<SqlException>();
    }

    [Fact]
    public async Task UpdateCategory_ReturnsTrue_WhenDataIsCorrect()
    {
        var createdCategory = await _categoryData.CreateAsync(new CategoryCreate { Name = "TestCategory3" });
        _createdCategoryIds.Add(createdCategory.Id);

        var updateCategoryModel = new CategoryUpdate { Id = createdCategory.Id, Name = "UpdatedName", Description = "Updated" };
        var isUpdated = await _categoryData.UpdateAsync(updateCategoryModel);
        isUpdated.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateCategory_ThrowSqlException_WhenDataIsDuplicate()
    {
        var createdCategory = await _categoryData.CreateAsync(new CategoryCreate { Name = "TestCategory4" });
        var createdCategory2 = await _categoryData.CreateAsync(new CategoryCreate { Name = "TestCategory5" });
        _createdCategoryIds.Add(createdCategory.Id);
        _createdCategoryIds.Add(createdCategory2.Id);

        var updateCategoryModel = new CategoryUpdate { Id = createdCategory.Id, Name = "TestCategory5" };
        await _categoryData
                .Invoking(async x => 
                    await x.UpdateAsync(updateCategoryModel))
                .Should()
                .ThrowAsync<SqlException>();
    }

    [Fact]
    public async Task GetAllCategories_ReturnsAllCategories_WhenInvoked()
    {
        var createdCategory = await _categoryData.CreateAsync(new CategoryCreate { Name = "TestCategory6" });
        _createdCategoryIds.Add(createdCategory.Id);

        var allCategories = await _categoryData.GetAllAsync();
        allCategories.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    public async Task DeleteCategory_ReturnsTrue_WhenCategoryExists()
    {
        var createdCategory = await _categoryData.CreateAsync(new CategoryCreate { Name = "TestCategory7" });
        _createdCategoryIds.Add(createdCategory.Id);

        var isDeleted = await _categoryData.DeleteAsync(createdCategory.Id);
        isDeleted.Should().BeTrue();
    }


    [Fact]
    public async Task DeleteCategory_ReturnsFalse_WhenCategoryDoesNotExist()
    {
        var isDeleted = await _categoryData.DeleteAsync(int.MinValue);
        isDeleted.Should().BeFalse();
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync() 
    {
        try
        {
            foreach (var id in _createdCategoryIds)
                await _categoryData.DeleteAsync(id);
        } catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

}
