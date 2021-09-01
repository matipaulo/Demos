using Pipelines.Data.Models;
using System;
using System.Buffers;
using System.IO;
using System.IO.Pipelines;
using System.Text;
using System.Threading.Tasks;

namespace Pipelines.FileReader.Parsers
{
    public class PipelineParser
    {
        private readonly byte[] _header;
        private readonly byte _separator = (byte)',';

        public PipelineParser()
        {
            _header =
                Encoding.UTF8.GetBytes(
                    "Region,Country,ItemType,SalesChannel,OrderPriority,OrderDate,OrderId,ShipDate,UnitsSold,UnitPrice,UnitCost,TotalRevenue,TotalCost,TotalProfit");
        }

        public async Task<Sale[]> Parse(string filePath)
        {
            var salesPool = ArrayPool<Sale>.Shared;
            var sales = salesPool.Rent(100000);
            int position = 0;
            await using (var fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                var reader = PipeReader.Create(fileStream);
                while (true)
                {
                    var data = await reader.ReadAsync();
                    var dataBuffer = data.Buffer;
                    // Parse

                    var actualPosition = ParseLine(dataBuffer, position, sales);
                    reader.AdvanceTo(actualPosition, dataBuffer.End);

                    if (data.IsCompleted)
                        break;
                }

                await reader.CompleteAsync();
            }

            salesPool.Return(sales); // This should be our last step, but for demo purposes we can keep it where it is.

            return sales;
        }

        private SequencePosition ParseLine(ReadOnlySequence<byte> dataBuffer, int position, Sale[] sales)
        {
            var reader = new SequenceReader<byte>(dataBuffer);
            while (reader.TryReadTo(out ReadOnlySpan<byte> line, (byte)'\n'))
            {
                Sale sale = GetSale(line);
                if (sale != null)
                {
                    sales[position] = sale;
                    position++;
                }
            }

            return reader.Position;
        }

        private Sale GetSale(ReadOnlySpan<byte> line)
        {
            if (line.IndexOf(_header) >= 0)
                return null;

            var record = new Sale();

            for (int i = 0; i < 14; i++)
            {
                var index = line.IndexOf(_separator);
                if (index < 0)
                {
                    index = line.Length;
                }

                switch (i)
                {
                    case 0:
                        record.Region = Encoding.UTF8.GetString(line.Slice(0, index));
                        break;

                    case 1:
                        record.Country = Encoding.UTF8.GetString(line.Slice(0, index));
                        break;

                    case 2:
                        record.ItemType = Encoding.UTF8.GetString(line.Slice(0, index));
                        break;

                    case 3:
                        record.SalesChannel = Encoding.UTF8.GetString(line.Slice(0, index));
                        break;

                    case 4:
                        record.OrderPriority = Encoding.UTF8.GetString(line.Slice(0, index));
                        break;

                    case 5:
                        record.OrderDate = DateTime.Parse(Encoding.UTF8.GetString(line.Slice(0, index)));
                        break;

                    case 6:
                        record.OrderId = Encoding.UTF8.GetString(line.Slice(0, index));
                        break;

                    case 7:
                        record.ShipDate = DateTime.Parse(Encoding.UTF8.GetString(line.Slice(0, index)));
                        break;

                    case 8:
                        record.UnitsSold = int.Parse(Encoding.UTF8.GetString(line.Slice(0, index)));
                        break;

                    case 9:
                        record.UnitPrice = decimal.Parse(Encoding.UTF8.GetString(line.Slice(0, index)));
                        break;

                    case 10:
                        record.UnitCost = decimal.Parse(Encoding.UTF8.GetString(line.Slice(0, index)));
                        break;

                    case 11:
                        record.TotalRevenue = decimal.Parse(Encoding.UTF8.GetString(line.Slice(0, index)));
                        break;

                    case 12:
                        record.TotalCost = decimal.Parse(Encoding.UTF8.GetString(line.Slice(0, index)));
                        break;

                    case 13:
                        record.TotalProfit = decimal.Parse(Encoding.UTF8.GetString(line.Slice(0, index)));

                        return record;
                }

                line = line.Slice(index + 1);
            }

            return record;
        }
    }
}