using Pipelines.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pipelines.FileReader.Parsers
{
    public class StreamParser
    {
        public async Task<List<Sale>> Parse(string filePath)
        {
            return await ReadStream(filePath)
                .Skip(1)
                .Select(x =>
                {
                    var fields = x.Split(",");
                    return new Sale
                    {
                        Region = fields[0],
                        Country = fields[1],
                        ItemType = fields[2],
                        SalesChannel = fields[3],
                        OrderPriority = fields[4],
                        OrderDate = DateTime.Parse(fields[5]),
                        OrderId = fields[6],
                        ShipDate = DateTime.Parse(fields[7]),
                        UnitsSold = int.Parse(fields[8]),
                        UnitPrice = decimal.Parse(fields[9]),
                        UnitCost = decimal.Parse(fields[10]),
                        TotalRevenue = decimal.Parse(fields[11]),
                        TotalCost = decimal.Parse(fields[12]),
                        TotalProfit = decimal.Parse(fields[13])
                    };
                })
                .ToListAsync();
        }

        private async IAsyncEnumerable<string> ReadStream(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    yield return await reader.ReadLineAsync();
                }
            }
        }
    }
}