using Altkom.UTC.Core.FakeServices;
using Altkom.UTC.Core.IServices;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Altkom.UTC.Core.ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {            

            Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} App Starting...");

            //ManyTask();

            //ManyTaskAsync();


            Console.WriteLine("next job");

            //  SyncTest();

            // TaskTest();

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancellationTokenSource.Token;
            
            IProgress<byte> progress = new Progress<byte>(step => Console.Write(step));

            try
            {
                await CalculateAsync(100, cancellationToken, progress);
            }
            catch (Exception e)
            {

            }


            Console.WriteLine("Press any key to cancel");


            // cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(3));
            //Console.ReadKey();

            //cancellationTokenSource.Cancel();




          //  AsyncAwaitTest();

            // decimal result = await AsyncAwaitTest();

            //Task<decimal> task1 = AsyncAwaitTest();
            // Task<decimal> task2 = AsyncAwaitTest();

            //Console.WriteLine("Do work...");

            // GetDevicesTest();

            // Task.WaitAny(task1, task2);


            Console.WriteLine("Press any key to exit.");

            Console.ReadKey();
        }

        private static decimal ManyTask()
        {
            Task<decimal> task1 = AsyncAwaitTest();
            Task<decimal> task2 = AsyncAwaitTest();

            Task.WaitAll(task1, task2);

            decimal result = task1.Result + task2.Result;

            return result;
        }


        private static async Task<decimal> ManyTaskAsync()
        {
            Task<decimal> task1 = AsyncAwaitTest();
            Task<decimal> task2 = AsyncAwaitTest();

            await Task.WhenAll(task1, task2);

            decimal result = task1.Result + task2.Result;

            return result;
        }



        private static void TaskTest2()
        {
            //Task<decimal> task = new Task<decimal>(() => Calculate(100));
            //task.Start();

            //Task<decimal> task2 = Task.Run(() => Calculate(100));
            //task2.ContinueWith(t => Console.WriteLine($"Result: {t.Result}"));

            //            Console.WriteLine("Hello");


            CalculateAsync(100)
                .ContinueWith(t => Console.WriteLine($"Result: {t.Result}"))
                    .ContinueWith(t => SendAsync($"send"));

            //  Console.WriteLine($"Result: {task2.Result}");
        }

        private static Task SendAsync(string message)
        {
            return Task.Run(() => Send(message));
        }

        private static void Send(string message)
        {
            Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} Sending...");

            Thread.Sleep(TimeSpan.FromSeconds(2));

            Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} Sent.");

        }

        private static void SyncTest()
        {
            decimal result = Calculate(100);
            Console.WriteLine($"Result: {result}");
        }

        private static void TaskTest()
        {
            CalculateAsync(100)
                .ContinueWith(t => Console.WriteLine($"Result: {t.Result}"));            
        }

        private static async Task<decimal> AsyncAwaitTest()
        {
            decimal result = await CalculateAsync(100).ConfigureAwait(false);

            decimal result2 = await CalculateAsync(result);

            Console.WriteLine($"Result: {result2}");

            return result2;
        }




        static Task<decimal> CalculateAsync(decimal amount, 
            CancellationToken cancellationToken = default,
            IProgress<byte> progress = null
            )
        {
            return Task.Run(() => Calculate(amount, cancellationToken, progress));
        }

        static decimal Calculate(decimal amount, 
            CancellationToken cancellationToken = default, 
            IProgress<byte> progress = null)
        {
            Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} Calculating...");

            for (byte i = 0; i < 10; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                progress.Report(i);

                //if (cancellationToken.IsCancellationRequested)
                //{
                //    return 0;
                //}

                // Console.Write(".");
                Thread.Sleep(TimeSpan.FromSeconds(1));

            }


            Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} Calculated.");

            return amount * 1.23m;
        }

        private static void GetDevicesTest()
        {             

            //IDevicesService devicesService = new FakeDevicesService(new FakeServices.Fakers.DeviceFaker());

            //var devices = devicesService.Get();

            //foreach (var device in devices)
            //{
            //    Console.WriteLine($"{device.Id} {device.Name} {device.Firmware} {device.IsActive}");
            //}
        }
    }
}
