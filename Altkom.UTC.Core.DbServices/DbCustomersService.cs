using Altkom.UTC.Core.IServices;
using Altkom.UTC.Core.Models;
using Altkom.UTC.Core.Models.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Altkom.UTC.Core.DbServices
{
    public class DbCustomersService : ICustomersService
    {
        private readonly UTCContext context;

        public DbCustomersService(UTCContext context)
        {
            this.context = context;
        }

        public void Add(Customer entity)
        {
            throw new NotImplementedException();
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
            return context.Customers.Find(id);
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
