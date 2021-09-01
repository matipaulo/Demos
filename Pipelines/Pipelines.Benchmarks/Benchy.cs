using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Pipelines.Data.Models;
using Pipelines.FileReader.Parsers;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Pipelines.Benchmarks
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class Benchy
    {
        private string _filePath;

        [GlobalSetup]
        public void Setup()
        {
            _filePath = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)}\\Data\\100000_Sales_Records.csv";
        }

        [Benchmark]
        public List<Sale> CsvHelperParser()
        {
            var parser = new CsvHelperParser();
            return parser.Parse(_filePath);
        }

        [Benchmark(Baseline = true)]
        public async Task<List<Sale>> StreamParser()
        {
            var parser = new StreamParser();
            return await parser.Parse(_filePath);
        }

        [Benchmark]
        public async Task<Sale[]> PipelinesParser()
        {
            var parser = new PipelineParser();
            return await parser.Parse(_filePath);
        }
    }
}