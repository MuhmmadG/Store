using SparePartsWarehouse.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SparePartsWarehouse.UI.Views
{
    /// <summary>
    /// Interaction logic for StockOutView.xaml
    /// </summary>
    public partial class StockOutView : UserControl
    {
        public StockOutView(StockOutViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            Loaded += async (_, __) =>
            {
                await vm.InitAsync();
            };
        }
    }
}
