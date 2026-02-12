using SparePartsWarehouse.CORE.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.Interfaces
{

    public interface IStockOutService
    {
        Task SaveStockOutAsync(
            string receiverName,
            DateTime date,
            List<StockOutRequestDto> items,
            int factoryDepartmentId,
            int StoreDepartmentId,
            int machineId);
        Task<decimal> GetAvailableQuantityAsync(int itemDescriptionId);
        Task<List<StockBalanceDto>> GetStockBalanceAsync();
        Task<OperationResultDto> EditStockOutQuantityAsync(
       int stockOutDetailId,
       decimal newQuantity,
       string editedBy);
        Task<List<StockOutDetailDto>> GetStockOutDetailsAsync();
    }

}
