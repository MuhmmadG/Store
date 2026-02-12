using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.DTO
{
  
    public class ItemMovement
    {
        public DateTime Date { get; set; }
        public string MovementType { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string ItemName { get; set; } = null!;
        public string ItemCode { get; set; } = null!;
        public decimal Quantity { get; set; }
    }



}
