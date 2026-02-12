using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SparePartsWarehouse.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.DATA.Configrations
{
    public class FactoryDepartmentConfiguration : IEntityTypeConfiguration<FactoryDepartment>
    {
        public void Configure(EntityTypeBuilder<FactoryDepartment> builder)
        {
            builder.ToTable("FactoryDepartments");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(d => d.IsActive)
                   .HasDefaultValue(true);

            builder.HasMany(d => d.Machines)
                   .WithOne(m => m.FactoryDepartment)
                   .HasForeignKey(m => m.FactoryDepartmentId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasIndex(d => d.Name)
            .IsUnique(); // 🔥 اسم القسم Unique
            builder.HasData(
                 new FactoryDepartment { Id = 1, Name = "الجلد" },
                 new FactoryDepartment { Id = 2, Name = "النسيج" },
                 new FactoryDepartment { Id = 3, Name = "قطع غيار" },
                 new FactoryDepartment { Id = 4, Name = "الاداره" },
                 new FactoryDepartment { Id = 5, Name = "مخزن" },
                 new FactoryDepartment { Id = 6, Name = "كومبروسور" },
                 new FactoryDepartment { Id = 7, Name = "الغلايه" }
);
        }
    }

}
