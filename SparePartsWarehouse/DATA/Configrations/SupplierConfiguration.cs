using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SparePartsWarehouse.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.DATA.Configrations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Suppliers");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.SupplierName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasIndex(s => s.SupplierName)
                   .IsUnique();   // 🔥 منع التكرار

            builder.Property(s => s.IsActive)
                   .HasDefaultValue(true);
        }
    }

}
