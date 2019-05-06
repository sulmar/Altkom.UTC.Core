using Altkom.UTC.Core.FakeServices;
using Altkom.UTC.Core.IServices;
using System;

namespace Altkom.UTC.Core.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IDevicesService devicesService = new FakeDevicesService();

            var devices = devicesService.Get();

            foreach (var device in devices)
            {
                Console.WriteLine($"{device.Id} {device.Name} {device.Firmware} {device.IsActive}");
            }

            Console.WriteLine("Press any key to exit.");

            Console.ReadKey();
        }
    }
}
