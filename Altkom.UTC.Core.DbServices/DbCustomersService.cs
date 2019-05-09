using Altkom.UTC.Core.IServices;
using Altkom.UTC.Core.Models;
using Altkom.UTC.Core.Models.SearchCriteria;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Altkom.UTC.Core.DbServices
{
    public class DbEntitiesService<T> : IEntiesService<T>
        where T : Base, new()
    {
        protected readonly UTCContext context;

        public DbEntitiesService(UTCContext context)
        {
            this.context = context;
        }

        public virtual void Add(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        public virtual IEnumerable<T> Get()
        {
            return context.Set<T>().ToList();
        }

        public virtual T Get(int id)
        {
            return context.Set<T>().Find(id);
        }

        public virtual void Remove(int id)
        {
            T entity = new T() { Id = id };

            context.Remove(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            context.Update(entity);
            context.SaveChanges();
        }
    }

    public class DbDevicesService : DbEntitiesService<Device>, IDevicesService
    {
        public DbDevicesService(UTCContext context) : base(context)
        {
        }

        public Device Get(string name)
        {
            return context.Devices.Where(d => d.Name == name).FirstOrDefault();
        }
    }

    public class DbCustomersService : ICustomersService
    {
        private readonly UTCContext context;

        public DbCustomersService(UTCContext context)
        {
            this.context = context;
        }

        public void Add(Customer entity)
        {
            Trace.WriteLine(context.Entry(entity).State);

            context.Customers.Add(entity);

            Trace.WriteLine(context.Entry(entity).State);

            context.SaveChanges();

            Trace.WriteLine(context.Entry(entity).State);

            entity.FirstName = "Bartek";

            Trace.WriteLine(context.Entry(entity).State);

            var x = context.Entry(entity).Property(p => p.FirstName);

            context.SaveChanges();
            

            Trace.WriteLine(context.Entry(entity).State);



        }

        public IEnumerable<Customer> Get(CustomerSearchCriteria searchCriteria)
        {
            return context.Customers.ToList();
        }

        public IEnumerable<Customer> Get()
        {
            return context.Customers.ToList();
        }

        public Customer Get(int id)
        {
            return context.Customers
                .Include(p => p.Devices)
                .SingleOrDefault(p=>p.Id == id);

            return context.Customers.Find(id);
        }

        public void Remove(int id)
        {
            // Customer customer = Get(id);

            Customer customer = new Customer { Id = id };

            context.Customers.Attach(customer);
              
            Trace.WriteLine(context.Entry(customer).State);

            // context.Entry(customer).State = EntityState.Deleted;
            context.Customers.Remove(customer);

            Trace.WriteLine(context.Entry(customer).State);

            context.SaveChanges();

            Trace.WriteLine(context.Entry(customer).State);
        }

        public void Update(Customer entity)
        {
            Trace.WriteLine(context.Entry(entity).State);

            context.Customers.Update(entity);

            Trace.WriteLine(context.Entry(entity).State);

            context.SaveChanges();

            Trace.WriteLine(context.Entry(entity).State);
        }
    }
}
