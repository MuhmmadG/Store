using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SparePartsWarehouse.CORE.DTO;
using SparePartsWarehouse.CORE.Entities;
using SparePartsWarehouse.CORE.Interfaces;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

public class EditPurchaseViewModel : ObservableObject
{
    private readonly IPurchaseEditService _editService;
    private readonly IPurchaseDeleteService _lookupService; // لإعادة استخدام تحميل الموردين والفواتير

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

    private PurchaseItemDto _selectedItem;
    public PurchaseItemDto SelectedItem
    {
        get => _selectedItem;
        set
        {
            SetProperty(ref _selectedItem, value);
            if (value != null)
            {
                NewQuantity = value.Quantity;
                NewUnitPrice = value.UnitPrice;
            }
        }
    }

    private decimal _newQuantity;
    public decimal NewQuantity
    {
        get => _newQuantity;
        set => SetProperty(ref _newQuantity, value);
    }

    private decimal _newUnitPrice;
    public decimal NewUnitPrice
    {
        get => _newUnitPrice;
        set => SetProperty(ref _newUnitPrice, value);
    }

    private string _reason;
    public string Reason
    {
        get => _reason;
        set => SetProperty(ref _reason, value);
    }

    public IRelayCommand EditCommand { get; }
    public EditPurchaseViewModel(
    IPurchaseEditService editService,
    IPurchaseDeleteService lookupService)
    {
        _editService = editService;
        _lookupService = lookupService;

        EditCommand = new AsyncRelayCommand(EditAsync);

        _ = LoadSuppliersAsync();
    }
    private async Task EditAsync()
    {
        if (SelectedItem == null)
        {
            MessageBox.Show("اختر صنف أولاً");
            return;
        }

        var result = await _editService.EditPurchaseItemAsync(
            SelectedItem.PurchaseDetailId,
            NewQuantity,
            NewUnitPrice,
            Reason,
            "CurrentUser" // لاحقًا من Session
        );

        if (!result.IsSuccess)
        {
            MessageBox.Show(result.Message, "خطأ محاسبي", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        MessageBox.Show(result.Message, "نجاح", MessageBoxButton.OK, MessageBoxImage.Information);

        await LoadItemsAsync(SelectedInvoice.Id);
    }

    private async Task LoadInvoicesAsync(int supplierId)
    {
        Invoices.Clear();
        var invoices = await _lookupService.GetInvoicesBySupplierAsync(supplierId);
        foreach (var invoice in invoices)
        {
            Invoices.Add(invoice);
        }
    }

    private async Task LoadItemsAsync(int invoiceId)
    {
        Items.Clear();
        var items = await _lookupService.GetInvoiceItemsAsync(invoiceId);
        foreach (var item in items)
        {
            Items.Add(item);
        }
    }

    // Add this method to the EditPurchaseViewModel class to fix CS0103
    private async Task LoadSuppliersAsync()
    {
        Suppliers.Clear();
        var suppliers = await _lookupService.GetSuppliersAsync();
        foreach (var supplier in suppliers)
        {
            Suppliers.Add(supplier);
        }
    }
}
