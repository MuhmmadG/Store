using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.DTO
{
    public class StockBalanceDto
    {
        public string CategoryName { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }

        public decimal TotalInQty { get; set; }
        public decimal TotalOutQty { get; set; }
        public decimal BalanceQty { get; set; }
    }

}
