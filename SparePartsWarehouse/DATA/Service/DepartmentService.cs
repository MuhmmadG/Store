using Microsoft.EntityFrameworkCore;
using SparePartsWarehouse.CORE.Entities;
using SparePartsWarehouse.CORE.Interfaces;
using SparePartsWarehouse.DATA.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.DATA.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly AppDbContext _context;

        public DepartmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<StoreDepartment> CreateIfNotExistsAsync(string name)
        {
            var dept = await _context.StoreDepartments
                .FirstOrDefaultAsync(d => d.Name == name);

            if (dept != null)
                return dept;

            dept = new StoreDepartment
            {
                Name = name,
                IsActive = true
            };

            _context.StoreDepartments.Add(dept);
            await _context.SaveChangesAsync();

            return dept;
        }

    }
}
