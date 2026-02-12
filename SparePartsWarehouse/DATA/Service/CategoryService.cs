using Microsoft.EntityFrameworkCore;
using SparePartsWarehouse.CORE.Entities;
using SparePartsWarehouse.CORE.Interfaces;
using SparePartsWarehouse.DATA.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.DATA.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Category> CreateIfNotExistsAsync(string name, int departmentId)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c =>
                    c.Name == name && c.StoreDepartmentId == departmentId);

            if (category != null)
                return category;

            category = new Category
            {
                Name = name,
                StoreDepartmentId = departmentId,
                IsActive = true
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return category;
        }


    }
}
