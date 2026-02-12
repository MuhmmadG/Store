using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SparePartsWarehouse.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.DATA.Configrations
{

    public class StockOutConfiguration : IEntityTypeConfiguration<StockOut>
    {
        public void Configure(EntityTypeBuilder<StockOut> builder)
        {
            builder.ToTable("StockOuts");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Date)
                   .IsRequired();

            builder.Property(s => s.IssuerName)
                   .HasMaxLength(150)
                   .HasDefaultValue("Ahmed"); // 👈 قيمة افتراضية

            builder.Property(s => s.ReceiverName)
                   .HasMaxLength(150);

            builder.HasOne(s => s.StoreDepartment)
                   .WithMany()
                   .HasForeignKey(s => s.StoreDepartmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.FactoryDepartment)
                   .WithMany()
                   .HasForeignKey(s => s.FactoryDepartmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Machine)
                   .WithMany()
                   .HasForeignKey(s => s.MachineId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(s => s.Details)
                   .WithOne(d => d.StockOut)
                   .HasForeignKey(d => d.StockOutId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }



}
