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
    public class ItemMovementService : IItemMovementService
    {
        private readonly AppDbContext _context;

        public ItemMovementService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ItemMovement>> GetAsync(
            int? itemId,
            int? itemDescriptionId)
        {
            var query = _context.ItemMovements.AsQueryable();

            if (itemId.HasValue)
                query = query.Where(x => x.ItemName ==
                    _context.Items
                        .Where(i => i.Id == itemId)
                        .Select(i => i.ItemName)
                        .FirstOrDefault());

            if (itemDescriptionId.HasValue)
                query = query.Where(x => x.ItemCode ==
                    _context.ItemDescriptions
                        .Where(d => d.Id == itemDescriptionId)
                        .Select(d => d.ItemCode)
                        .FirstOrDefault());

            return await query
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }
      
    }


}
