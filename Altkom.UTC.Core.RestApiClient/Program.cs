using Altkom.UTC.Core.FakeServices.Fakers;
using Altkom.UTC.Core.IServices;
using Altkom.UTC.Core.Models;
using Altkom.UTC.Core.RestApiServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Altkom.UTC.Core.RestApiClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            await GetCustomersAsyncTest();

            await GetCustomersTest();

            await AddCustomerTest();

            Console.WriteLine("Press any key to exit.");

            Console.ReadKey();
        }

        private static async Task GetCustomersAsyncTest()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001");

            ICustomersServiceAsync customersService = new RestApiCustomersService(client);

            var customers = await customersService.GetAsync();

            foreach (var customer in customers)
            {
                Console.WriteLine(customer.Email);
            }
        }

        private static async Task AddCustomerTest()
        {
            CustomerFaker customerFaker = new CustomerFaker();
            Customer customer = customerFaker.Generate();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001");

                HttpResponseMessage response = await client.PostAsJsonAsync("api/customers", customer);

                string content = await response.RequestMessage.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    
                }

            }
        }

        private static async Task GetCustomersTest()
        {
            using (HttpClient client = new HttpClient())
            {
               client.BaseAddress = new Uri("https://localhost:5001");

               HttpResponseMessage response = await client.GetAsync("api/customers");

                if (response.IsSuccessStatusCode)
                {
                    //string content = await response.Content.ReadAsStringAsync();

                    //Console.WriteLine(content);

                    // dotnet add package Newtonsoft.Json
                    // var customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(content);

                    // dotnet add package Microsoft.AspNet.WebApi.Client

                    var customers = await response.Content.ReadAsAsync<IEnumerable<Customer>>();

                    foreach (var customer in customers)
                    {
                        Console.WriteLine(customer.FirstName);
                    }
                }
            }

           

        }
    }
}
