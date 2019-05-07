using Altkom.UTC.Core.IServices;
using Altkom.UTC.Core.Models;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Altkom.UTC.Core.FakeServices
{

    public class FakeEntitiesService<T> : IEntiesService<T>
        where T : Base
    {
        protected readonly ICollection<T> entites;

        public FakeEntitiesService(Faker<T> faker)
        {
            entites = faker.Generate(100);
        }

        public virtual void Add(T entity) => entites.Add(entity);

        public virtual IEnumerable<T> Get() => entites;

        public virtual T Get(int id) => entites.SingleOrDefault(d => d.Id == id);

        public virtual void Remove(int id)
        {
            T entity = Get(id);

            entites.Remove(entity);
        }

        public virtual void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
