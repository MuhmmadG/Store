using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.DTO
{
    public class StockOutDetailRowVM : ObservableObject
    {
        public int Id { get; set; }                 // StockOutDetailId
        public string ItemName { get; set; }
        public string ItemCode { get; set; }

        public decimal Quantity { get; set; }       // الكمية الحالية
    }
}
