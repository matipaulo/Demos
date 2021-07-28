using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Microsoft.IO;

namespace RecyclableMemoryStreamDemo
{
    [MemoryDiagnoser]
    public class Benchy
    {
        private RecyclableMemoryStreamManager _memoryStreamManager;
        private List<User> _users;

        [GlobalSetup]
        public void Setup()
        {
            _memoryStreamManager = new RecyclableMemoryStreamManager();
            _users = PersonGenerator.Generate();
        }
        
        [Benchmark]
        public async Task SerializeRecyclableMemoryManager()
        {
            using (var stream = _memoryStreamManager.GetStream("memory"))
            {
                using (var gzip = new GZipStream(stream, CompressionLevel.Optimal, true))
                {
                    await JsonSerializer.SerializeAsync(gzip, _users);
                    await gzip.FlushAsync();
                }
            }
        }

        [Benchmark]
        public async Task SerializeMemoryStream()
        {
            using (var stream = new MemoryStream())
            {
                using (var gzip = new GZipStream(stream, CompressionLevel.Optimal, true))
                {
                    await JsonSerializer.SerializeAsync(gzip, _users);
                    await gzip.FlushAsync();
                }
            }
        }
    }
}