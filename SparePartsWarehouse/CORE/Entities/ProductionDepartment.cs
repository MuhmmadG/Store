using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.Entities
{
    public class ProductionDepartment
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public bool IsActive { get; set; } = true;

        public ICollection<Machine> Machines { get; set; } = new List<Machine>();
    }

}
