using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.DTO
{
    public class PurchaseInvoiceDto
    {
        public int Id { get; set; }
        public string NumberInvoice { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string SupplierName { get; set; }
        // للعرض فقط
        public string DisplayText =>
            $"{NumberInvoice} - {Date:yyyy/MM/dd}";

        public string DisplayName
      => $"{NumberInvoice} {SupplierName}";
    }

}
