using BaliTreeData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaliTree.Models.Event
{
    public class IndexVM
    {
        public IEnumerable<StockEvent> AllEvents { get; set; }
    }
}
