using BaliTreeData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaliTreeData
{
    public interface IStockTypes
    {
        IEnumerable<StockType> GetAllTypes();
        StockType GetTypeByID(int Id);
    }
}
