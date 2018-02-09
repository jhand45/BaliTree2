using BaliTreeData.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BaliTreeData;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BaliTreeServices
{
    public class StockEventsService : IStockEvents
    {
        private BaliTreeContext _context;

        public StockEventsService(BaliTreeContext context)
        {
            _context = context;
        }


        public IEnumerable<StockEvent> GetAll()
        {
            return _context.StockEvents
                .Include(x => x.StockItem)
                .Include(x=>x.StockItem.ItemType)
                .ToList();
        }
    }
}
