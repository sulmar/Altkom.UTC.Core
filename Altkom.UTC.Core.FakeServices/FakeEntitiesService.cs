using Altkom.UTC.Core.IServices;
using Altkom.UTC.Core.Models;
using Bogus;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Altkom.UTC.Core.FakeServices
{

    public class MyOptions
    {
        public int Quantity { get; set; }
    }

    public class FakeEntitiesService<T> : IEntiesService<T>
        where T : Base
    {
        protected readonly ICollection<T> entites;

        private readonly MyOptions options;

        // dotnet add package Microsoft.Extensions.Options
        //public FakeEntitiesService(IOptions<MyOptions> options, Faker<T> faker)
        //{
        //    this.options = options.Value;

        //    entites = faker.Generate(this.options.Quantity);
        //}

        public FakeEntitiesService(MyOptions options, Faker<T> faker)
        {
            this.options = options;

            entites = faker.Generate(this.options.Quantity);

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
