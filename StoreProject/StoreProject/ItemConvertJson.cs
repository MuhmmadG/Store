using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StoreProject
{
    public class ItemConvertJson : IItemConverter
    {
        public string ConvertData(List<ItemModel> data)
        {
            return JsonSerializer.Serialize(data);
        }
        public List<ItemModel> ConvertBack(string fileContet)
        {
            return JsonSerializer.Deserialize<List<ItemModel>>(fileContet);
        }

    }
}
