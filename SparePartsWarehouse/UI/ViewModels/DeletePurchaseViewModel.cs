using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SparePartsWarehouse.CORE.DTO;
using SparePartsWarehouse.CORE.Interfaces;
using SparePartsWarehouse.DATA.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace SparePartsWarehouse.UI.ViewModels
{
    public class DeletePurchaseViewModel : ObservableObject
    {
        private readonly IPurchaseDeleteService _service;

        public ObservableCollection<SupplierDto> Suppliers { get; } = new();
        public ObservableCollection<PurchaseInvoiceDto> Invoices { get; } = new();
       
        public ObservableCollection<PurchaseItemDto> Items { get; } = new();
    

        private SupplierDto _selectedSupplier;
        public SupplierDto SelectedSupplier
        {
            get => _selectedSupplier;
            set
            {
                SetProperty(ref _selectedSupplier, value);

                if (value != null)
                    _ = LoadInvoicesAsync(value.Id);
            }
        }

        private PurchaseInvoiceDto _selectedInvoice;
        public PurchaseInvoiceDto SelectedInvoice
        {
            get => _selectedInvoice;
            set
            {
                SetProperty(ref _selectedInvoice, value);

                if (value != null)
                    _ = LoadItemsAsync(value.Id);
            }
        }


        public IRelayCommand DeleteSelectedItemsCommand { get; }
        public IRelayCommand DeleteInvoiceCommand { get; }

        public DeletePurchaseViewModel(IPurchaseDeleteService service)
        {
            _service = service;

            DeleteSelectedItemsCommand = new AsyncRelayCommand(DeleteSelectedItemsAsync);
            DeleteInvoiceCommand = new AsyncRelayCommand(DeleteInvoice);
            // 🔹 تحميل الموردين عند بداية النافذة
            _ = LoadSuppliersAsync();
        }

        private async Task DeleteSelectedItems()
        {
            var selected = Items.Where(x => x.IsSelected).ToList();

            foreach (var item in selected)
                await _service.DeleteItemAsync(item.PurchaseDetailId);

            MessageBox.Show("تم حذف الأصناف المحددة");
        }
        private async Task DeleteSelectedItemsAsync()
        {
            if (SelectedInvoice == null)
            {
                MessageBox.Show("اختر فاتورة أولاً");
                return;
            }

            var selectedIds = Items
                .Where(i => i.IsSelected)
                .Select(i => i.PurchaseDetailId)
                .ToList();

            if (!selectedIds.Any())
            {
                MessageBox.Show("لم يتم اختيار أي صنف");
                return;
            }

            // 🔹 استدعاء الـ Stored Procedure
            var result = await _service.DeleteItemsAsync(selectedIds);

            // 🔹 الأصناف التي فشل حذفها
            var failed = result.Where(r => !r.IsSuccess).ToList();

            if (failed.Any())
            {
                var msg = string.Join("\n",
                    failed.Select(f => $"• {f.Message}"));

                MessageBox.Show(
                    $"تم حذف بعض الأصناف، والبعض لم يُحذف:\n\n{msg}",
                    "تنبيه محاسبي",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show(
                    "تم حذف الأصناف المحددة بنجاح",
                    "نجاح",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            // 🔹 إعادة تحميل الأصناف بعد الحذف
            await LoadInvoiceItemsAsync(SelectedInvoice.Id);
        }


        private async Task DeleteInvoice()
        {
            if (!await _service.CanDeleteInvoiceAsync(SelectedInvoice.Id))
            {
                MessageBox.Show("لا يمكن حذف الفاتورة لوجود صرف");
                return;
            }

            await _service.DeleteInvoiceAsync(SelectedInvoice.Id);
            MessageBox.Show("تم حذف الفاتورة بالكامل");
        }
        private async Task LoadSuppliersAsync()
        {
            Suppliers.Clear();

            var suppliers = await _service.GetSuppliersAsync();
            foreach (var s in suppliers)
                Suppliers.Add(s);
        }
        private async Task LoadItemsAsync(int invoiceId)
        {
            Items.Clear();

            var items = await _service.GetInvoiceItemsAsync(invoiceId);
            foreach (var item in items)
                Items.Add(item);
        }
        private async Task LoadInvoicesAsync(int supplierId)
        {
            Invoices.Clear();
            Items.Clear();

            var invoices = await _service.GetInvoicesBySupplierAsync(supplierId);
            foreach (var inv in invoices)
                Invoices.Add(inv);
        }
        private async Task LoadInvoiceItemsAsync(int invoiceId)
        {
            Items.Clear();

            var items = await _service
                .GetInvoiceItemsAsync(invoiceId);

            foreach (var item in items)
                Items.Add(item);
        }

    }

}
