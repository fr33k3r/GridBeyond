using GridBeyond.Models;
using System.Collections.Generic;

namespace GridBeyond.Services
{
    public interface IStockService
    {
        List<Stock> ReadCSV();        

        double? GetMaxExpensive(List<Stock> s);
        double? GetMin(List<Stock> s);
        double? GetMax(List<Stock> s);

        double? GetAverage(List<Stock> s);
    }
}
