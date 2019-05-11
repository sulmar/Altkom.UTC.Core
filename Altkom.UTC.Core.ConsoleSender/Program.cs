﻿using Altkom.UTC.Core.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Altkom.UTC.Core.ConsoleSender
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Signal-R Sender");

            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;

            await SendTest();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        // dotnet add package Microsoft.AspNetCore.SignalR.Client
        private static async Task SendTest()
        {
            const string url = "http://localhost:5000/hubs/customers";

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            Console.WriteLine("Connecting...");

            await connection.StartAsync();

            Console.WriteLine("Connected.");

            

            while (true)
            {
                Customer customer = new Customer
                {
                    FirstName = "John",
                    LastName = "Smith"
                };

                await connection.SendAsync("CustomerAdded", customer);

                Console.WriteLine($"Sent {customer.FirstName} {customer.LastName}");

                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}
