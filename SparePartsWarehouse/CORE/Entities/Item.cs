using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.Entities
{
    public class Item
    {
        public int Id { get; set; }
       
        public string ItemName { get; set; } = null!;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public string? Specification { get; set; } // مقاس / موديل
        public string Unit { get; set; }
        public decimal ReorderLevel { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<PurchaseInDetail> PurchaseInDetails { get; set; }
            = new List<PurchaseInDetail>();

        public ICollection<StockOutDetail> StockOutDetails { get; set; }
            = new List<StockOutDetail>();

        // ⭐ الربط الجديد
        public ICollection<ItemDescription> Descriptions { get; set; }
            = new List<ItemDescription>();

    }

}
