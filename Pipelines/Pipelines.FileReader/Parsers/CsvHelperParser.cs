using CsvHelper;
using CsvHelper.Configuration;
using Pipelines.Data.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Pipelines.FileReader.Parsers
{
    public class CsvHelperParser
    {
        public List<Sale> Parse(string filePath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                return csv.GetRecords<Sale>().ToList();
            }
        }
    }
}