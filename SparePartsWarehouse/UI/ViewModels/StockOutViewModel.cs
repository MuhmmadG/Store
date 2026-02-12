using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SparePartsWarehouse.CORE.DTO;
using SparePartsWarehouse.CORE.Entities;
using SparePartsWarehouse.CORE.Interfaces;
using SparePartsWarehouse.DATA.StockOutService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SparePartsWarehouse.UI.ViewModels
{
    public class StockOutViewModel : ObservableObject
    {
        private readonly IStockOutService _stockOutService;
        public ItemSelectionViewModel ItemSelector { get; }
      
        public ObservableCollection<StockOutItemVM> Items { get; } = new();      
        public FactoryMachineSelectionViewModel FactorySelector { get; }

        private DateTime _date = DateTime.Today;
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }
        private string _inputQuantityText = "";
        public string InputQuantityText
        {
            get => _inputQuantityText;
            set
            {
                _inputQuantityText = value;

                if (decimal.TryParse(value.Replace(',', '.'),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out var result))
                {
                    InputQuantity = result;
                }

                OnPropertyChanged();
            }
        }

        private decimal _inputQuantity;
        public decimal InputQuantity
        {
            get => _inputQuantity;
            set =>SetProperty(ref _inputQuantity, value);
        }
        private string _receiverName;
        public string ReceiverName
        {
            get => _receiverName;
            set => SetProperty(ref _receiverName, value);
        }

        public ICommand AddItemCommand { get; }
        public ICommand SaveCommand { get; }

        public StockOutViewModel(
            IStockOutService stockOutService,
            ILookupService lookup)
        {
            FactorySelector = new FactoryMachineSelectionViewModel(lookup);
            ItemSelector = new ItemSelectionViewModel(lookup);
            AddItemCommand = new AsyncRelayCommand(AddItemAsync);
            SaveCommand = new AsyncRelayCommand(SaveAsync);
            _stockOutService = stockOutService;
            //_ = InitAsync();
        }
        public async Task InitAsync()
        {
            await ItemSelector.InitAsync();
            await FactorySelector.InitAsync();
        }
       
        private async Task AddItemAsync()
        {
            if (ItemSelector.SelectedItem == null ||
                ItemSelector.SelectedItemDescription == null ||
                InputQuantity <= 0)
            {
                MessageBox.Show("من فضلك أكمل البيانات");
                return;
            }

            var itemDescriptionId = ItemSelector.SelectedItemDescription.Id;

            // 🔹 التحقق من الكمية المتاحة
            var availableQty = await _stockOutService
                .GetAvailableQuantityAsync(itemDescriptionId);

            if (InputQuantity > availableQty)
            {
                MessageBox.Show(
                    $"الكمية غير متاحة للصنف، المتاح فقط: {availableQty}",
                    "تحذير",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            // ✅ الإضافة للـ DataGrid
            Items.Add(new StockOutItemVM
            {
                ItemId = ItemSelector.SelectedItem.Id,
                ItemDescriptionId = itemDescriptionId,
                ItemName = ItemSelector.SelectedItem.ItemName,
                ItemCode = ItemSelector.SelectedItemDescription.ItemCode,
                Quantity = InputQuantity
            });

            InputQuantity = 0;
        }

        public async Task SaveAsync()
        {
            try
            {
                if (!Items.Any())
                {
                    MessageBox.Show("لا يوجد أصناف للصرف");
                    return;
                }

                var department = FactorySelector.SelectedFactoryDepartment;
                var machine = FactorySelector.SelectedMachine;

                if (department == null || machine == null)
                {
                    MessageBox.Show("من فضلك اختر القسم والماكينة");
                    return;
                }

                // تحويل الأصناف إلى DTO
                var request = Items.Select(i => new StockOutRequestDto
                {
                    ItemDescriptionId = i.ItemDescriptionId,
                    ItemId = i.ItemId,
                    Quantity = i.Quantity
                }).ToList();

                // استدعاء الخدمة مع القيم الصحيحة للقسم والماكينة
                await _stockOutService.SaveStockOutAsync(
                     ReceiverName,
                     Date,
                    request,
                    department.Id,
                    ItemSelector.SelectedStoreDepartment.Id,
                    machine.Id
                );

                MessageBox.Show("تم حفظ إذن الصرف بنجاح ✅");

                // مسح الأصناف بعد الحفظ
                Items.Clear();
                InputQuantity = 0;
            }
            catch (InvalidOperationException ex)
            {
                // هذا يلتقط رسالة الكمية غير المتاحة
                MessageBox.Show(ex.Message, "خطأ في الصرف", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                // أي خطأ آخر
                MessageBox.Show($"حدث خطأ أثناء الحفظ: {ex.Message}", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




    }


}
