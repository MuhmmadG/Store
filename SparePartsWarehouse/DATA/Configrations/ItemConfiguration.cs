using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SparePartsWarehouse.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.DATA.Configrations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.ItemName)
                   .IsRequired()
                   .HasMaxLength(200);


            // ⭐ اسم الصنف لا يتكرر داخل نفس الفئة
            builder.HasIndex(i => new { i.ItemName, i.CategoryId })
                   .IsUnique();

            builder.HasOne(i => i.Category)
                   .WithMany(c => c.Items)
                   .HasForeignKey(i => i.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }


}
