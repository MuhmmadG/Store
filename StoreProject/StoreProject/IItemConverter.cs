using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace StoreProject
{
    public interface IItemConverter
    {
        public string ConvertData(List<ItemModel> data);

        public List<ItemModel> ConvertBack(string fileContet);
       
    }
}
