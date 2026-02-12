using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SparePartsWarehouse.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.DATA.Configrations
{
    public class PurchaseInConfiguration : IEntityTypeConfiguration<PurchaseIn>
    {
        public void Configure(EntityTypeBuilder<PurchaseIn> builder)
        {
            builder.ToTable("PurchaseIns");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.PurchasedBy)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(p => p.ReceivedBy)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(p => p.NumberInvoice)
                   .IsRequired()
                   .HasMaxLength(50);
                 
            builder.HasOne(p => p.Supplier)
                   .WithMany()
                   .HasForeignKey(p => p.SupplierId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
