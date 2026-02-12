using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SparePartsWarehouse.CORE;
using SparePartsWarehouse.CORE.Interfaces;
using SparePartsWarehouse.DATA.Context;
using SparePartsWarehouse.DATA.Service;
using SparePartsWarehouse.DATA.StockOutService;
using SparePartsWarehouse.UI.ViewModels;
using SparePartsWarehouse.UI.Views;
using System;
using System.Collections.Generic;
using System.Text;
using UI;

namespace SparePartsWarehouse.UI
{
    public class AppHost
    {
        private static ServiceProvider _serviceProvider;
        public static void ConfigureServices()
        {
                       var services = new ServiceCollection();
            // 1️⃣ تسجيل DbContext
            //services.AddDbContext<AppDbContext>(options =>
            //    options.UseSqlServer(AppConfiguration.GetConnectionString()));
            services.AddDbContextFactory<AppDbContext>(options =>

               options.UseSqlServer(AppConfiguration.GetConnectionString()));


            services.AddScoped<ILookupService, LookupService>();

            // Register your services, view models, and other dependencies here
            // Example:
            services.AddTransient<MainWindow>();
             services.AddTransient<PurchaseInViewModel>();
             services.AddSingleton<ILookupService, LookupService>();
            services.AddSingleton<IPurchaseInService, PurchaseInService>();
            services.AddSingleton<IDepartmentService, DepartmentService>();
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton<ISupplierService, SupplierService>();
            services.AddSingleton<IStockOutService, StockOutService>();
            services.AddSingleton<IMachineSparePartsCostService, MachineSparePartsCostService>();
            services.AddTransient<IItemMovementService, ItemMovementService>();

            services.AddTransient<StockOutViewModel>();
            services.AddTransient<StockOutView>();

            services.AddTransient<StockBalanceViewModel>();
            services.AddTransient<StockBalanceView>();

            services.AddSingleton<IPurchaseDeleteService, PurchaseDeleteService>();
            services.AddTransient<DeletePurchaseViewModel>();
            services.AddTransient<PurchaseInDetailView>();

            services.AddSingleton<IPurchaseEditService, PurchaseEditService>();
            services.AddTransient<EditPurchaseViewModel>();
            services.AddTransient<EditPurchaseItemView>();

            services.AddTransient<EditStockOutViewModel>();
            services.AddTransient<EditStockOutQuantityView>();

            services.AddTransient<MachineSparePartsCostViewModel>();
            services.AddTransient<MachineSparePartsCostView>();


            services.AddTransient<MachineSpareTotalCostViewModel>();
            services.AddTransient<MachineSpareTotalCostView>();

            services.AddTransient<ItemMovementViewModel>();
            services.AddTransient<ItemMovementView>();

            services.AddTransient<DashboardView>();
            services.AddSingleton<ViewWindowModel>();
            services.AddTransient<ViewWindow>();
            _serviceProvider = services.BuildServiceProvider();

        }
        public static T GetService<T>() where T : class
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
