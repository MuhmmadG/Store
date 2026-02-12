using Microsoft.EntityFrameworkCore;
using SparePartsWarehouse.CORE.Entities;
using SparePartsWarehouse.CORE.Interfaces;
using SparePartsWarehouse.DATA.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.DATA.Service
{
    public class SupplierService : ISupplierService
    {
        private readonly AppDbContext _context;

        public SupplierService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Supplier> CreateSupplierIfNotExistsAsync(string supplierName)
        {
            supplierName = supplierName.Trim();

            // 🔴 حماية
            if (string.IsNullOrWhiteSpace(supplierName))
                throw new ArgumentException("اسم المورد لا يمكن أن يكون فارغًا");

            // 1️⃣ هل المورد موجود؟
            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(s => s.SupplierName == supplierName);

            if (supplier != null)
                return supplier;

            // 2️⃣ إنشاء مورد جديد
            supplier = new Supplier
            {
                SupplierName = supplierName
            };

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return supplier;
        }
    }

}
