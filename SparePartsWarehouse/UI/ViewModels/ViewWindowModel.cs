using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using SparePartsWarehouse.UI.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using UI;

namespace SparePartsWarehouse.UI.ViewModels
{
    public class ViewWindowModel : ObservableObject
    {
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public ICommand ShowDashboardCommand { get; }
        public ICommand ShowStockOutCommand { get; }
        public ICommand ShowStockBalanceCommand { get; }
        public ICommand ShowPurchaseInCommand { get; }
        public ICommand ShowItemMovementCommand { get; }
        public ICommand ShowIEditPurchaseItemCommand { get; }
        public ICommand ShowEditStockOutQuantityCommand { get; }
        public ICommand ShowMachineSparePartsCostViewCommand { get; }
        public ICommand ShowMachineMachineSpareTotalCostViewCommand { get; }
        public ICommand ShowPurchaseInDetailViewCommand { get; }

        public ViewWindowModel()
        {
            ShowDashboardCommand = new RelayCommand(() =>
                CurrentView = AppHost.GetService<DashboardView>());

            ShowStockOutCommand = new RelayCommand(() =>
                CurrentView = AppHost.GetService<StockOutView>());

            ShowStockBalanceCommand = new RelayCommand(() =>
                CurrentView = AppHost.GetService<StockBalanceView>());

            ShowPurchaseInCommand = new RelayCommand(() =>
                CurrentView = AppHost.GetService<MainWindow>());

            ShowItemMovementCommand = new RelayCommand(() =>
                CurrentView = AppHost.GetService<ItemMovementView>());

            ShowIEditPurchaseItemCommand = new RelayCommand(() =>
                CurrentView = AppHost.GetService<EditPurchaseItemView>());

            ShowEditStockOutQuantityCommand = new RelayCommand(() =>
                CurrentView = AppHost.GetService<EditStockOutQuantityView>());

            ShowMachineSparePartsCostViewCommand = new RelayCommand(() =>
                CurrentView = AppHost.GetService<MachineSparePartsCostView>());

            ShowMachineMachineSpareTotalCostViewCommand = new RelayCommand(() =>
                CurrentView = AppHost.GetService<MachineSpareTotalCostView>());

            ShowPurchaseInDetailViewCommand = new RelayCommand(() =>
                CurrentView = AppHost.GetService<PurchaseInDetailView>());

            // أول شاشة
            CurrentView = AppHost.GetService<DashboardView>();
        }
    }



}
