using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SparePartsWarehouse.CORE.Entities
{
    [Table("DocumentEditsLogs")]
    public class DocumentEditsLog
    {
        public int Id { get; set; }

        public string DocumentType { get; set; }   // PurchaseIn / StockOut
        public int DocumentId { get; set; }

        public string FieldName { get; set; }

        public string OldValue { get; set; }
        public string NewValue { get; set; }

        public string EditedBy { get; set; }

        public DateTime EditedAt { get; set; } = DateTime.Now;
    }

}
