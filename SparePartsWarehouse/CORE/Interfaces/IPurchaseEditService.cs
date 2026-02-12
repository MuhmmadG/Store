using SparePartsWarehouse.CORE.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.Interfaces
{
    public interface IPurchaseEditService
    {
        Task<OperationResultDto> EditPurchaseItemAsync(
            int purchaseDetailId,
            decimal newQuantity,
            decimal newUnitPrice,
            string reason,
            string editedBy
        );
    }

}
