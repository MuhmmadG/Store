using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.Entities
{
    public class ItemDescription
    {
        public int Id { get; set; }

        public string ItemCode { get; set; } = "";

        public int ItemId { get; set; }
        public Item Item { get; set; } = null!;
    }

}
