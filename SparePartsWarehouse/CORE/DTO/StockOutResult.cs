using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.DTO
{
    public class StockOutResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";

        public static StockOutResult Success()
            => new() { IsSuccess = true };

        public static StockOutResult Fail(string message)
            => new() { IsSuccess = false, Message = message };
    }


}
