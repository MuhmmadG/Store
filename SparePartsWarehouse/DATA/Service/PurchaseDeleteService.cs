using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SparePartsWarehouse.CORE.DTO;
using SparePartsWarehouse.CORE.Interfaces;
using SparePartsWarehouse.DATA.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SparePartsWarehouse.DATA.Service
{
    public class PurchaseDeleteService : IPurchaseDeleteService
    {
        private readonly AppDbContext _context;

        public PurchaseDeleteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CanDeleteInvoiceAsync(int purchaseInId)
        {
            var result = await _context.Database
                .SqlQueryRaw<int>(
                    "EXEC sp_CanDeletePurchaseInvoice @PurchaseInId",
                    new SqlParameter("@PurchaseInId", purchaseInId))
                .ToListAsync();

            return result.First() == 1;
        }

        public async Task DeleteItemAsync(int purchaseDetailId)
        {

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_DeletePurchaseItem @PurchaseDetailId",
                new SqlParameter("@PurchaseDetailId", purchaseDetailId));
        }
       
        public async Task<List<DeleteResultDto>> DeleteItemsAsync(List<int> ids)
        {
            var table = new DataTable();
            table.Columns.Add("Id", typeof(int));

            foreach (var id in ids)
                table.Rows.Add(id);

            var param = new SqlParameter("@PurchaseDetailIds", table)
            {
                SqlDbType = SqlDbType.Structured,
                TypeName = "dbo.IntList"
            };

            return await _context.Database
                .SqlQueryRaw<DeleteResultDto>(
                    "EXEC sp_DeletePurchaseItems @PurchaseDetailIds",
                    param)
                .ToListAsync();
        }


        public async Task DeleteInvoiceAsync(int purchaseInId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_DeletePurchaseInvoice @PurchaseInId",
                new SqlParameter("@PurchaseInId", purchaseInId));
        }
        public async Task<List<SupplierDto>> GetSuppliersAsync()
        {
            return await _context.Suppliers
                .Where(s => s.IsActive)
                .OrderBy(s => s.SupplierName)
                .Select(s => new SupplierDto
                {
                    Id = s.Id,
                    SupplierName = s.SupplierName
                })
                .ToListAsync();
        }
        public async Task<List<PurchaseInvoiceDto>> GetInvoicesBySupplierAsync(int supplierId)
        {
            return await _context.PurchaseIns
                .Where(p => p.SupplierId == supplierId)
                .OrderByDescending(p => p.Date)
                .Select(p => new PurchaseInvoiceDto
                {
                    Id = p.Id,
                    NumberInvoice = p.NumberInvoice,
                    Date = p.Date
                })
                .ToListAsync();
        }
        public async Task<List<PurchaseItemDto>> GetInvoiceItemsAsync(int purchaseInvoiceId)
        {
            return await _context.PurchaseInDetails
                .Where(d => d.PurchaseInId == purchaseInvoiceId)
                .Select(d => new PurchaseItemDto
                {
                    PurchaseDetailId = d.Id,
                    ItemName = d.Item.ItemName,
                    ItemCode = d.ItemDescription.ItemCode,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice,
                    IsSelected = false
                })
                .ToListAsync();
        }

    }

}
