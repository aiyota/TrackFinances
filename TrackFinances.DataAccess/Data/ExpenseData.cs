using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackFinances.DataAccess.DbAccess;
using TrackFinances.DataAccess.Models;

namespace TrackFinances.DataAccess.Data;
public class ExpenseData : IExpenseData
{
    private readonly ISqlDataAccess _database;

    public ExpenseData(ISqlDataAccess database)
    {
        _database = database;
    }

    public async Task<Expense?> Get(int id)
    {
        return await _database
                        .QueryFirstOrDefaultAsync<Expense, dynamic>("spExpense_Get", new { Id = id });
    }

    public async Task<IEnumerable<Expense>> GetByUserIdAsync(string userId)
    {
        return await _database
                        .QueryAsync<Expense, dynamic>("spExpense_GetAllByUserId", new { UserId = userId });
    }

    public async Task<Expense> CreateAsync(ExpenseCreate expense)
    {
        return await _database
                        .QueryFirstOrDefaultAsync<Expense, ExpenseCreate>("spExpense_Create", expense);
    }

    public async Task<bool> UpdateAsync(ExpenseUpdate expense)
    {
        return await _database
                        .ExecuteAsync("spExpense_Update", expense);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _database
                        .ExecuteAsync<dynamic>("spExpense_Delete", new { Id = id });
    }
}
