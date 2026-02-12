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
    public class MachineSpareTotalCostViewModel : ObservableObject
    {
        private readonly IMachineSparePartsCostService _service;
        private readonly ILookupService _lookup;

        public ObservableCollection<MachineSparePartsCostDto> Items { get; } = new();
        public ObservableCollection<FactoryDepartment> Departments { get; } = new();
        public ObservableCollection<Machine> Machines { get; } = new();

        public IRelayCommand SearchCommand { get; }

        public MachineSpareTotalCostViewModel(
            IMachineSparePartsCostService service,
            ILookupService lookup)
        {
            _service = service;
            _lookup = lookup;

            SearchCommand = new RelayCommand(async () => await LoadDataAsync());

            _ = LoadDepartmentsAsync();
            _ = LoadAllDataAsync(); // 🔥 عرض كل البيانات عند الفتح
        }

        // ================== Filters ==================

        private FactoryDepartment? _selectedDepartment;
        public FactoryDepartment? SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                if (SetProperty(ref _selectedDepartment, value))
                    _ = LoadMachinesAsync();
            }
        }

        private Machine? _selectedMachine;
        public Machine? SelectedMachine
        {
            get => _selectedMachine;
            set => SetProperty(ref _selectedMachine, value);
        }

        private DateTime? _fromDate;
        public DateTime? FromDate
        {
            get => _fromDate;
            set => SetProperty(ref _fromDate, value);
        }

        private DateTime? _toDate;
        public DateTime? ToDate
        {
            get => _toDate;
            set => SetProperty(ref _toDate, value);
        }

        // ================== Load ==================

        private async Task LoadAllDataAsync()
        {
            Items.Clear();

            var data = await _service.GetAsync();
            foreach (var d in data)
                Items.Add(d);
        }

        private async Task LoadDataAsync()
        {
            Items.Clear();

            var data = await _service.GetAsync(
                SelectedDepartment?.Id ?? 0,
                SelectedMachine?.Id ?? 0,
                FromDate,
                ToDate);

            foreach (var d in data)
                Items.Add(d);
        }

        private async Task LoadDepartmentsAsync()
        {
            Departments.Clear();
            foreach (var d in await _lookup.GetFactoryDepartmentsAsync())
                Departments.Add(d);
        }

        private async Task LoadMachinesAsync()
        {
            Machines.Clear();

            if (SelectedDepartment == null)
                return;

            foreach (var m in await _lookup.GetMachinesByFactoryDepartmentAsync(SelectedDepartment.Id))
                Machines.Add(m);

            SelectedMachine = null;
        }
    }

}
