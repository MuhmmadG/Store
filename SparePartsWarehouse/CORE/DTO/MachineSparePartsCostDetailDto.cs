using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.DTO
{
    public class MachineSparePartsCostDetailDto
    {
        public string FactoryDepartmentName { get; set; } = "";
        public string MachineName { get; set; } = "";

        public string CategoryName { get; set; } = "";
        public string ItemName { get; set; } = "";
        public string ItemCode { get; set; } = "";

        public decimal TotalQuantity { get; set; }
        public decimal TotalCost { get; set; }

    }
}