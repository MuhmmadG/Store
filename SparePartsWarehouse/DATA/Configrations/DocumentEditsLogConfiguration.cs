using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SparePartsWarehouse.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.DATA.Configrations
{
    public class DocumentEditsLogConfiguration
    : IEntityTypeConfiguration<DocumentEditsLog>
    {
        public void Configure(EntityTypeBuilder<DocumentEditsLog> builder)
        {
            builder.ToTable("DocumentEditsLogs");

            builder.Property(x => x.DocumentType)
                   .HasMaxLength(50);

            builder.Property(x => x.FieldName)
                   .HasMaxLength(50);

            builder.Property(x => x.OldValue)
                   .HasMaxLength(100);

            builder.Property(x => x.NewValue)
                   .HasMaxLength(100);
        }
    }

}
