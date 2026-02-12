using Microsoft.EntityFrameworkCore;
using SparePartsWarehouse.CORE.DTO;
using SparePartsWarehouse.CORE.Entities;
using SparePartsWarehouse.CORE.Interfaces;
using SparePartsWarehouse.DATA.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.DATA.Service
{
    public class LookupService : ILookupService
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public LookupService(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        // ===============================
        // إنشاء صنف + وصفه (إن لم يوجد)
        // ===============================
        public async Task<Item> CreateItemIfNotExistsAsync(
            string itemName,
            int categoryId,
            string itemCode)
        {
            await using var context = _contextFactory.CreateDbContext();

            var item = await context.Items
                .Include(i => i.Descriptions)
                .FirstOrDefaultAsync(i =>
                    i.ItemName == itemName &&
                    i.CategoryId == categoryId);

            if (item == null)
            {
                item = new Item
                {
                    ItemName = itemName,
                    CategoryId = categoryId,
                    Unit = "قطعة",
                    ReorderLevel = 0,
                    IsActive = true
                };

                context.Items.Add(item);
                await context.SaveChangesAsync();
            }

            if (!string.IsNullOrWhiteSpace(itemCode) &&
                !item.Descriptions.Any(d =>
                    d.ItemCode.Equals(itemCode, StringComparison.OrdinalIgnoreCase)))
            {
                item.Descriptions.Add(new ItemDescription
                {
                    ItemCode = itemCode,
                    ItemId = item.Id
                });

                await context.SaveChangesAsync();
            }

            return item;
        }

        // ===============================
        // الأقسام (المخازن)
        // ===============================
        public async Task<List<StoreDepartment>> GetDepartmentsAsync()
        {
            await using var context = _contextFactory.CreateDbContext();
            return await context.StoreDepartments
                .Where(d => d.IsActive)
                .ToListAsync();
        }

        public async Task<List<StoreDepartment>> GetStoreDepartmentsAsync()
        {
            await using var context = _contextFactory.CreateDbContext();
            return await context.StoreDepartments
                .Where(x => x.IsActive)
                .OrderBy(x => x.Name)
                .AsNoTracking()
                .ToListAsync();
        }

        // ===============================
        // الفئات
        // ===============================
        public async Task<List<Category>> GetCategoriesByDepartmentAsync(int departmentId)
        {
            await using var context = _contextFactory.CreateDbContext();
            return await context.Categories
                .Where(c => c.StoreDepartmentId == departmentId && c.IsActive)
                .ToListAsync();
        }

        // ===============================
        // الأصناف
        // ===============================
        public async Task<List<Item>> GetItemsByCategoryAsync(int categoryId)
        {
            await using var context = _contextFactory.CreateDbContext();
            return await context.Items
                .Include(i => i.Descriptions)
                .Where(i => i.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<List<Item>> GetItemsAsync()
        {
            await using var context = _contextFactory.CreateDbContext();
            return await context.Items
                .Include(i => i.Descriptions)
                .Where(i => i.IsActive)
                .OrderBy(i => i.ItemName)
                .AsNoTracking()
                .ToListAsync();
        }

        // ===============================
        // أوصاف الأصناف
        // ===============================
        public async Task<List<ItemDescription>> GetItemDescriptionsByItemIdAsync(int itemId)
        {
            await using var context = _contextFactory.CreateDbContext();
            return await context.ItemDescriptions
                .Where(x => x.ItemId == itemId)
                .OrderBy(x => x.ItemCode)
                .AsNoTracking()
                .ToListAsync();
        }

        // ===============================
        // الموردين
        // ===============================
        public async Task<List<Supplier>> GetSuppliersAsync()
        {
            await using var context = _contextFactory.CreateDbContext();
            return await context.Suppliers
                .Where(s => s.IsActive)
                .ToListAsync();
        }

        // ===============================
        // أقسام المصنع
        // ===============================
        public async Task<List<FactoryDepartment>> GetFactoryDepartmentsAsync()
        {
            await using var context = _contextFactory.CreateDbContext();
            return await context.FactoryDepartments
                .OrderBy(x => x.Name)
                .AsNoTracking()
                .ToListAsync();
        }

        // ===============================
        // الماكينات
        // ===============================
        public async Task<List<Machine>> GetMachinesByFactoryDepartmentAsync(int factoryDepartmentId)
        {
            await using var context = _contextFactory.CreateDbContext();
            return await context.Machines
                .Include(i => i.FactoryDepartment)
                .Where(x => x.FactoryDepartmentId == factoryDepartmentId)
                .ToListAsync();
        }
    }

}
