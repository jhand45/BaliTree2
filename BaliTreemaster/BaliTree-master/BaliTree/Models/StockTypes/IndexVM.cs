﻿using BaliTreeData.Models;
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
    }
}
