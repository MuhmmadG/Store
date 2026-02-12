using SparePartsWarehouse.CORE;
using SparePartsWarehouse.CORE.Entities;
using SparePartsWarehouse.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SparePartsWarehouse.UI.ViewModels
{
    public class PurchaseInDetailVM : ObservableObject
    {
        public Item? Item { get; set; }
        public string ItemName { get; set; } = "";
        public string ItemCode { get; set; } = "";

        private decimal _quantity;
        public decimal Quantity
        {
            get => _quantity;
            set
            {
                SetProperty(ref _quantity, value);
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private decimal _unitPrice;
        public decimal UnitPrice
        {
            get => _unitPrice;
            set
            {
                SetProperty(ref _unitPrice, value);
                OnPropertyChanged(nameof(TotalPrice));
            }
        }
        public StoreDepartment? RowDepartment { get; set; }
        public string RowDepartmentName { get; set; } = "";

        public Category? RowCategory { get; set; }
        public string RowCategoryName { get; set; } = "";
        public decimal TotalPrice => Quantity * UnitPrice;
    }


}
