﻿using BaliTreeData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaliTree.Models.Event
{
    public class BrokenVM
    {
        public StockEvent stockEvent { get; set; }
        public IEnumerable<StockItem> StockItems { get; set; }
        public int stockId { get; set; }
    }
}
