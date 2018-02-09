using BaliTreeData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaliTreeData
{
    public interface IStockEvents
    {
        IEnumerable<StockEvent> GetAll();
    }
}
