using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace DomainLogic
{
    public static class TopProductWorkflow
    {
        public static string WorkflowName() => "Top Product";

        public static string Process(string country)
        {
            return null;
        }

        public static IEnumerable<LastYearSalesData> GetSalesData(string country)
        {
            using (var reader = new StreamReader($"Data/{country}.csv"))
            using (var csv = new CsvReader(reader))
            {
                return csv.GetRecords<LastYearSalesData>().ToList(); // To make sure all data are read
            }
        }

        public static LastYearSalesData GetTop(this IEnumerable<LastYearSalesData> salesData)
        {
            return null;
        }


    }

    public class LastYearSalesDataWithEstimate
    {
        
    }

    public class LastYearSalesData
    {
        public string ProductName { get; set;  }

        public int Month1 { get; set; }

        public int Month2 { get; set; }

        public int Month3 { get; set; }

        public int Month4 { get; set; }

        public int Month5 { get; set; }

        public int Month6 { get; set; }

        public int Month7 { get; set; }

        public int Month8 { get; set; }

        public int Month9 { get; set; }

        public int Month10 { get; set; }

        public int Month11 { get; set; }

        public int Month12 { get; set; }

    }

}