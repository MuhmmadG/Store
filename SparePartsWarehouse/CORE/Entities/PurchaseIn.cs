using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.Entities
{
    public class PurchaseIn
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string NumberInvoice { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;

        public string PurchasedBy { get; set; } = null!;   // المندوب / المشتري
        public string ReceivedBy { get; set; } = null!;    // أمين المخزن

        public string? Reference { get; set; }              // رقم الفاتورة

        public ICollection<PurchaseInDetail> Details { get; set; }
            = new List<PurchaseInDetail>();
    }

}
