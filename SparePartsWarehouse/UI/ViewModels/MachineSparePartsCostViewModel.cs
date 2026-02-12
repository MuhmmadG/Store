using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SparePartsWarehouse.CORE.DTO;
using SparePartsWarehouse.CORE.Entities;
using SparePartsWarehouse.CORE.Interfaces;
using SparePartsWarehouse.UI.PDF;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using QuestPDF.Fluent;

namespace SparePartsWarehouse.UI.ViewModels
{
    public class MachineSparePartsCostViewModel : ObservableObject
    {
        private readonly IMachineSparePartsCostService _service;
        private readonly ILookupService _lookup;
      //  public ObservableCollection<MachineSparePartsCostDetailDto> Details { get; } = new();
        public ObservableCollection<FactoryDepartment> Departments { get; } = new();
        // ===============================
        // الأقسام
        // ===============================
        private FactoryDepartment? _selectedDepartment;
        public FactoryDepartment? SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                if (SetProperty(ref _selectedDepartment, value))
                {
                    _ = LoadMachinesAsync();
                }
            }
        }

        // ===============================
        // الماكينات
        // ===============================
        public ObservableCollection<Machine> Machines { get; } = new();

        private Machine? _selectedMachine;
        public Machine? SelectedMachine
        {
            get => _selectedMachine;
            set => SetProperty(ref _selectedMachine, value);
        }

        // ===============================
        // البيانات المعروضة
        // ===============================
        public ObservableCollection<MachineSparePartsCostDetailDto> Items { get; } = new();
        private DateTime? _fromDate;
        public DateTime? FromDate
        {
            get => _fromDate;
            set
            {
                if (SetProperty(ref _fromDate, value))
                    _ = LoadDataAsync();
            }
        }

        private DateTime? _toDate;
        public DateTime? ToDate
        {
            get => _toDate;
            set
            {
                if (SetProperty(ref _toDate, value))
                    _ = LoadDataAsync();
            }
        }

        public MachineSparePartsCostViewModel(IMachineSparePartsCostService service, ILookupService lookup)
        {
            _service = service;

            _service = service;
            _lookup = lookup;
            ExportPdfCommand = new RelayCommand(ExportToPdf);

            _ = LoadDepartmentsAsync();

            // 🔥 تحميل كل البيانات عند فتح النافذة
            _ = LoadAllDataAsync();
            SearchCommand = new RelayCommand(async () => await LoadDataAsync());
        }
        private void ExportToPdf()
        {
            if (!Items.Any())
                return;

            var path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                $"MachineSparePartsCost_{DateTime.Now:yyyyMMdd_HHmm}.pdf");

            var doc = new MachineSparePartsCostPdfDocument(
                Items.ToList(),
                SelectedDepartment?.Name,
                SelectedMachine?.MachineName,
                FromDate,
                ToDate
            );

            doc.GeneratePdf(path);

            Process.Start(new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            });
        }

        public IRelayCommand SearchCommand { get; }
        public IRelayCommand ExportPdfCommand { get; }

        private async Task LoadDepartmentsAsync()
        {
            Departments.Clear();
            var list = await _lookup.GetFactoryDepartmentsAsync();

            foreach (var d in list)
                Departments.Add(d);
        }
        private async Task LoadMachinesAsync()
        {
            Machines.Clear();

            if (SelectedDepartment == null)
                return;

            var list = await _lookup.GetMachinesByFactoryDepartmentAsync(SelectedDepartment.Id);

            foreach (var m in list)
                Machines.Add(m);

            SelectedMachine = null; // مهم جداً
        }
        private async Task LoadDataAsync()
        {
            Items.Clear();

            var data = await _service.GetDetailsAsync(
                factoryDepartmentId: SelectedDepartment?.Id ?? 0,
                machineId: SelectedMachine?.Id ?? 0,
                fromDate: FromDate,
                toDate: ToDate
            );

            foreach (var item in data)
                Items.Add(item);
        }




        private async Task LoadAllDataAsync()
        {
            Items.Clear();

            var data = await _service.GetDetailsAsync(
                factoryDepartmentId: 0,
                machineId: 0,
                fromDate: null,
                toDate: null
            );

            foreach (var d in data)
                Items.Add(d);
        }

    }


}
