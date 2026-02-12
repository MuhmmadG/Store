using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.Entities
{
    public class PurchaseInDetail
    {
        public int Id { get; set; }

        public int PurchaseInId { get; set; }
        public PurchaseIn PurchaseIn { get; set; } = null!;

        public int ItemId { get; set; }
        public Item Item { get; set; } = null!;


        public int ItemDescriptionId { get; set; }
        public ItemDescription ItemDescription { get; set; } = null!;

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }   // Quantity * UnitPrice
                                                  // 🔥 الجديد (أساسي جدًا)
        public decimal AverageUnitCost { get; set; }
    }

}
