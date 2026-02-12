using SparePartsWarehouse.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.Interfaces
{
    public interface ISupplierService
    {
        Task<Supplier> CreateSupplierIfNotExistsAsync(string supplierName);
    }

}
