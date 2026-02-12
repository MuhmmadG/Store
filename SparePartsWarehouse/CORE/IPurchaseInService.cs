using SparePartsWarehouse.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE
{
    public interface IPurchaseInService
    {
        Task SaveAsync(PurchaseIn purchaseIn);
    }

}
