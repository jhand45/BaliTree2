using BaliTreeData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaliTree.Models.StockTypes
{
    public class SubIndexVM
    {
        public IndexVM IndexVM { get; set; }
        public StockType StockType { get; set; }
        public IEnumerable<StockItem> ItemsOfType { get; set; }
        public decimal AverageCost { get; set; }
        public decimal RRP { get; set; }
        public int InStock { get; set; }
    }
}
