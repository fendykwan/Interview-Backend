using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moduit_BE.Models
{
    public class Item
    {
        public int id { get; set; }
        public int category { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string footer { get; set; }
        public IEnumerable<string> tags { get; set; }
        public DateTime? createdAt { get; set; }
    }

    public class CategoryItem
    {
        public int id { get; set; }
        public int category { get; set; }
        public IEnumerable<Item> items { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
