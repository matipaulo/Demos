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

            WithLoop(cancellationTokenSource);

            Console.WriteLine("Hello World!");
        }

        private static void WithLoop(CancellationTokenSource cancellationTokenSource)
        {
            throw new NotImplementedException();
        }
    }
}