using SparePartsWarehouse.CORE;
using SparePartsWarehouse.CORE.Entities;
using SparePartsWarehouse.DATA.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.DATA.Service
{
    public class PurchaseInService : IPurchaseInService
    {
        private readonly AppDbContext _context;

        public PurchaseInService(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(PurchaseIn purchaseIn)
        {
            _context.PurchaseIns.Add(purchaseIn);
            await _context.SaveChangesAsync();
        }
    }

}
