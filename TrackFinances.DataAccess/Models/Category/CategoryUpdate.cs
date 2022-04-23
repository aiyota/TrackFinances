using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackFinances.DataAccess.Models;
public class CategoryUpdate
{
    public int Id { get; set; } = default!;
    public string? Name { get; set; }
    public string? Description { get; set; }
}
