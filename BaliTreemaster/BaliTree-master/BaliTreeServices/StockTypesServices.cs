using BaliTreeData;
using BaliTreeData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaliTreeServices
{
    public class StockTypesServices : IStockTypes
    {
        private BaliTreeContext _context;

        public StockTypesServices(BaliTreeContext context)
        {
            _context = context;
        }

        public IEnumerable<StockType> GetAllTypes()
        {
            return _context.StockTypes.ToList();
        }

        public StockType GetTypeByID(int Id)
        {
            return GetAllTypes()
            .FirstOrDefault(type => type.Id == Id);
        }
    }
}
