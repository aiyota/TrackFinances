using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackFinances.DataAccess.Models;
public class CategoryCreate
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
