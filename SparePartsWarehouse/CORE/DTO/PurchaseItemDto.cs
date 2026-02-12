using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.DTO
{
    public class PurchaseItemDto : ObservableObject
    {
        public int PurchaseDetailId { get; set; }

        public string ItemName { get; set; } = string.Empty;
        public string ItemCode { get; set; } = string.Empty;

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // ✔ اختيار للحذف
        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
    }

}
