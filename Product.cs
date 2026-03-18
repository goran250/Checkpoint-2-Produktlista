using System;

namespace Checkpoint_2_Produktlista
{
    class Product
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public bool HighlightThis { get; set; }

        public Product()
        {
        }

        public Product(string category, string name, int price)
        {
            Category = category;
            Name = name;
            Price = price;
            HighlightThis = false;
        }
    }
}