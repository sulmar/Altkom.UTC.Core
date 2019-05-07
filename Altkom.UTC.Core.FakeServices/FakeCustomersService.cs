using System.Collections.Generic;
using System.Linq;
using Altkom.UTC.Core.FakeServices.Fakers;
using Altkom.UTC.Core.IServices;
using Altkom.UTC.Core.Models;
using Altkom.UTC.Core.Models.SearchCriteria;

namespace Altkom.UTC.Core.FakeServices
{
    public class FakeCustomersService : FakeEntitiesService<Customer>,
        ICustomersService
    {
        public FakeCustomersService(CustomerFaker faker) : base(faker)
        {
        }

        public IEnumerable<Customer> Get(CustomerSearchCriteria searchCriteria)
        {
            IQueryable<Customer> query = entites.AsQueryable();

            if (searchCriteria.From.HasValue)
            {
               query = query.Where(e => e.Birthday >= searchCriteria.From.Value);
            }

            if (searchCriteria.To.HasValue)
            {
                query = query.Where(e => e.Birthday <= searchCriteria.To.Value);
            }

            return query.ToList();


        }

        public override void Remove(int id)
        {
            Get(id).IsDeleted = true;
        }
    }
}
