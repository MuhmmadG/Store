using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.DTO
{
    public class StockOutRequest
    {
       
        //==================================
        public DateTime Date { get; set; }

        public int StoreDepartmentId { get; set; }
        public int FactoryDepartmentId { get; set; }
        public int? MachineId { get; set; }

        public List<StockOutRequestItem> Items { get; set; }
     = new();

    }



}
