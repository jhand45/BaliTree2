using BaliTreeData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaliTreeData
{
    public interface IStockItems
    {
        IEnumerable<StockItem> GetAll();
        IEnumerable<StockItem> ItemsOfType(StockType stockType);

        StockItem GetItemByID(int Id);
        
    }
}
