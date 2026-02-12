using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using SparePartsWarehouse.UI;
using SparePartsWarehouse.UI.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application 
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            

            // في بداية التطبيق (مثل Program.cs أو عند Startup)
            QuestPDF.Settings.License = LicenseType.Community;
            var services = new ServiceCollection();
           // base.OnStartup(e);

            AppHost.ConfigureServices();
            var MainWindow = AppHost.GetService<ViewWindow>();
            MainWindow.Show();
        }
        
    }

}
