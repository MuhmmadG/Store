using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuestPDF.Fluent;
using SparePartsWarehouse.CORE.DTO;
using SparePartsWarehouse.CORE.Interfaces;
using SparePartsWarehouse.UI.PDF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SparePartsWarehouse.UI.ViewModels
{
    public class StockBalanceViewModel : ObservableObject
    {
        private readonly IStockOutService _service;
        public ObservableCollection<StockBalanceDto> AllItems { get; }
            = new ObservableCollection<StockBalanceDto>();
        public ObservableCollection<StockBalanceDto> FilteredItems { get; }
            = new ObservableCollection<StockBalanceDto>();
        public ObservableCollection<string> Categories { get; }
            = new ObservableCollection<string>();

        private string _selectedCategory;
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                SetProperty(ref _selectedCategory, value);
                ApplyFilter();
            }
        }
        public ObservableCollection<StockBalanceDto> Items { get; }
            = new ObservableCollection<StockBalanceDto>();
        public IRelayCommand ExportToPdfCommand { get; }

        public StockBalanceViewModel(IStockOutService service)
        {
            _service = service;
            ExportToPdfCommand = new RelayCommand(ExportToPdf);

            _ = LoadAsync();
        }
        private void ExportToPdf()
        {
            var filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                $"StockBalance_{DateTime.Now:yyyyMMdd_HHmm}.pdf");

            var document = new StockBalancePdfDocument(
                FilteredItems.ToList(),
                SelectedCategory
            );

            document.GeneratePdf(filePath);

            Process.Start(new ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            });
        }

        private async Task LoadAsync()
        {
            var data = await _service.GetStockBalanceAsync();

            AllItems.Clear();
            FilteredItems.Clear();
            Categories.Clear();

            Categories.Add("الكل");

            foreach (var item in data)
            {
                AllItems.Add(item);
                FilteredItems.Add(item);

                if (!Categories.Contains(item.CategoryName))
                    Categories.Add(item.CategoryName);
            }

            SelectedCategory = "الكل";
        }
        private void ApplyFilter()
        {
            FilteredItems.Clear();

            var items = SelectedCategory == "الكل"
                ? AllItems
                : AllItems.Where(x => x.CategoryName == SelectedCategory);

            foreach (var item in items)
                FilteredItems.Add(item);
        }

    }

}
