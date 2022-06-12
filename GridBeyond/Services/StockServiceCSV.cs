using CsvHelper;
using GridBeyond.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace GridBeyond.Services
{
    public class StockServiceCSV: IStockService
    {
        public List<Stock> ReadCSV()
        {
            using var streamReader = new StreamReader(@"C:\Temp\sample.csv");
            
            using var csvReader = new CsvReader(streamReader, CultureInfo.GetCultureInfo("el-GR"));            
            var records = csvReader.GetRecords<Stock>().ToList();            
            return records;
        }

        public double? GetMaxExpensive(List<Stock> s)
        {            
            double? sum = double.MinValue;

            for (int i = 0; i < s.Count - 1; i++)
                if (s[i].MarketPrice + s[i+1].MarketPrice > sum)
                    sum = s[i].MarketPrice + s[i+1].MarketPrice;

            return sum;
        }


        double? IStockService.GetMin(List<Stock> s)
        {
            double? min = double.MaxValue;            

            foreach (Stock stock in s)
            {
                if (stock.MarketPrice < min)
                {                    
                    min = stock.MarketPrice;
                }
            }

            return min;
        }

        double? IStockService.GetMax(List<Stock> s)
        {
            double? max = double.MinValue;

            foreach (Stock stock in s)
            {
                if (stock.MarketPrice > max)
                {
                    max = stock.MarketPrice;
                }
            }

            return max;
        }

        double? IStockService.GetAverage(List<Stock> s)
        {
            double? sum = s.Sum(p => p.MarketPrice);
            return sum / s.Count;
        }
    }
}
