using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackFinances.DataAccess.DbAccess;
using TrackFinances.DataAccess.Models;

namespace TrackFinances.DataAccess.Data;
public class CategoryData : ICategoryData
{
    private readonly ISqlDataAccess _database;

    public CategoryData(ISqlDataAccess database)
    {
        _database = database;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _database
                        .QueryAsync<Category>("spCategory_GetAll");
    }

    public async Task<Category> CreateAsync(Category category)
    {
        return await _database
                        .QueryFirstOrDefaultAsync<Category, Category>("spCategory_Create", category);
    }

    public async Task<bool> UpdateAsync(Category category)
    {
        return await _database
                        .ExecuteAsync("spCategory_Update", category);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _database
                        .ExecuteAsync<dynamic>("spCategory_Delete", new { Id = id });
    }
}
