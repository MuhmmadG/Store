using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SparePartsWarehouse.CORE.DTO;
using SparePartsWarehouse.CORE.Entities;
using SparePartsWarehouse.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SparePartsWarehouse.UI.ViewModels
{
    public class ItemMovementViewModel : ObservableObject
    {
        private readonly ILookupService _lookup;
        private readonly IItemMovementService _service;

        public ObservableCollection<Item> Items { get; } = new();
        public ObservableCollection<ItemDescription> Descriptions { get; } = new();
        public ObservableCollection<ItemMovement> Movements { get; } = new();

        private int? _selectedItemId;
        public int? SelectedItemId
        {
            get => _selectedItemId;
            set
            {
                if (SetProperty(ref _selectedItemId, value))
                {
                    SelectedDescriptionId = null;
                    _ = LoadDescriptionsAsync();
                    _ = LoadMovementsAsync();
                }
            }
        }

        private int? _selectedDescriptionId;
        public int? SelectedDescriptionId
        {
            get => _selectedDescriptionId;
            set
            {
                if (SetProperty(ref _selectedDescriptionId, value))
                    _ = LoadMovementsAsync();
            }
        }

        public ItemMovementViewModel(ILookupService lookup, IItemMovementService service)
        {
            _lookup = lookup;
            _service = service;

            _ = LoadAsync(); // تحميل البيانات عند فتح النافذة
        }

        private async Task LoadAsync()
        {
            Items.Clear();
            foreach (var item in await _lookup.GetItemsAsync())
                Items.Add(item);

            await LoadMovementsAsync(); // عرض كل البيانات أولاً
        }

        private async Task LoadDescriptionsAsync()
        {
            Descriptions.Clear();
            if (!SelectedItemId.HasValue) return;

            foreach (var d in await _lookup.GetItemDescriptionsByItemIdAsync(SelectedItemId.Value))
                Descriptions.Add(d);
        }

        private async Task LoadMovementsAsync()
        {
            Movements.Clear();
            var data = await _service.GetAsync(SelectedItemId, SelectedDescriptionId);

            foreach (var row in data)
                Movements.Add(row);
        }
    }



}
