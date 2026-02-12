using CommunityToolkit.Mvvm.ComponentModel;
using SparePartsWarehouse.CORE.Entities;
using SparePartsWarehouse.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SparePartsWarehouse.UI.ViewModels
{
    public class ItemSelectionViewModel : ObservableObject
    {
        private readonly ILookupService _lookup;

        public ItemSelectionViewModel(ILookupService lookup)
        {
            _lookup = lookup;
        }

        public async Task InitAsync()
        {
            await LoadStoreDepartmentsAsync();

            // ✅ اختيار أول قسم تلقائيًا
            SelectedStoreDepartment = StoreDepartments.FirstOrDefault();
        }

        // ================= Collections =================
        public ObservableCollection<StoreDepartment> StoreDepartments { get; } = new();
        public ObservableCollection<Category> Categories { get; } = new();
        public ObservableCollection<Item> Items { get; } = new();
        public ObservableCollection<ItemDescription> ItemDescriptions { get; } = new();

        // ================= Selected =================
        private StoreDepartment? _selectedStoreDepartment;
        public StoreDepartment? SelectedStoreDepartment
        {
            get => _selectedStoreDepartment;
            set
            {
                if (SetProperty(ref _selectedStoreDepartment, value))
                {
                    _ = LoadCategoriesAsync();
                }
            }
        }

        private Category? _selectedCategory;
        public Category? SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (SetProperty(ref _selectedCategory, value))
                {
                    _ = LoadItemsAsync();
                }
            }
        }

        private Item? _selectedItem;
        public Item? SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (SetProperty(ref _selectedItem, value))
                {
                    _ = LoadDescriptionsAsync();
                }
            }
        }

        private ItemDescription? _selectedItemDescription;
        public ItemDescription? SelectedItemDescription
        {
            get => _selectedItemDescription;
            set => SetProperty(ref _selectedItemDescription, value);
        }

        // ================= Load Logic =================
        private async Task LoadStoreDepartmentsAsync()
        {
            StoreDepartments.Clear();

            var departments = await _lookup.GetStoreDepartmentsAsync();
            foreach (var d in departments)
                StoreDepartments.Add(d);
        }

        private async Task LoadCategoriesAsync()
        {
            Categories.Clear();
            Items.Clear();
            ItemDescriptions.Clear();

            SelectedCategory = null;
            SelectedItem = null;
            SelectedItemDescription = null;

            if (SelectedStoreDepartment == null)
                return;

            var categories = await _lookup
                .GetCategoriesByDepartmentAsync(SelectedStoreDepartment.Id);

            foreach (var c in categories)
                Categories.Add(c);

            // ✅ اختيار أول فئة تلقائيًا
            SelectedCategory = Categories.FirstOrDefault();
        }

        private async Task LoadItemsAsync()
        {
            Items.Clear();
            ItemDescriptions.Clear();

            SelectedItem = null;
            SelectedItemDescription = null;

            if (SelectedCategory == null)
                return;

            var items = await _lookup.GetItemsByCategoryAsync(SelectedCategory.Id);
            foreach (var i in items)
                Items.Add(i);

            // ✅ اختيار أول صنف تلقائيًا
            SelectedItem = Items.FirstOrDefault();
        }

        private async Task LoadDescriptionsAsync()
        {
            ItemDescriptions.Clear();
            SelectedItemDescription = null;

            if (SelectedItem == null)
                return;

            var descriptions = await _lookup
                .GetItemDescriptionsByItemIdAsync(SelectedItem.Id);

            foreach (var d in descriptions)
                ItemDescriptions.Add(d);

            // ✅ اختيار أول وصف تلقائيًا
            SelectedItemDescription = ItemDescriptions.FirstOrDefault();
        }
    }


}
