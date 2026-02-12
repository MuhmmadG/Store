using SparePartsWarehouse.CORE.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.Interfaces
{
    public interface IPurchaseDeleteService
    {
        Task<List<SupplierDto>> GetSuppliersAsync();
        Task<List<PurchaseInvoiceDto>> GetInvoicesBySupplierAsync(int supplierId);
        Task<List<PurchaseItemDto>> GetInvoiceItemsAsync(int purchaseInvoiceId);

        Task<bool> CanDeleteInvoiceAsync(int invoiceId);
        Task DeleteInvoiceAsync(int invoiceId);
        Task DeleteItemAsync(int purchaseDetailId);
      //  Task DeleteItemsAsync(List<int> purchaseDetailIds);
        Task<List<DeleteResultDto>> DeleteItemsAsync(List<int> ids);
    }

}
