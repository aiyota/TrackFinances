using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackFinances.DataAccess.Models;
public class ExpenseUpdate
{
    public int Id { get; set; } = default!;
    public Guid? UserId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Charge { get; set; }
    public DateTime? ChargeDate { get; set; }
    public int? CategoryId { get; set; }
    public bool? IsDirectCharge { get; set; }
    public bool? IsCleared { get; set; }
}
