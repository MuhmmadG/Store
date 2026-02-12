using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;


        public int StoreDepartmentId { get; set; }
        public StoreDepartment StoreDepartment { get; set; } = null!;

        public ICollection<Item> Items { get; set; }
            = new List<Item>();

        

     

        public bool IsActive { get; set; } = true;
    }

}
