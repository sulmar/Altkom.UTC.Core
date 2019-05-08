using Altkom.UTC.Core.IServices;
using Altkom.UTC.Core.Models;
using Altkom.UTC.Core.Models.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Altkom.UTC.Core.RestApiServices
{
    public class RestApiCustomersService : ICustomersServiceAsync
    {

        private readonly HttpClient client;

        public RestApiCustomersService(HttpClient client)
        {
            this.client = client;            
        }

        public Task AddAsync(Customer entity)
        {
            return client.PostAsJsonAsync("api/customers", entity);
        }

        public async Task<IEnumerable<Customer>> GetAsync()
        {
            HttpResponseMessage response = await client.GetAsync("api/customers");

            // dotnet add package Microsoft.AspNet.WebApi.Client

            return await response.Content.ReadAsAsync<IEnumerable<Customer>>();

        }

        public Task<Customer> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
