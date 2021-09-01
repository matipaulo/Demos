using BenchmarkDotNet.Running;

namespace Pipelines.Benchmarks
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var filePath = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)}\\Data\\100000_Sales_Records.csv";
            //var parser = new PipelineParser();
            //await parser.Parse(filePath);

            BenchmarkRunner.Run<Benchy>();
        }
    }
}