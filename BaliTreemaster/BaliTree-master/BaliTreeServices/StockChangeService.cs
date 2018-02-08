using System;
using System.Collections.Generic;
using System.Text;
using BaliTreeData;
using BaliTreeData.Models;

namespace BaliTreeServices
{
    public class StockChangeService : IStockChanges
    {
        public static int Add(int Existing, int ToAdd)
        {
            return (Existing + ToAdd);
        }

        public static int Subtract(int Existing, int ToRemove)
        {
            return (Existing - ToRemove);
        }

        public int Broken(StockEvent stockEvent)
        {
            return Subtract(stockEvent.StockItem.InStock, stockEvent.Change);
        }

        public int Recieved(StockEvent stockEvent)
        {
            return Add(stockEvent.StockItem.InStock, stockEvent.Change);
        }

        public int Sold(StockEvent stockEvent)
        {
            return Subtract(stockEvent.StockItem.InStock, stockEvent.Change); 
        }
    }
}