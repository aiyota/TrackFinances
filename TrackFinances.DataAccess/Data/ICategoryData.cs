﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackFinances.DataAccess.Models;

namespace TrackFinances.DataAccess.Data;
public interface ICategoryData
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category> CreateAsync(CategoryCreate category);
    Task<bool> UpdateAsync(CategoryUpdate category);
    Task<bool> DeleteAsync(int id);
}
