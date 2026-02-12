using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.DTO
{
    public class StockOutRequestItem
    {
        public int ItemId { get; set; }
        public int ItemDescriptionId { get; set; }
        public decimal Quantity { get; set; }
    }
}
