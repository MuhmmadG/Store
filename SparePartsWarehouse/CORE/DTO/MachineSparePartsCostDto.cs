using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.DTO
{
    public class MachineSparePartsCostDto
    {
        public string FactoryDepartmentName { get; set; } = "";
        public string MachineName { get; set; } = "";
        public decimal TotalQuantity { get; set; }
        public decimal TotalCost { get; set; }
    }

}
