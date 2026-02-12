using Microsoft.EntityFrameworkCore;
using SparePartsWarehouse.CORE.DTO;
using SparePartsWarehouse.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.DATA.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        // DbSets
        // Constructor إضافي للـ design-time
        public AppDbContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // إذا لم يكن هناك إعدادات مسبقة، استخدم الاتصال الافتراضي
            if (!options.IsConfigured)
            {
                options.UseSqlServer(AppConfiguration.GetConnectionString());
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configrations.CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new Configrations.StoreDepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new Configrations.ItemConfiguration());
            modelBuilder.ApplyConfiguration(new Configrations.MachineConfiguration());
            modelBuilder.ApplyConfiguration(new Configrations.PurchaseInConfiguration());
            modelBuilder.ApplyConfiguration(new Configrations.PurchaseInDetailConfiguration());
            modelBuilder.ApplyConfiguration(new Configrations.StockOutConfiguration());
            modelBuilder.ApplyConfiguration(new Configrations.StockOutDetailConfiguration());
            modelBuilder.ApplyConfiguration(new Configrations.SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new Configrations.ItemDescriptionConfiguration());
            modelBuilder.ApplyConfiguration(new Configrations.FactoryDepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new Configrations.DocumentEditsLogConfiguration());
            modelBuilder.Entity<StockBalanceDto>()
        .HasNoKey()
        .ToView("vw_StockBalance");

            modelBuilder.Entity<ItemMovement>(entity =>
            {
                entity.HasNoKey();                // ⭐ مهم جدًا
                entity.ToView("vw_ItemMovement"); // اسم الـ View
            });

            // DTO ليس جدولًا حقيقيًا
            modelBuilder.Entity<MachineSparePartsCostDetailDto>().HasNoKey();
            modelBuilder.Entity<MachineSparePartsCostDto>().HasNoKey();
        }
        public DbSet<CORE.Entities.Category> Categories { get; set; }
        public DbSet<CORE.Entities.StoreDepartment> StoreDepartments { get; set; }
        public DbSet<CORE.Entities.Item> Items { get; set; }
        public DbSet<CORE.Entities.Machine> Machines { get; set; }
        public DbSet<CORE.Entities.PurchaseIn> PurchaseIns { get; set; }
        public DbSet<CORE.Entities.PurchaseInDetail> PurchaseInDetails { get; set; }
        public DbSet<CORE.Entities.StockOut> StockOuts { get; set; }
        public DbSet<CORE.Entities.StockOutDetail> StockOutDetails { get; set; }
        public DbSet<CORE.Entities.Supplier> Suppliers { get; set; }
        public DbSet<CORE.Entities.ItemDescription> ItemDescriptions { get; set; }
        public DbSet<CORE.Entities.FactoryDepartment> FactoryDepartments { get; set; }
        public DbSet<CORE.Entities.DocumentEditsLog> DocumentEditsLogs { get; set; }
        public DbSet<StockBalanceDto> StockBalances { get; set; }
        public DbSet<StockAdjustment> StockAdjustments { get; set; }
        public DbSet<MachineSparePartsCostDetailDto> MachineSparePartsCost { get; set; }

        public DbSet<MachineSparePartsCostDto> MachineSparePartsCosts { get; set; }
        public DbSet<ItemMovement> ItemMovements { get; set; }

    }
}
