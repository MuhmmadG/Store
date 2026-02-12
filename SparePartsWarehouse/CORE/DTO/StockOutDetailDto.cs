using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.DTO
{
    public class StockOutDetailDto
    {
        public int Id { get; set; }                  // StockOutDetailId

        public int StockOutId { get; set; }           // رقم إذن الصرف (لو احتجته لاحقًا)

        public int ItemId { get; set; }

        public int ItemDescriptionId { get; set; }

        public string ItemName { get; set; }          // اسم الصنف

        public string ItemCode { get; set; }          // كود / وصف الصنف

        public decimal Quantity { get; set; }         // الكمية المنصرفة الحالية
    }

}
