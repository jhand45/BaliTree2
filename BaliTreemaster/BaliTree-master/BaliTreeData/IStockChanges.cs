using BaliTreeData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaliTreeData
{
    public interface IStockChanges
    {
        int Recieved (StockEvent stockEvent);
        int Sold(StockEvent stockEvent);
        int Broken(StockEvent stockEvent);
    }
}
