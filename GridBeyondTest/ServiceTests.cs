using System;
using GridBeyond.Models;
using GridBeyond.Services;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;

namespace GridBeyondTest
{
    [TestFixture]
    public class ServiceTests
    {
        Mock<IStockService> mockStockService;
        IStockService systemUnderTest;

        [Test]
        public void MaxExpensive_Is_Calculated_Correctly()
        {            
            List<Stock> s = CreateSampleList();

            double? expectedResult = 105;

            mockStockService = new Mock<IStockService>(MockBehavior.Strict);
            mockStockService.Setup(p => p.ReadCSV()).Returns(s);
            mockStockService.Setup(p => p.GetMaxExpensive(s)).Returns(expectedResult);

            systemUnderTest = mockStockService.Object;
            var result = systemUnderTest.GetMaxExpensive(s);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Max_Is_Calculated_Correctly()
        {
            List<Stock> s = CreateSampleList();            

            double? expectedResult = 55.0;


            mockStockService = new Mock<IStockService>(MockBehavior.Strict);
            mockStockService.Setup(p => p.ReadCSV()).Returns(s);
            mockStockService.Setup(p => p.GetMax(s)).Returns(expectedResult);

            systemUnderTest = mockStockService.Object;
            var result = systemUnderTest.GetMax(s);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Min_Is_Calculated_Correctly()
        {
            List<Stock> s = CreateSampleList();

            double? expectedResult = 42.4;


            mockStockService = new Mock<IStockService>(MockBehavior.Strict);
            mockStockService.Setup(p => p.ReadCSV()).Returns(s);
            mockStockService.Setup(p => p.GetMin(s)).Returns(expectedResult);

            systemUnderTest = mockStockService.Object;
            var result = systemUnderTest.GetMin(s);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Average_Is_Calculated_Correctly()
        {
            List<Stock> s = CreateSampleList();

            double? expectedResult = 48.16;


            mockStockService = new Mock<IStockService>(MockBehavior.Strict);
            mockStockService.Setup(p => p.ReadCSV()).Returns(s);
            mockStockService.Setup(p => p.GetAverage(s)).Returns(expectedResult);

            systemUnderTest = mockStockService.Object;
            var result = systemUnderTest.GetAverage(s);

            Assert.AreEqual(expectedResult, result);
        }

        List<Stock> CreateSampleList()
        {
            List<Stock> s = new List<Stock>();

            s.Add(new Stock
            {
                Date = new DateTime(100),
                MarketPrice = 50
            });
            s.Add(new Stock
            {
                Date = new DateTime(150),
                MarketPrice = 55
            });
            s.Add(new Stock
            {
                Date = new DateTime(200),
                MarketPrice = 45
            });
            s.Add(new Stock
            {
                Date = new DateTime(300),
                MarketPrice = 48.4
            });
            s.Add(new Stock
            {
                Date = new DateTime(500),
                MarketPrice = 42.4
            });

            return s;
        }
    }    
}
