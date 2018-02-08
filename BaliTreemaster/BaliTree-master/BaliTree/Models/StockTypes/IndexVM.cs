using BaliTreeData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaliTree.Models.StockTypes
{
    public class IndexVM
    {
        public IEnumerable<StockType> TypesToList { get; set; }
        public List<SubIndexVM> SubIndex { get; set; }

        //public StockType Type { get; set; }
        //public IEnumerable<StockItem> ItemsOfType { get; set; }
        //public decimal AverageCost { get; set; }
        //public decimal RRP { get; set; }
        //public int InStock { get; set; }
    }
}
