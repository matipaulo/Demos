using BenchmarkDotNet.Running;

namespace RecyclableMemoryStreamDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchy>();
        }
    }
}