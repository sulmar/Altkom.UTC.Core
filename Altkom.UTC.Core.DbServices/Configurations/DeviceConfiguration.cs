using Altkom.UTC.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.UTC.Core.DbServices.Configurations
{
    class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder
             .Property(p => p.Name)
             .HasMaxLength(100)
             .IsUnicode(false);

            builder
             .Property(p => p.Model)
             .HasMaxLength(100)
             .IsRequired(true);
        }
    }
}
