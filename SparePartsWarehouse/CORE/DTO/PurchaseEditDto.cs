using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.DTO
{
    public class PurchaseEditDto
    {
        public int PurchaseDetailId { get; set; }

        public decimal NewQuantity { get; set; }
        public decimal NewUnitPrice { get; set; }

        public string Reason { get; set; }
        public string EditedBy { get; set; }
    }

}
