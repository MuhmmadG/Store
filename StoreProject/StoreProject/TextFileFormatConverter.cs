using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace StoreProject
{
    public class TextFileFormatConverter : IItemConverter
    {
        public string ConvertData(List<ItemModel> item)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (ItemModel data in item)
            {
                stringBuilder.AppendLine($"Id#{data.Id}");
                stringBuilder.AppendLine($"Name#{data.Name}");
                stringBuilder.AppendLine($"Price#{data.Price}");
                stringBuilder.AppendLine($"Quantity#{data.Quantity}");
                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString();
        }
        public List<ItemModel> ConvertBack(string fileContet)
        {
            var items = new List<ItemModel>();

            if (string.IsNullOrWhiteSpace(fileContet))
                return items;

            var current = new ItemModel();
            bool hasAnyField = false;
            using var reader = new StringReader(fileContet);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line))
                {
                    if (hasAnyField)
                    {
                        items.Add(current);
                        current = new ItemModel();
                        hasAnyField = false;
                    }
                    continue;
                }
                int hashIndex = line.IndexOf('#');
                if (hashIndex <= 0 || hashIndex == line.Length - 1)
                    continue;
                string key = line.Substring(0, hashIndex).Trim();
                string value = line.Substring(hashIndex + 1);
                switch (key)
                {
                    case "Id":
                        if (int.TryParse(value, out int id))
                        {
                            current.Id = id;
                            hasAnyField = true;
                        }
                        break;
                    case "Name":
                        current.Name = value;
                        hasAnyField = true;
                        break;
                    case "Price":
                        if(decimal.TryParse(value, NumberStyles.Any , CultureInfo.InvariantCulture
                                    , out decimal price))
                        {
                            current.Price = price;
                            hasAnyField = true;
                        }
                        break;
                    case "Quantity":
                        if(int.TryParse(value, out var qty))
                        {
                            current.Quantity = qty;
                            hasAnyField = true;
                        }
                        break;

                }
            }

            return items;
        }
    }
}
