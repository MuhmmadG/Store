using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SparePartsWarehouse.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.DATA.Configrations
{
    public class StockAdjustmentConfiguration
    : IEntityTypeConfiguration<StockAdjustment>
    {
        public void Configure(EntityTypeBuilder<StockAdjustment> builder)
        {
            builder.ToTable("StockAdjustments");

            builder.Property(x => x.Quantity)
                   .HasPrecision(18, 2);

            builder.Property(x => x.UnitCost)
                   .HasPrecision(18, 2);

            builder.Property(x => x.Reason)
                   .HasMaxLength(250);
        }
    }

}
