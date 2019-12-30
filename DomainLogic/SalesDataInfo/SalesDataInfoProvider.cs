using System.Linq;
using DomainLogic.Data;

namespace DomainLogic.SalesDataInfo
{
    public static class SalesDataInfoProvider
    {
        public static SalesDatum GetTopPerformance(Product product)
            => product.SalesData.OrderByDescending(sales => sales.Amount).First();

        public static SalesDatum GetWorstPerformance(Product product)
            => product.SalesData.OrderBy(sales => sales.Amount).First();
    }
}