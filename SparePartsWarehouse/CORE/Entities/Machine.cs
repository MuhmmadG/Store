using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.Entities
{
    public class Machine
    {
        public int Id { get; set; }
        public string MachineName { get; set; } = "";

        public int FactoryDepartmentId { get; set; }
        public FactoryDepartment FactoryDepartment { get; set; } = null!;
    }


}
