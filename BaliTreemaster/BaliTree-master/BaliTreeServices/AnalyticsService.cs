using BaliTreeData;
using BaliTreeData.Models;
using System;
using System.Collections.Generic;

namespace BaliTreeServices
{
    public class AnalyticsService : IAnalytics
    {
        public decimal AverageCost(IEnumerable<StockItem> items)
        {
            decimal cost = 0;
            int count = 0;

            foreach(var item in items)
            {
                cost += item.CostPrice;
                count += 1;
            }

            return (cost / count);
        }

        public int InStock(IEnumerable<StockItem> items)
        {
            int count = 0;
            foreach(var item in items)
            {
                count += item.InStock;
            }

            return count;
        }

        public decimal RRP(decimal AverageCost)
        {
            return AverageCost * 2;
        }
    }
}
