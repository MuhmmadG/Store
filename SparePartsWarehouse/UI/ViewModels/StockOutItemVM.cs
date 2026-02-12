using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.UI.ViewModels
{
    public class StockOutItemVM : ObservableObject
    {
        // 🔹 IDs (للـ Save)
        public int ItemId { get; set; }
        public int ItemDescriptionId { get; set; }

        // 🔹 للعرض فقط
        public string StoreDepartmentName { get; set; } = "";
        public string FactoryDepartmentName { get; set; } = "";
        public string? MachineName { get; set; }

        public string ItemName { get; set; } = "";
        public string ItemCode { get; set; } = "";

        private decimal _quantity;
        public decimal Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }
    }


}
