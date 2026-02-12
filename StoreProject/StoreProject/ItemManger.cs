using System;
using System.Collections.Generic;
using System.Text;

namespace StoreProject
{
    public class ItemManger
    {
        IItemConverter _converter;
        public ItemManger(IItemConverter converter) 
        {
            _converter = converter;
        }
        const string FileName = "data.json";
        public void AddItem()
        {
            ItemModel itemModel = new ItemModel();
            Console.WriteLine("please enetr ID");
            itemModel.Id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("please enetr Name");
            itemModel.Name = Console.ReadLine();

            Console.WriteLine("please enetr Price");
            itemModel.Price = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("please enetr Quanity");
            itemModel.Quantity = Convert.ToInt32(Console.ReadLine());

          
            var itemString = _converter.ConvertData(new List<ItemModel> { itemModel });

            FileHelper.Append("data.txt", itemString);
        }
        public void ShowAllItems()
        {
            Console.WriteLine("========================");
            Console.WriteLine("Items In Database");
            Console.WriteLine("========================");
            var items = new List<ItemModel>();
            items = GetData();
            foreach (var item in items)
            {
                Console.WriteLine($"id : {item.Id}");
                Console.WriteLine($"Name : {item.Name}");
                Console.WriteLine($"Price : {item.Price}");
                Console.WriteLine($"Quantity : {item.Quantity}");
                Console.WriteLine();
            }
        }
        List<ItemModel> GetData()
        {
            
            var items = _converter.ConvertBack(FileHelper.ReadAll(FileName));
            return items;
        }
    }
}
