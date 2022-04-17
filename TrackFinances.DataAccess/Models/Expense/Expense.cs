using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackFinances.DataAccess.Models;

public class Expense
{
    public int Id { get; set; } = default!;
    public Guid UserId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Charge { get; set; } = default!;
    public DateTime? ChargeDate { get; set; }
    public int CategoryId { get; set; } = default!;
    public bool IsDirectCharge { get; set; } = default!;
    public bool IsCleared { get; set; } = default!;
    public DateTime? DateCreated { get; set; }
}
