using Altkom.UTC.Core.FakeServices.Fakers;
using Altkom.UTC.Core.IServices;
using Altkom.UTC.Core.Models;

namespace Altkom.UTC.Core.FakeServices
{
    public class FakeCustomersService : FakeEntitiesService<Customer>,
        ICustomersService
    {
        public FakeCustomersService(CustomerFaker faker) : base(faker)
        {
        }

        public override void Remove(int id)
        {
            Get(id).IsDeleted = true;
        }
    }
}
