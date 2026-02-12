using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SparePartsWarehouse.CORE.DTO;
using SparePartsWarehouse.CORE.Entities;
using SparePartsWarehouse.CORE.Interfaces;
using SparePartsWarehouse.DATA.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SparePartsWarehouse.DATA.StockOutService
{
    public class StockOutService :  IStockOutService
    {
        private class StockBalanceInfo
        {
            public string ItemName { get; set; } = "";
            public string ItemCode { get; set; } = "";
            public decimal AvailableQty { get; set; }
            public decimal AverageUnitCost { get; set; }
        }
      


        private readonly AppDbContext _context;

        public StockOutService(AppDbContext context)
        {
            _context = context;
        }
        public async Task SaveStockOutAsync(
            string receiverName,
            DateTime date,
    List<StockOutRequestDto> items,
    int factoryDepartmentId,
    int storeDepartmentId,
    int machineId)
        {
            if (items == null || !items.Any())
                throw new ArgumentException("لا يوجد أصناف للصرف");

            var stockOut = new StockOut
            {
                ReceiverName = receiverName,
                Date = date,
                FactoryDepartmentId = factoryDepartmentId,
                StoreDepartmentId = storeDepartmentId,
                MachineId = machineId,
                Details = new List<StockOutDetail>()
            };

            foreach (var req in items)
            {
                // ===============================
                // 1️⃣ إجمالي الكمية الواردة
                // ===============================
                var totalInQty = await _context.PurchaseInDetails
                    .Where(d => d.ItemDescriptionId == req.ItemDescriptionId)
                    .SumAsync(d => (decimal?)d.Quantity) ?? 0;

                if (totalInQty <= 0)
                    throw new InvalidOperationException(
                        $"لا يوجد وارد للصنف {req.ItemDescriptionId}");

                // ===============================
                // 2️⃣ إجمالي الكمية المنصرفة
                // ===============================
                var totalOutQty = await _context.StockOutDetails
                    .Where(d => d.ItemDescriptionId == req.ItemDescriptionId)
                    .SumAsync(d => (decimal?)d.Quantity) ?? 0;

                var remainingQty = totalInQty - totalOutQty;

                if (remainingQty <= 0)
                    throw new InvalidOperationException(
                        $"لا يوجد رصيد متبقي للصنف");

                if (req.Quantity > remainingQty)
                    throw new InvalidOperationException(
                        $"الكمية غير متاحة، المتاح فقط: {remainingQty}");

                // ===============================
                // 3️⃣ إجمالي قيمة الوارد
                // ===============================
                var totalInValue = await _context.PurchaseInDetails
                    .Where(d => d.ItemDescriptionId == req.ItemDescriptionId && d.ItemId == req.ItemId)
                    .SumAsync(d => (decimal?)(d.Quantity * d.UnitPrice)) ?? 0;

                // ===============================
                // 4️⃣ إجمالي قيمة المنصرف
                // ===============================
                var totalOutValue = await _context.StockOutDetails
                    .Where(d => d.ItemDescriptionId == req.ItemDescriptionId)
                    .SumAsync(d => (decimal?)(d.Quantity * d.AverageUnitCost)) ?? 0;

                var remainingValue = totalInValue - totalOutValue;

                if (remainingValue <= 0)
                    throw new InvalidOperationException(
                        $"لا يوجد قيمة متبقية للصنف");

                // ===============================
                // 5️⃣ المتوسط المرجح الحالي (🔥 الأساس)
                // ===============================
                var issuePrice = Math.Round(remainingValue / remainingQty, 2);

                // ===============================
                // 6️⃣ إضافة سطر الصرف
                // ===============================
                stockOut.Details.Add(new StockOutDetail
                {
                    ItemId = req.ItemId,
                    ItemDescriptionId = req.ItemDescriptionId,
                    Quantity = req.Quantity,
                    AverageUnitCost = issuePrice
                });
            }

            _context.StockOuts.Add(stockOut);
            await _context.SaveChangesAsync();
        }


        public async Task<List<StockOutDetailDto>> GetStockOutDetailsAsync()
        {
            return await (
                from sod in _context.StockOutDetails.AsNoTracking()
                join id in _context.ItemDescriptions.AsNoTracking()
                    on sod.ItemDescriptionId equals id.Id
                join i in _context.Items.AsNoTracking()
                    on id.ItemId equals i.Id
                select new StockOutDetailDto
                {
                    Id = sod.Id,
                    ItemDescriptionId = sod.ItemDescriptionId,
                    ItemName = i.ItemName,
                    ItemCode = id.ItemCode,
                    Quantity = sod.Quantity
                }
            )
            .OrderBy(x => x.ItemName)
            .ToListAsync();
        }




        public async Task<decimal> GetAvailableQuantityAsync(int itemDescriptionId)
        {
            var totalIn = await _context.PurchaseInDetails
                .Where(d => d.ItemDescriptionId == itemDescriptionId)
                .Select(d => (decimal?)d.Quantity)
                .SumAsync() ?? 0;

            var totalOut = await _context.StockOutDetails
                .Where(d => d.ItemDescriptionId == itemDescriptionId)
                .Select(d => (decimal?)d.Quantity)
                .SumAsync() ?? 0;

            return totalIn - totalOut;
        }


        public async Task<List<StockBalanceDto>> GetStockBalanceAsync()
        {
            return await _context.StockBalances
                .AsNoTracking()
                .ToListAsync();
        }



        public async Task<OperationResultDto> EditStockOutQuantityAsync(
     int stockOutDetailId,
     decimal newQuantity,
     string editedBy)
        {
            var isSuccessParam = new SqlParameter("@IsSuccess", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };

            var messageParam = new SqlParameter("@Message", SqlDbType.NVarChar, 250)
            {
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                @"EXEC sp_EditStockOutQuantity 
            @StockOutDetailId, 
            @NewQuantity, 
            @EditedBy, 
            @IsSuccess OUTPUT, 
            @Message OUTPUT",
                new SqlParameter("@StockOutDetailId", stockOutDetailId),
                new SqlParameter("@NewQuantity", newQuantity),
                new SqlParameter("@EditedBy", editedBy),
                isSuccessParam,
                messageParam
            );

            return new OperationResultDto
            {
                IsSuccess = (bool)isSuccessParam.Value,
                Message = messageParam.Value?.ToString() ?? ""
            };
        }

    }
}