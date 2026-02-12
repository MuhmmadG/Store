using SparePartsWarehouse.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.Interfaces
{
    public interface IDepartmentService
    {
        Task<StoreDepartment> CreateIfNotExistsAsync(string name);
    }

}
