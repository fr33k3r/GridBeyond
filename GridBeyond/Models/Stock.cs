using CsvHelper.Configuration.Attributes;
using System;
using System.Collections;

namespace GridBeyond.Models
{
    public class Stock : IComparable
    {
        [Name("Date")]        
        public DateTime Date { get; set; }

        [Name("Market Price EX1")]
        public double? MarketPrice { get; set; }

        //Beginning of nested classes.
        //Nested class to do ascending sort on Date property.
        private class SortDateAscendingHelper : IComparer
        {
            int IComparer.Compare(object a, object b)
            {
                Stock s1 = (Stock)a;
                Stock s2 = (Stock)b;

                if (s1.Date > s2.Date)
                    return 1;

                if (s1.Date < s2.Date)
                    return -1;
                else
                    return 0;
            }
        }

        // Nested class to do descending sort on Date property.
        private class SortDateDescendingHelper : IComparer
        {
            int IComparer.Compare(object a, object b)
            {
                Stock s1 = (Stock)a;
                Stock s2 = (Stock)b;

                if (s1.Date < s2.Date)
                    return 1;

                if (s1.Date > s2.Date)
                    return -1;

                else
                    return 0;
            }
        }

        // Nested class to do ascending sort on MarketPrice property.
        private class SortMarketPriceAscendingHelper : IComparer
        {
            int IComparer.Compare(object a, object b)
            {
                Stock s1 = (Stock)a;
                Stock s2 = (Stock)b;

                if (s1.MarketPrice > s2.MarketPrice)
                    return 1;

                if (s1.MarketPrice < s2.MarketPrice)
                    return -1;
                else
                    return 0;
            }
        }

        // Nested class to do descending sort on MarketPrice property.
        private class SortMarketPriceDescendingHelper : IComparer
        {
            int IComparer.Compare(object a, object b)
            {
                Stock s1 = (Stock)a;
                Stock s2 = (Stock)b;

                if (s1.MarketPrice < s2.MarketPrice)
                    return 1;

                if (s1.MarketPrice > s2.MarketPrice)
                    return -1;

                else
                    return 0;

            }
        }
        // End of nested classes.


        // Implement IComparable CompareTo to provide default sort order.
        int IComparable.CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        // Method to return IComparer object for sort helper.
        public static IComparer SortDateAscending()
        {
            return (IComparer)new SortDateAscendingHelper();
        }
        // Method to return IComparer object for sort helper.
        public static IComparer SortDateDescending()
        {
            return (IComparer)new SortDateDescendingHelper();
        }
        public static IComparer SortMarketPriceAscending()
        {
            return (IComparer)new SortMarketPriceAscendingHelper();
        }

        // Method to return IComparer object for sort helper.
        public static IComparer SortMarketPriceDescending()
        {
            return (IComparer)new SortMarketPriceDescendingHelper();
        }
    }
}
