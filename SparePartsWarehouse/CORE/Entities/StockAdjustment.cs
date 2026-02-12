using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.Entities
{
    public class StockAdjustment
    {
        public int Id { get; set; }

        public int ItemDescriptionId { get; set; }

        public decimal Quantity { get; set; }   // + أو -
        public decimal UnitCost { get; set; }

        public string Reason { get; set; }

        public string ReferenceType { get; set; } // Purchase / Issue
        public int ReferenceId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation (اختياري لكن مهم)
        public ItemDescription ItemDescription { get; set; }
    }

}
