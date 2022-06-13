using GridBeyond.Models;
using GridBeyond.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChartJSCore.Models;
using ChartJSCore.Helpers;
using System.IO;

namespace GridBeyond.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStockService _stockService;

        public HomeController(IStockService stockService)
        {
            _stockService = stockService;
        }

        public IActionResult Index()
        {
            return View();
        }
            public IActionResult TableClientSide()
        {   try
            {
                List<Stock> stocks = _stockService.ReadCSV();
                StockAggregates stockAggregates = new()
                {
                    Min = _stockService.GetMin(stocks),
                    Max = _stockService.GetMax(stocks),
                    Average = _stockService.GetAverage(stocks),
                    MaxExpensive = _stockService.GetMaxExpensive(stocks)
                };

                ViewData["aggregates"] = stockAggregates;

                return View(stocks);
            }
            catch (IOException ioex)
            {
                string message = ioex.Message;
                var errorViewModel = new ErrorViewModel
                {
                    ErrorMsg = message
                };

                return View("Error", errorViewModel);                
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                var errorViewModel = new ErrorViewModel
                {
                    ErrorMsg = message
                };

                return View("Error", errorViewModel);
            }
        }

        public IActionResult TableServerSide()
        {
            try
            {
                List<Stock> stocks = _stockService.ReadCSV();
                StockAggregates stockAggregates = new()
                {
                    Min = _stockService.GetMin(stocks),
                    Max = _stockService.GetMax(stocks),
                    Average = _stockService.GetAverage(stocks),
                    MaxExpensive = _stockService.GetMaxExpensive(stocks)
                };

                ViewData["aggregates"] = stockAggregates;

                return View();
            }
            catch (IOException ioex)
            {
                string message = ioex.Message;
                var errorViewModel = new ErrorViewModel
                {
                    ErrorMsg = message
                };

                return View("Error", errorViewModel);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                var errorViewModel = new ErrorViewModel
                {
                    ErrorMsg = message
                };

                return View("Error", errorViewModel);
            }            
        }


        public IActionResult GetData(JqueryDatatableParam param)
        {
            try
            {
                List<Stock> stocks = _stockService.ReadCSV();
                StockAggregates stockAggregates = new()
                {
                    Min = _stockService.GetMin(stocks),
                    Max = _stockService.GetMax(stocks),
                    Average = _stockService.GetAverage(stocks),
                    MaxExpensive = _stockService.GetMaxExpensive(stocks)
                };

                ViewData["aggregates"] = stockAggregates;

                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    stocks = stocks.Where(x => x.Date.ToString().Contains(param.sSearch) || x.MarketPrice.ToString().Contains(param.sSearch)).ToList();
                }

                var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Query["iSortCol_0"]);
                var sortDirection = HttpContext.Request.Query["sSortDir_0"];
                if (sortColumnIndex == 0)
                {
                    stocks = sortDirection == "asc" ? stocks.OrderBy(c => c.Date).ToList() : stocks.OrderByDescending(c => c.Date).ToList();
                }
                else if (sortColumnIndex == 1)
                {
                    stocks = sortDirection == "asc" ? stocks.OrderBy(c => c.MarketPrice).ToList() : stocks.OrderByDescending(c => c.MarketPrice).ToList();
                }

                var displayResult = stocks.Skip(param.iDisplayStart)
               .Take(param.iDisplayLength).ToList();
                var totalRecords = stocks.Count;

                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords,
                    aaData = displayResult
                });
            }
            catch (IOException ioex)
            {
                string message = ioex.Message;
                var errorViewModel = new ErrorViewModel
                {
                    ErrorMsg = message
                };

                return View("Error", errorViewModel);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                var errorViewModel = new ErrorViewModel
                {
                    ErrorMsg = message
                };

                return View("Error", errorViewModel);
            }
        }

        public IActionResult Chart()
        {
            
            //List<Stock> stocks = _stockService.ReadCSV();

            //Chart chart = new Chart();            

            //chart.Type = Enums.ChartType.Line;

            //Data data = new()
            //{
            //    Labels = new List<string>()                
            //};

            //foreach (Stock stock in stocks)
            //{
            //    data.Labels.Add(stock.Date.ToString());
            //}            

            //List<double?> stockData = new();
            //foreach (Stock stock in stocks)
            //{
            //    stockData.Add(stock.MarketPrice);
            //}

            //LineDataset dataset = new()
            //{
            //    Label = "Stocks Line Chart",                
            //    Data = stockData,
            //    Fill = "false",
            //    Tension = 0.1,
            //    BackgroundColor = new List<ChartColor> { ChartColor.FromRgba(75, 192, 192, 0.4) },
            //    BorderColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
            //    BorderCapStyle = "butt",
            //    BorderDash = new List<int> { },
            //    BorderDashOffset = 0.0,
            //    BorderJoinStyle = "miter",
            //    PointBorderColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
            //    PointBackgroundColor = new List<ChartColor> { ChartColor.FromHexString("#ffffff") },
            //    PointBorderWidth = new List<int> { 1 },
            //    PointHoverRadius = new List<int> { 5 },
            //    PointHoverBackgroundColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
            //    PointHoverBorderColor = new List<ChartColor> { ChartColor.FromRgb(220, 220, 220) },
            //    PointHoverBorderWidth = new List<int> { 2 },
            //    PointRadius = new List<int> { 1 },
            //    PointHitRadius = new List<int> { 10 },
            //    SpanGaps = false                
            //};

            //data.Datasets = new List<Dataset>
            //{
            //    dataset
            //};

            //chart.Data = data;

            //ViewData["chart"] = chart;

            return View();
           
        }

        [HttpPost]
        public IActionResult Chart(string start, string end)
        {
            try
            {
                DateTime startDate = DateTime.MinValue;
                DateTime endDate = DateTime.MaxValue;

                if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
                {
                    startDate = DateTime.Parse(start);
                    endDate = DateTime.Parse(end);
                }

                List<Stock> stocks = _stockService.ReadCSV();

                AjaxDates ajaxDates = new()
                {
                    startDate = startDate,
                    endDate = endDate
                };

                Chart newChart = new Chart();

                newChart.Type = Enums.ChartType.Line;

                List<double?> stockData = new();

                Data data = new()
                {
                    Labels = new List<string>()
                };

                foreach (Stock stock in stocks)
                {
                    if (stock.Date >= ajaxDates.startDate && stock.Date <= ajaxDates.endDate)
                    {
                        data.Labels.Add(stock.Date.ToString());
                        stockData.Add(stock.MarketPrice);
                    }
                }

                LineDataset dataset = new()
                {
                    Label = "Stocks Line Chart",
                    Data = stockData,
                    Fill = "false",
                    Tension = 0.1,
                    BackgroundColor = new List<ChartColor> { ChartColor.FromRgba(75, 192, 192, 0.4) },
                    BorderColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                    BorderCapStyle = "butt",
                    BorderDash = new List<int> { },
                    BorderDashOffset = 0.0,
                    BorderJoinStyle = "miter",
                    PointBorderColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                    PointBackgroundColor = new List<ChartColor> { ChartColor.FromHexString("#ffffff") },
                    PointBorderWidth = new List<int> { 1 },
                    PointHoverRadius = new List<int> { 5 },
                    PointHoverBackgroundColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                    PointHoverBorderColor = new List<ChartColor> { ChartColor.FromRgb(220, 220, 220) },
                    PointHoverBorderWidth = new List<int> { 2 },
                    PointRadius = new List<int> { 1 },
                    PointHitRadius = new List<int> { 10 },
                    SpanGaps = false
                };

                data.Datasets = new List<Dataset>
            {
                dataset
            };

                newChart.Data = data;

                ViewData["chart"] = newChart;

                return Ok(Json(newChart));
            }
            catch (IOException ioex)
            {
                string message = ioex.Message;
                var errorViewModel = new ErrorViewModel
                {
                    ErrorMsg = message
                };

                return View("Error", errorViewModel);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                var errorViewModel = new ErrorViewModel
                {
                    ErrorMsg = message
                };

                return View("Error", errorViewModel);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string message)
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                ErrorMsg = message
            }); 
        }
    }
}
