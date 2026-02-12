using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.Entities
{
    public class StockOutDetail
    {
        public int Id { get; set; }

        public int StockOutId { get; set; }
        public StockOut StockOut { get; set; } = null!;

        public int ItemDescriptionId { get; set; }
        public ItemDescription ItemDescription { get; set; } = null!;

        public int ItemId { get; set; }
        public Item Item { get; set; } = null!;

      

        public decimal Quantity { get; set; }

        // 🔥 مهم جدًا
        public decimal AverageUnitCost { get; set; }
        public decimal TotalCost => Quantity * AverageUnitCost;
    }



}
