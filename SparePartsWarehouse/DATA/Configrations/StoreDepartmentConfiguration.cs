using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SparePartsWarehouse.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.DATA.Configrations
{
    public class StoreDepartmentConfiguration : IEntityTypeConfiguration<StoreDepartment>
    {
        public void Configure(EntityTypeBuilder<StoreDepartment> builder)
        {
            builder.ToTable("StoreDepartment");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.HasIndex(d => d.Name)
                   .IsUnique(); // 🔥 اسم القسم Unique
            builder.HasData(
                 new StoreDepartment { Id = 1, Name = "ميكانيكا" },
                 new StoreDepartment { Id = 2, Name = "كهرباء" },
                 new StoreDepartment { Id = 3, Name = "عدد وقطع تشغيل" }
);
        }
    }

}
