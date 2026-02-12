using SparePartsWarehouse.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.Interfaces
{
    public interface ILookupService
    {
        Task<List<StoreDepartment>> GetDepartmentsAsync();
        Task<List<Category>> GetCategoriesByDepartmentAsync(int departmentId);
        Task<List<Item>> GetItemsByCategoryAsync(int categoryId);
        Task<List<Supplier>> GetSuppliersAsync();
        Task<Item> CreateItemIfNotExistsAsync(string itemName, int categoryId , string itemCode);
        //--------------------------------
        Task<List<StoreDepartment>> GetStoreDepartmentsAsync();
        Task<List<FactoryDepartment>> GetFactoryDepartmentsAsync();
        Task<List<Machine>> GetMachinesByFactoryDepartmentAsync(int FactoryDepartmentId);

        Task<List<ItemDescription>> GetItemDescriptionsByItemIdAsync(int itemId);
        Task<List<Item>> GetItemsAsync();
    }

}
