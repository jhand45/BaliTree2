using BaliTreeData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaliTreeData
{
    public interface IAnalytics
    {
        decimal AverageCost (IEnumerable<StockItem> items);
        int InStock(IEnumerable<StockItem> items);
        decimal RRP(decimal AverageCost);


    }
}
