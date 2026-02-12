using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.Entities
{
    public class Supplier
    {
        public int Id { get; set; }
        public string SupplierName { get; set; } = null!;
        public bool IsActive { get; set; } = true;
    }

}
