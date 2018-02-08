using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BaliTreeData.Models
{
    public class StockItem
    {
        public int Id { get; set; }

        [DisplayName("Item Name")]
        public string ItemName { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMMM dd yyyy}")]
        [DefaultValue("")]
        public DateTime? Date { get; set; }

        [DisplayName("Cost Price")]
        public decimal CostPrice { get; set; }

        [DisplayName("Amount In Stock")]
        public int InStock { get; set; }

        public StockType ItemType { get; set; } //each item is of a single type

        public IEnumerable<StockEvent> Event { get; set; } //each item has many events
    }
}
