using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SparePartsWarehouse.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.DATA.Configrations
{
    public class PurchaseInDetailConfiguration : IEntityTypeConfiguration<PurchaseInDetail>
    {
        public void Configure(EntityTypeBuilder<PurchaseInDetail> builder)
        {
            builder.ToTable("PurchaseInDetails");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.UnitPrice)
                   .HasColumnType("decimal(18,2)");

            builder.Property(d => d.TotalPrice)
                   .HasColumnType("decimal(18,2)");

            builder.Property(d => d.AverageUnitCost)
                   .HasColumnType("decimal(8,2)");

            builder.HasOne(d => d.Item)
                   .WithMany(i => i.PurchaseInDetails)
                   .HasForeignKey(d => d.ItemId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
