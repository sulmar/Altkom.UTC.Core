using Altkom.UTC.Core.DbServices.Configurations;
using Altkom.UTC.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.UTC.Core.DbServices
{

    // dotnet add package Microsoft.EntityFrameworkCore
    public class UTCContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<User> Users { get; set; }

        public UTCContext(DbContextOptions<UTCContext> options)
            : base(options)
        {

            // this.Database.EnsureCreated();

             

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new CustomerConfiguration())
                .ApplyConfiguration(new DeviceConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
