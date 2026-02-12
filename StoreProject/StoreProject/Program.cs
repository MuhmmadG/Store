using System.Text;
using System.Xml.Linq;

namespace StoreProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                PrintMenu();
                var chooes = Console.ReadLine()?.Trim();
                ItemManger oItemManger = new ItemManger(new ItemConvertJson());
                switch(chooes)
                {
                    case "1":
                        oItemManger.AddItem();
                        break;  
                    case "2":
                        oItemManger.ShowAllItems();
                        break;  
                    case "0":
                        return;
                        default:
                        Console.WriteLine("Invalid Choice. Try again....");
                        break;
                }
            }
        }
        static void PrintMenu()
        {
            Console.WriteLine("=========================");
            Console.WriteLine("1- Add item (Append & Save) ");
            Console.WriteLine("2- Show All Items (read from file) ");
            Console.WriteLine("0- Exit ");
            Console.WriteLine("=========================");
            Console.WriteLine("Chooes: ");

        }
      
    }
}
