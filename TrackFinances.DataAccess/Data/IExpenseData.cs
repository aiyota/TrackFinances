using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackFinances.DataAccess.Models;

namespace TrackFinances.DataAccess.Data;
public interface IExpenseData
{
    Task<Expense> Get(string id);
    Task<IEnumerable<Expense>> GetByUserIdAsync(string userId);
    Task<Expense> CreateAsync(Expense expense);
    Task<bool> UpdateAsync(Expense expense);
    Task<bool> DeleteAsync(string id);
}
