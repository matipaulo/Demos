using System;
using System.Threading;
using System.Threading.Tasks;

namespace LongTask
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();

            Task.Run(() =>
            {
                if (Console.ReadKey().Key == ConsoleKey.C)
                {
                    cancellationTokenSource.Cancel();
                    Console.WriteLine("Task canceled.");
                }
            });
            
            await WithLoop(cancellationTokenSource);
            //await WithLoopImmediately(cancellationTokenSource);
            //await WithException(cancellationTokenSource);
        }

        private static async Task WithException(CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                while (true)
                {
                    cancellationTokenSource.Token.ThrowIfCancellationRequested();
                    Console.WriteLine("Task running.");
                    await Task.Delay(2000);
                }
            }
            catch (Exception e)
            {   
                Console.WriteLine(e);
            }
        }

        private static async Task WithLoopImmediately(CancellationTokenSource cancellationTokenSource)
        {
            while (!cancellationTokenSource.IsCancellationRequested)
            {
                Console.WriteLine("Task running.");
                await Task.Delay(2000, cancellationTokenSource.Token);
            }
            
            Console.WriteLine("The operation was canceled.");
            
            cancellationTokenSource.Dispose();
        }

        private static async Task WithLoop(CancellationTokenSource cancellationTokenSource)
        {
            while (!cancellationTokenSource.IsCancellationRequested)
            {
                Console.WriteLine("Task running.");
                await Task.Delay(2000);
            }
            
            Console.WriteLine("The operation was canceled.");
            
            cancellationTokenSource.Dispose();
        }
    }
}