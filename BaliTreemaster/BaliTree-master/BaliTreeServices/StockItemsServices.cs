using BaliTreeData;
using System;
using System.Collections.Generic;
using System.Text;
using BaliTreeData.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BaliTreeServices
{
    public class StockItemsServices : IStockItems
    {
        private BaliTreeContext _context;

        public StockItemsServices (BaliTreeContext context)
        {
            _context = context;
        }

        public IEnumerable<StockItem> GetAll()
        {
            return _context.StockItems
                .Include(x=>x.ItemType)
                .ToList();
        }

        public StockItem GetItemByID(int Id)
        {
            return GetAll()
                .FirstOrDefault(item => item.Id == Id);
        }

        public IEnumerable<StockItem> ItemsOfType(StockType stockType)
        {
            return _context.StockItems
                .Where(x => x.ItemType == stockType)
                .ToList();
        }
    }
}
