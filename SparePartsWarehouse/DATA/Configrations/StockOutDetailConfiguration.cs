using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SparePartsWarehouse.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.DATA.Configrations
{
    public class StockOutDetailConfiguration : IEntityTypeConfiguration<StockOutDetail>
    {
        public void Configure(EntityTypeBuilder<StockOutDetail> builder)
        {
            builder.ToTable("StockOutDetails");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Quantity)
                   .HasPrecision(18, 3)
                   .IsRequired();

            builder.Property(d => d.AverageUnitCost)
                   .HasPrecision(18, 4)
                   .IsRequired();

            // 🔹 StockOut (الرأس)
            builder.HasOne(d => d.StockOut)
                   .WithMany(h => h.Details)
                   .HasForeignKey(d => d.StockOutId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 🔹 Item
            builder.HasOne(d => d.Item)
                   .WithMany()
                   .HasForeignKey(d => d.ItemId)
                   .OnDelete(DeleteBehavior.Restrict);

            // 🔹 ItemDescription (الوصف)
            builder.HasOne(d => d.ItemDescription)
                   .WithMany()
                   .HasForeignKey(d => d.ItemDescriptionId)
                   .OnDelete(DeleteBehavior.Restrict);

            // 🔒 منع تكرار نفس الصنف + الوصف داخل نفس إذن الصرف
            builder.HasIndex(d => new
            {
                d.StockOutId,
                d.ItemId,
                d.ItemDescriptionId
            })
            .IsUnique();
        }

    }


}
