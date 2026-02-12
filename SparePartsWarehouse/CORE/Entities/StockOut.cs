using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.Entities
{
    public class StockOut
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public string? IssuerName { get; set; }
        public string? ReceiverName { get; set; }


        // من أين (المخزن)
        public int StoreDepartmentId { get; set; }
        public StoreDepartment StoreDepartment { get; set; } = null!;

        // إلى أين (الإنتاج)
        public int FactoryDepartmentId { get; set; }
        public FactoryDepartment FactoryDepartment { get; set; } = null!;

        // ماكينة (اختياري)
        public int? MachineId { get; set; }
        public Machine? Machine { get; set; }


        public ICollection<StockOutDetail> Details { get; set; }
            = new List<StockOutDetail>();
    }



}
