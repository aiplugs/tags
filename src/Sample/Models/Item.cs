using System.Collections.Generic;

namespace Sample
{
    public class Item {
        public string Title { get; set; }
        public List<Item> Children { get; set; } = new List<Item>();
    }
}