using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using SparePartsWarehouse.CORE;
using SparePartsWarehouse.CORE.Entities;
using SparePartsWarehouse.CORE.Interfaces;
using SparePartsWarehouse.DATA.Context;
using SparePartsWarehouse.DATA.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace SparePartsWarehouse.UI.ViewModels
{
    public class PurchaseInViewModel : ObservableObject
    {

        private readonly ILookupService _lookup;
        private readonly IPurchaseInService _service;
        private readonly ISupplierService _supplierService;
        private readonly IDepartmentService _departmentService;
        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _context;


        private string _itemName = "";
        public string ItemName
        {
            get => _itemName;
            set
            {
                if (!SetProperty(ref _itemName, value))
                    return;
                ItemCode = ""; // ⭐ مهم
                LoadItemDescriptions();
                AddDetailCommand.NotifyCanExecuteChanged();
            }
        }


        private string _itemCode = "";
        public string ItemCode
        {
            get => _itemCode;
            set
            {
                SetProperty(ref _itemCode, value);
                AddDetailCommand.NotifyCanExecuteChanged();
            }
        }


        private string? _selectedDescription;
        public string? SelectedDescription
        {
            get => _selectedDescription;
            set
            {
                if (SetProperty(ref _selectedDescription, value))
                {
                    ItemCode = value ?? "";
                }
            }
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
        private string _inputUnitPriceText = "";
        public string InputUnitPriceText
        {
            get => _inputUnitPriceText;
            set
            {
                _inputUnitPriceText = value;

                if (decimal.TryParse(value.Replace(',', '.'),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out var result))
                {
                    InputUnitPrice = result;
                }

                OnPropertyChanged();
            }
        }


        private decimal _inputQuantity;
        public decimal InputQuantity
        {
            get => _inputQuantity;
            set
            {
                SetProperty(ref _inputQuantity, value);
                AddDetailCommand.NotifyCanExecuteChanged();
                OnPropertyChanged(nameof(InputTotal));
            }
        }

        private decimal _inputUnitPrice;
        public decimal InputUnitPrice
        {
            get => _inputUnitPrice;
            set
            {
                SetProperty(ref _inputUnitPrice, value);
                AddDetailCommand.NotifyCanExecuteChanged();
                OnPropertyChanged(nameof(InputTotal));
            }
        }
        private string _supplierName = "";
        public string SupplierName
        {
            get => _supplierName;
            set
            {
                SetProperty(ref _supplierName, value);

                // لو الاسم موجود → اربطه تلقائي
                SelectedSupplier = Suppliers
                    .FirstOrDefault(s => s.SupplierName == value);

                SaveCommand.NotifyCanExecuteChanged();
            }
        }
        private string _departmentName = "";
        public string DepartmentName
        {
            get => _departmentName;
            set
            {
                SetProperty(ref _departmentName, value);

                SelectedDepartment = StoreDepartments
                    .FirstOrDefault(d => d.Name == value);
            }
        }


        private Supplier? _selectedSupplier;
        public Supplier? SelectedSupplier
        {
            get => _selectedSupplier;
            set
            {
                SetProperty(ref _selectedSupplier, value);
                SaveCommand.NotifyCanExecuteChanged();
            }
        }
        private StoreDepartment? _selectedDepartment;
        public StoreDepartment? SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                if (SetProperty(ref _selectedDepartment, value))
                {
                    LoadCategoriesAsync();
                }
            }
        }


        public decimal InputTotal => InputQuantity * InputUnitPrice;


        public PurchaseInViewModel(ILookupService lookup,
                                   IPurchaseInService service,
                                   ISupplierService supplierService,
                                   IDepartmentService departmentService,
                                   ICategoryService categoryService,
                                   AppDbContext context)
        {
            _lookup = lookup;
            _service = service;
            _supplierService = supplierService;
            _departmentService = departmentService;
            _categoryService = categoryService;
            _context = context;
            Details = new ObservableCollection<PurchaseInDetailVM>();
            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            AddDetailCommand = new RelayCommand(AddDetail, CanAddDetail);
            Details.CollectionChanged += (_, __) =>
            {
                SaveCommand.NotifyCanExecuteChanged();
            };

            LoadAsync();
        }

        // ================= Header =================

        public string PurchasedBy { get; set; } = "";
        public string ReceivedBy { get; set; } = "";
        private DateTime _date = DateTime.Today;
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value); // أو OnPropertyChanged
        }

        public string NumberInvoice { get; set; } = "";

        // ================= Lookups =================
        public List<Supplier> Suppliers { get; set; } = new();
        public ObservableCollection<StoreDepartment> StoreDepartments { get; } = new();
        public List<Category> Categories { get; set; } = new();
        public ObservableCollection<string> ItemNames { get; } = new();
        private ObservableCollection<string> _itemDescriptions = new();
        public ObservableCollection<string> ItemDescriptions
        {
            get => _itemDescriptions;
            set => SetProperty(ref _itemDescriptions, value);
        }
        public List<Item> Items { get; set; } = new();
        public Category? SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                SetProperty(ref _selectedCategory, value);
                LoadItemsAsync();
            }
        }
        public string CategoryName { get; set; } = "";
        private Category? _selectedCategory;


        private Item? _selectedItem;
        public Item? SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (!SetProperty(ref _selectedItem, value))
                    return;

                if (value == null)
                    return;

                // تحديث الاسم
                _itemName = value.ItemName;
                OnPropertyChanged(nameof(ItemName));

                // ⭐ تحميل الأوصاف هنا
                LoadItemDescriptions();

                // مسح الوصف الحالي
                ItemCode = "";
            }
        }
        // ================= Details =================
        public ObservableCollection<PurchaseInDetailVM> Details { get; }
        public decimal TotalInvoice => Details.Sum(d => d.TotalPrice);

        // ================= Commands =================
        public IAsyncRelayCommand SaveCommand { get; }
        public IRelayCommand AddDetailCommand { get; }
        // ================= Logic =================
        private async Task LoadAsync()

        {
            Suppliers = await _lookup.GetSuppliersAsync();

            StoreDepartments.Clear();
            foreach (var d in await _lookup.GetDepartmentsAsync())
                StoreDepartments.Add(d);

            OnPropertyChanged(nameof(Suppliers));
            OnPropertyChanged(nameof(StoreDepartments));
        }
        private async void LoadCategoriesAsync()
        {
            if (SelectedDepartment == null) return;

            Categories = await _lookup
                .GetCategoriesByDepartmentAsync(SelectedDepartment.Id);

            OnPropertyChanged(nameof(Categories));
        }

        private async void LoadItemsAsync()
        {
            if (SelectedCategory == null) return;

            Items = await _lookup.GetItemsByCategoryAsync(SelectedCategory.Id);
            OnPropertyChanged(nameof(Items));

            LoadItemNames();
        }
        private void LoadItemNames()
        {
            ItemNames.Clear();

            var names = Items
                .Where(i => !string.IsNullOrWhiteSpace(i.ItemName))
                .Select(i => i.ItemName)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(n => n);

            foreach (var name in names)
                ItemNames.Add(name);
        }
        public async void AddDetail(decimal qty, decimal price)
        {
            await EnsureDepartmentAsync();
            await EnsureCategoryAsync();
            if (SelectedItem == null) return;

            Details.Add(new PurchaseInDetailVM
            {
                Item = SelectedItem,     // قد يكون null
                ItemName = ItemName,     // الاسم
                ItemCode = ItemCode,     // الوصف
                Quantity = InputQuantity,
                UnitPrice = InputUnitPrice
            });

            OnPropertyChanged(nameof(TotalInvoice));
        }
        private async Task SaveAsync()
        {
            if (!Details.Any())
            {
                MessageBox.Show("لا يوجد أصناف في الفاتورة");
                return;
            }

            var supplier = SelectedSupplier
                ?? await _supplierService.CreateSupplierIfNotExistsAsync(SupplierName);

            var purchaseDetails = new List<PurchaseInDetail>();

            foreach (var d in Details)
            {
                var rowDept = d.RowDepartment
                    ?? await _departmentService.CreateIfNotExistsAsync(d.RowDepartmentName);

                var rowCat = d.RowCategory
                    ?? await _categoryService.CreateIfNotExistsAsync(d.RowCategoryName, rowDept.Id);

                var item = await _lookup.CreateItemIfNotExistsAsync(
                    d.ItemName,
                    rowCat.Id,
                    d.ItemCode
                );

                var itemDescription = item.Descriptions
                    .FirstOrDefault(desc =>
                        desc.ItemCode.Equals(d.ItemCode, StringComparison.OrdinalIgnoreCase));

                if (itemDescription == null)
                {
                    MessageBox.Show($"حدث خطأ في بيانات الصنف: {d.ItemName}");
                    return;
                }

                // ===============================
                // 🔥 حساب المتوسط المتحرك الصحيح
                // ===============================

                // 1️⃣ آخر حالة للصنف
                // ===============================
                // 1️⃣ حساب الكمية المتبقية
                // ===============================
                var totalInQty = await _context.PurchaseInDetails
                    .Where(x => x.ItemDescriptionId == itemDescription.Id && x.ItemId == item.Id)
                    .SumAsync(x => (decimal?)x.Quantity) ?? 0;

                var totalOutQty = await _context.StockOutDetails
                    .Where(x => x.ItemDescriptionId == itemDescription.Id)
                    .SumAsync(x => (decimal?)x.Quantity) ?? 0;

                var remainingQty = totalInQty - totalOutQty;

                // ===============================
                // 2️⃣ حساب صافي القيمة
                // ===============================
                var totalInValue = await _context.PurchaseInDetails
                    .Where(x => x.ItemDescriptionId == itemDescription.Id)
                    .SumAsync(x => (decimal?)(x.Quantity * x.UnitPrice)) ?? 0;

                var totalOutValue = await _context.StockOutDetails
                    .Where(x => x.ItemDescriptionId == itemDescription.Id)
                    .SumAsync(x => (decimal?)(x.Quantity * x.AverageUnitCost)) ?? 0;

                var remainingValue = totalInValue - totalOutValue;

                // ===============================
                // 3️⃣ حساب المتوسط الجديد
                // ===============================
                decimal newAvg;

                if (remainingQty <= 0)
                {
                    // 🔥 المخزن فاضي → نبدأ من جديد
                    newAvg = d.UnitPrice;
                }
                else
                {
                    newAvg = (remainingValue + (d.Quantity * d.UnitPrice))
                             / (remainingQty + d.Quantity);
                }

                newAvg = Math.Round(newAvg, 2);


                // ===============================
                // 2️⃣ إضافة السطر مع المتوسط
                // ===============================
                purchaseDetails.Add(new PurchaseInDetail
                {
                    ItemId = item.Id,
                    ItemDescriptionId = itemDescription.Id,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice,
                    TotalPrice = d.TotalPrice,
                    AverageUnitCost = newAvg
                });

            }

            var purchase = new PurchaseIn
            {
                Date = Date,
                NumberInvoice = NumberInvoice,
                SupplierId = supplier.Id,
                PurchasedBy = PurchasedBy,
                ReceivedBy = ReceivedBy,
                Details = purchaseDetails
            };

            await _service.SaveAsync(purchase);

            MessageBox.Show("تم حفظ الفاتورة بنجاح ✅");

            Details.Clear();
            OnPropertyChanged(nameof(TotalInvoice));
        }
       
        private bool CanAddDetail()
        {
            return !string.IsNullOrWhiteSpace(ItemName)
                   && InputQuantity > 0
                   && InputUnitPrice > 0;
        }

        private bool CanSave()
        {
            return Details.Any();
        }
        private void AddDetail()
        {
            if (string.IsNullOrWhiteSpace(ItemName))
                return;

            Details.Add(new PurchaseInDetailVM
            {
                Item = SelectedItem,
                ItemName = ItemName,
                ItemCode = ItemCode,
                Quantity = InputQuantity,
                UnitPrice = InputUnitPrice,

                RowDepartment = SelectedDepartment,
                RowDepartmentName = DepartmentName,
                RowCategory = SelectedCategory,
                RowCategoryName = CategoryName
            });

            // ✅ تصفير بيانات الصنف فقط
            InputQuantity = 0;
            InputUnitPrice = 0;

            SelectedItem = null;
            ItemName = "";
            SelectedDescription = null;

            // ❌ لا تفعل هذا
            // SelectedDepartment = null;
            // DepartmentName = "";
            // SelectedCategory = null;
            // CategoryName = "";

            OnPropertyChanged(nameof(TotalInvoice));
            SaveCommand.NotifyCanExecuteChanged();
        }




        private async Task EnsureDepartmentAsync()
        {
            if (string.IsNullOrWhiteSpace(DepartmentName))
                return;

            var department = StoreDepartments
                .FirstOrDefault(d => d.Name == DepartmentName);

            if (department == null)
            {
                department = await _departmentService
                    .CreateIfNotExistsAsync(DepartmentName);

                // 👇 هنا المكان الصحيح
                StoreDepartments.Add(department);
            }

            SelectedDepartment = department;
        }
        private async Task EnsureCategoryAsync()
        {
            if (SelectedDepartment == null ||
                string.IsNullOrWhiteSpace(CategoryName))
                return;

            var category = Categories.FirstOrDefault(c =>
                c.Name == CategoryName &&
                c.StoreDepartmentId == SelectedDepartment.Id);

            if (category == null)
            {
                category = await _categoryService.CreateIfNotExistsAsync(
                    CategoryName,
                    SelectedDepartment.Id);

                // 👇 هنا المكان الصحيح
                Categories.Add(category);
            }

            SelectedCategory = category;
        }
        private void LoadItemDescriptions()
        {
            ItemDescriptions.Clear();

            if (SelectedItem == null)
                return;

            foreach (var d in SelectedItem.Descriptions)
                ItemDescriptions.Add(d.ItemCode);
        }

    }

}
