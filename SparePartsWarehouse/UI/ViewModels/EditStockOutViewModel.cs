using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SparePartsWarehouse.CORE.DTO;
using SparePartsWarehouse.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SparePartsWarehouse.UI.ViewModels
{
    public class EditStockOutViewModel : ObservableObject
    {
        private readonly IStockOutService _stockOutService;

        public EditStockOutViewModel(IStockOutService stockOutService)
        {
            _stockOutService = stockOutService;
            EditCommand = new AsyncRelayCommand(EditAsync, CanEdit);
             _=LoadAsync();
        }

        // ================= DataGrid =================
        public ObservableCollection<StockOutDetailRowVM> Items { get; } = new();

        private StockOutDetailRowVM _selectedItem;
        public StockOutDetailRowVM SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (SetProperty(ref _selectedItem, value))
                {
                    LoadSelectedItem();
                    EditCommand.NotifyCanExecuteChanged();
                }
            }
        }

        // ================= Fields =================
        private decimal _oldQuantity;
        public decimal OldQuantity
        {
            get => _oldQuantity;
            set => SetProperty(ref _oldQuantity, value);
        }

        private decimal _newQuantity;
        public decimal NewQuantity
        {
            get => _newQuantity;
            set => SetProperty(ref _newQuantity, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public IAsyncRelayCommand EditCommand { get; }

        // ================= Methods =================
        private async Task LoadAsync()
        {
            var data = await _stockOutService.GetStockOutDetailsAsync();

            Items.Clear();
            foreach (var d in data)
            {
                Items.Add(new StockOutDetailRowVM
                {
                    Id = d.Id,
                    ItemName = d.ItemName,
                    ItemCode = d.ItemCode,
                    Quantity = d.Quantity
                });
            }
        }

        private void LoadSelectedItem()
        {
            if (SelectedItem == null) return;

            OldQuantity = SelectedItem.Quantity;
            NewQuantity = SelectedItem.Quantity;
            Message = string.Empty;
        }

        private bool CanEdit()
            => SelectedItem != null && NewQuantity >= 0;

        private async Task EditAsync()
        {
            if (NewQuantity > OldQuantity)
            {
                Message = "❌ لا يمكن زيادة الكمية عن المنصرف";
                return;
            }

            var result = await _stockOutService.EditStockOutQuantityAsync(
                SelectedItem.Id,
                NewQuantity,
                "Ahmed"
            );

            Message = result.Message;

            if (result.IsSuccess)
            {
                SelectedItem.Quantity = NewQuantity;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
    }


}
