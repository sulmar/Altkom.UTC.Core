using Altkom.UTC.Core.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.UTC.Core.ConsoleReceiver
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Signal-R Receiver");

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            await ReceiveTest();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }


        // dotnet add package Microsoft.AspNetCore.SignalR.Client
        private static async Task ReceiveTest()
        {
            const string url = "http://localhost:5000/hubs/customers";

            var username = "marcin";
            var password = "12345";

            var credentialBytes = Encoding.UTF8.GetBytes($"{username}:{password}");
            var credentials = Convert.ToBase64String(credentialBytes);

            string parameter = $"Basic {credentials}";

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url, options => options.Headers.Add("Authorization", parameter))
                .Build();

            Console.WriteLine("Connecting...");

            await connection.StartAsync();

            Console.WriteLine("Connected.");

            connection.On<Customer>("Added",
                customer => Console.WriteLine($"Received customer {customer.FirstName} {customer.LastName}"));
        }
    }
}
