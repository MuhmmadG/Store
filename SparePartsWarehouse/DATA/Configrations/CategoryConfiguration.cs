using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SparePartsWarehouse.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.DATA.Configrations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(150);

           

           
            builder.HasData(
    // ميكانيكا
    new Category { Id = 1, Name = "سيور", StoreDepartmentId = 1 },
    new Category { Id = 2, Name = "رولمان بلي", StoreDepartmentId = 1 },
    new Category { Id = 3, Name = "تروس", StoreDepartmentId = 1 },

    // كهرباء
    new Category { Id = 4, Name = "مواتير", StoreDepartmentId = 2 },
    new Category { Id = 5, Name = "لمبات", StoreDepartmentId = 2 }
);


        }
    }

}
