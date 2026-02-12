using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.Entities
{
    public class StoreDepartment
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public bool IsActive { get; set; } = true;

        public ICollection<Category> Categories { get; set; }
            = new List<Category>();
    }

}
