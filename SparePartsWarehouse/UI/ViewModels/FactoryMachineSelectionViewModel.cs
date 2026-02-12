using CommunityToolkit.Mvvm.ComponentModel;
using SparePartsWarehouse.CORE.Entities;
using SparePartsWarehouse.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace SparePartsWarehouse.UI.ViewModels
{
    public class FactoryMachineSelectionViewModel : ObservableObject
    {
        private readonly ILookupService _lookup;

        public ObservableCollection<FactoryDepartment> FactoryDepartments { get; } = new();
        public ObservableCollection<Machine> Machines { get; } = new();

        private FactoryDepartment _selectedFactoryDepartment;
        public FactoryDepartment SelectedFactoryDepartment
        {
            get => _selectedFactoryDepartment;
            set
            {
                if (SetProperty(ref _selectedFactoryDepartment, value))
                {
                    _ = LoadMachinesAsync();
                }
            }
        }

        private Machine _selectedMachine;
        public Machine SelectedMachine
        {
            get => _selectedMachine;
            set => SetProperty(ref _selectedMachine, value);
        }

        public FactoryMachineSelectionViewModel(ILookupService lookup)
        {
            _lookup = lookup;
        }

        public async Task InitAsync()
        {
            await LoadFactoryDepartmentsAsync();

            // ✅ تعيين أول قسم تلقائيًا
            SelectedFactoryDepartment = FactoryDepartments.FirstOrDefault();
        }

        private async Task LoadFactoryDepartmentsAsync()
        {
            FactoryDepartments.Clear();

            var departments = await _lookup.GetFactoryDepartmentsAsync();
            foreach (var d in departments)
                FactoryDepartments.Add(d);
        }

        private async Task LoadMachinesAsync()
        {
            Machines.Clear();

            if (SelectedFactoryDepartment == null)
                return;

            var machines = await _lookup
                .GetMachinesByFactoryDepartmentAsync(SelectedFactoryDepartment.Id);

            foreach (var m in machines)
                Machines.Add(m);

            // ✅ تعيين أول ماكينة تلقائيًا
            SelectedMachine = Machines.FirstOrDefault();
        }
    }



}
