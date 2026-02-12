using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SparePartsWarehouse.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.DATA.Configrations
{
    public class MachineConfiguration : IEntityTypeConfiguration<Machine>
    {
        public void Configure(EntityTypeBuilder<Machine> builder)
        {
            builder.ToTable("Machines");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.MachineName)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.HasOne(m => m.FactoryDepartment)
                   .WithMany(d => d.Machines)
                   .HasForeignKey(m => m.FactoryDepartmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            // منع تكرار اسم الماكينة داخل نفس القسم
            builder.HasIndex(m => new
            {
                m.MachineName,
                m.FactoryDepartmentId
            })
            .IsUnique();
            builder.HasData(
                new Machine { Id = 1, MachineName = "الخط القديم", FactoryDepartmentId = 1 },
                new Machine { Id = 2, MachineName = "الخط الجديد", FactoryDepartmentId = 1 },
                new Machine { Id = 3, MachineName = "خلاط", FactoryDepartmentId = 1 },
                new Machine { Id = 4, MachineName = "طباعه", FactoryDepartmentId = 1 },
                new Machine { Id = 5, MachineName = "امبوز", FactoryDepartmentId = 1 },
                new Machine { Id = 6, MachineName = "كاردا", FactoryDepartmentId = 1 },
                new Machine { Id = 7, MachineName = "مقص", FactoryDepartmentId = 1 },
                new Machine { Id = 8, MachineName = "اللفاف", FactoryDepartmentId = 1 },
                new Machine { Id = 9, MachineName = "الفرز", FactoryDepartmentId = 1 },
                new Machine { Id = 10, MachineName = "مرافق", FactoryDepartmentId = 1 },
                new Machine { Id = 11, MachineName = "كلارك", FactoryDepartmentId = 1 },
                new Machine { Id = 12, MachineName = "اسانسير", FactoryDepartmentId = 1 },
                new Machine { Id = 13, MachineName = "ماكينه 1 اورزيو", FactoryDepartmentId = 2 },
                new Machine { Id = 14, MachineName = "ماكينه 2 اورزيو", FactoryDepartmentId = 2 },
                new Machine { Id = 15, MachineName = "ماكينه 3 اورزيو", FactoryDepartmentId = 2 },
                new Machine { Id = 16, MachineName = "ماكينه 4 اورزيو", FactoryDepartmentId = 2 },
                new Machine { Id = 17, MachineName = "ماكينه 5 باى لونج", FactoryDepartmentId = 2 },
                new Machine { Id = 18, MachineName = "ماكينه 6 سانج يونج", FactoryDepartmentId = 2 },
                new Machine { Id = 19, MachineName = "ماكينه 7 الجديده", FactoryDepartmentId = 2 },
                new Machine { Id = 20, MachineName = "ماكينه 8 ", FactoryDepartmentId = 2 },
                new Machine { Id = 21, MachineName = "ماكينه 9   جاكار ", FactoryDepartmentId = 2 },
                new Machine { Id = 22, MachineName = "ماكينه 10 فرو", FactoryDepartmentId = 2 },
                new Machine { Id = 23, MachineName = "ماكينه 11 فرو", FactoryDepartmentId = 2 },
                new Machine { Id = 24, MachineName = "ماكينه 12 فرو", FactoryDepartmentId = 2 },
                new Machine { Id = 25, MachineName = "ماكينه 13 فرو", FactoryDepartmentId = 2 },
                new Machine { Id = 26, MachineName = "ماكينه 14 فرو", FactoryDepartmentId = 2 },
                new Machine { Id = 27, MachineName = "ماكينه 15 بو ليستر لامع", FactoryDepartmentId = 2 },
                new Machine { Id = 28, MachineName = "ماكينه 16 فرو أكرليليك", FactoryDepartmentId = 2 },
                new Machine { Id = 29, MachineName = "عدد و ادوات", FactoryDepartmentId = 3 },
                new Machine { Id = 30, MachineName = "مرافق", FactoryDepartmentId = 4 },
                new Machine { Id = 31, MachineName = "اخرى", FactoryDepartmentId = 5 },
                new Machine { Id = 32, MachineName = "مرافق", FactoryDepartmentId = 5 },
                new Machine { Id = 33, MachineName = "كومبروسور الكبير", FactoryDepartmentId = 6 },
                new Machine { Id = 34, MachineName = "كومبروسور الصغير", FactoryDepartmentId = 6 },
                new Machine { Id = 35, MachineName = "كومبروسور كيذر", FactoryDepartmentId = 6 },
                new Machine { Id = 36, MachineName = "كومبروسور أطلس", FactoryDepartmentId = 6 },
                new Machine { Id = 37, MachineName = "الغلايه القديمه", FactoryDepartmentId = 7 },
                new Machine { Id = 38, MachineName = "الغلايه الجديده", FactoryDepartmentId = 7 }
                );
        }
    }


}
