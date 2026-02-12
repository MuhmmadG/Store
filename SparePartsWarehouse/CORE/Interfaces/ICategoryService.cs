using SparePartsWarehouse.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.Interfaces
{
    public interface ICategoryService
    {
       public Task<Category> CreateIfNotExistsAsync(string name, int departmentId);
    }

}
