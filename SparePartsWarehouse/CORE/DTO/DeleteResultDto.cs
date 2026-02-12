using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.DTO
{
    public class DeleteResultDto
    {
        public int PurchaseDetailId { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }


}
