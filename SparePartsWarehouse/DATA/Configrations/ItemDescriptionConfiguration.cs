using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SparePartsWarehouse.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.DATA.Configrations
{
    public class ItemDescriptionConfiguration
    : IEntityTypeConfiguration<ItemDescription>
    {
        public void Configure(EntityTypeBuilder<ItemDescription> builder)
        {
            builder.ToTable("ItemDescriptions");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.ItemCode)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(d => new { d.ItemId, d.ItemCode })
                   .IsUnique(); // منع تكرار نفس الوصف

            builder.HasOne(d => d.Item)
                   .WithMany(i => i.Descriptions)
                   .HasForeignKey(d => d.ItemId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
