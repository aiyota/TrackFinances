using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackFinances.DataAccess.Models;

namespace TrackFinances.DataAccess.Data;
public interface IExpenseData
{
    Task<Expense?> Get(int id);
    Task<IEnumerable<Expense>> GetByUserIdAsync(string userId);
    Task<Expense> CreateAsync(ExpenseCreate expense);
    Task<bool> UpdateAsync(ExpenseUpdate expense);
    Task<bool> DeleteAsync(int id);
}
