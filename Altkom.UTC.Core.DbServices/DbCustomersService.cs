using Altkom.UTC.Core.IServices;
using Altkom.UTC.Core.Models;
using Altkom.UTC.Core.Models.SearchCriteria;
using System;
using System.Collections.Generic;

namespace Altkom.UTC.Core.DbServices
{
    public class DbCustomersService : ICustomersService
    {
        public void Add(Customer entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> Get(CustomerSearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> Get()
        {
            throw new NotImplementedException();
        }

        public Customer Get(int id)
        {
            throw new NotImplementedException();
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
