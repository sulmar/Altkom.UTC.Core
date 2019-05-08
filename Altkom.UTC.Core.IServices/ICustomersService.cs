using Altkom.UTC.Core.Models;
using Altkom.UTC.Core.Models.SearchCriteria;
using System;
using System.Collections.Generic;

namespace Altkom.UTC.Core.IServices
{
    public interface ICustomersService : IEntiesService<Customer>
    {

        //  IEnumerable<Customer> Get(DateTime from, DateTime to, string firstName);

        IEnumerable<Customer> Get(CustomerSearchCriteria searchCriteria);
    }



    public interface ICustomersServiceAsync : IEntiesServiceAsync<Customer>
    {

    }


}
