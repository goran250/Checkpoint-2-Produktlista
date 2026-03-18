using System.Net.Http.Headers;
using System.Numerics;

namespace Checkpoint_2_Produktlista
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Products products = new Products();

            products.AddProductToList();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nTo enter another product just press \"Enter\".");
            Console.WriteLine("To list the products enter: \"L\".");
            Console.WriteLine("To search for a product by name enter: \"S\".");
            Console.WriteLine("To quit enter: \"Q\".");
            Console.ResetColor();

            do
            {
                string answer = Console.ReadLine();

                if (answer.ToLower().Trim() == "l")
                {
                    products.ShowProducts(true); // The first True means the products should be sorted by Price 

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n To enter another product just press \"Enter\"");
                    Console.WriteLine(" To list the products enter: \"L\".");
                    Console.WriteLine(" To search for a product by name enter: \"S\".");
                    Console.WriteLine(" To quit enter: \"Q\".");
                    Console.ResetColor();                
                }
                else if (answer.ToLower().Trim() == "s")
                {
                    products.SearchForProductByName();
                    products.ShowProducts(true); 
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n To enter another product just press \"Enter\"");
                    Console.WriteLine(" To list the products enter: \"L\".");
                    Console.WriteLine(" To search for a another product enter: \"S\".");
                    Console.WriteLine(" To quit enter: \"Q\".");
                    Console.ResetColor();
                }
                else if (answer.ToLower().Trim() == "q")
                {
                    break;
                }
                else if (string.IsNullOrEmpty(answer))
                {
                    products.AddProductToList();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n To enter another product just press \"Enter\".");
                    Console.WriteLine(" To list the products enter: \"L\".");
                    Console.WriteLine(" To search for a product by name enter: \"S\".");
                    Console.WriteLine(" To quit enter: \"Q\".");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n Enter valid input: ENTER, L, S or Q ");
                    Console.ResetColor();
                }
            } while (true);
        }
    }
    

    class Products
    {
        public List<Product> ProductsList { get; set; }

        public Products()
        {
            ProductsList = new List<Product>();
        }

        public void AddProductToList()
        {
            Product product = new Product();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n To enter a new product - Follow the steps\n");
            Console.ResetColor();

            product.Category = GetValidatedStringFromConsole("Category");

            product.Name = GetValidatedStringFromConsole("Product name");

            bool isValidInteger;
            
            do
            {
                Console.Write(" Enter a price of the product: ");
                isValidInteger = int.TryParse(Console.ReadLine(), out int tempPrice);
                product.Price = tempPrice;
                
                Console.ForegroundColor = ConsoleColor.Red;
                if (isValidInteger == false) 
                {
                    Console.WriteLine(" Price can only contain digits and can't be empty.");
                }
                else if (product.Price <= 0)
                {
                    Console.WriteLine(" Price must be non-negative and higher than zero.");
                    isValidInteger = false;
                }
                Console.ResetColor();
            } while (isValidInteger == false);

            ProductsList.Add(product);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n The product was successfully added");
        }

        public void ShowProducts(bool sorted)
        {
            Console.WriteLine("\n --------------------------------------------------------------------------");

            // GetLongestTextLength() findd the longest word among product Category, product Name and product Price. 
            // This i to get the padding right
            int textLength = GetLongestTextLength() + 3;

            int totalPrice = GetTotalPrice();

            if (sorted)
            {
                ProductsList = ProductsList.OrderBy(p => p.Price).ToList();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Category".PadRight(textLength) + "Product".PadRight(textLength) + "Price".PadLeft(totalPrice.ToString().Length + 2));
            Console.ResetColor();
            // Write the products too a table
            foreach (Product product in ProductsList)
            {
                if (product.HighlightThis)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }

                Console.WriteLine(" " + product.Category.PadRight(textLength) + product.Name.PadRight(textLength) + product.Price.ToString().PadLeft(totalPrice.ToString().Length + 2));
                
                if (product.HighlightThis)
                {
                    Console.ResetColor();
                    product.HighlightThis = false;
                }
                    
            }

            Console.WriteLine("\n" + " Total Amount:  ".PadLeft(2*textLength) + totalPrice.ToString().PadLeft(totalPrice.ToString().Length + 2));

            Console.WriteLine("--------------------------------------------------------------------------");
        }

        public void SearchForProductByName()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\n Enter a product name to search for: ");
            Console.ResetColor();

            bool found = false;

            do { 
                string nameToSearchFor = Console.ReadLine();

                int index = ProductsList.FindIndex(p => p.Name.Contains(nameToSearchFor));
                if (index >= 0)
                {
                    ProductsList[index].HighlightThis = true;
                    found = true;
                }
                else
                {
                    Console.WriteLine("\n No product with that name exist: Please enter a new product name: ");
                }
            } while (found == false);
        }

        private string GetValidatedStringFromConsole( string variableName)
        {
            Console.Write(" Enter a " + variableName + ": ");
            string result = Console.ReadLine();

            while (String.IsNullOrEmpty(result))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" " + variableName + " can't be an empty string");
                Console.ResetColor();
                Console.Write(" Enter a " + variableName + ": ");
                result  = Console.ReadLine();
            }

            return result;
        }
        
        private int GetTotalPrice()
        {
            int totalPrice = 0;
            foreach (Product product in ProductsList) {
                totalPrice += product.Price;
            }

            return totalPrice;
        }

        private int GetLongestTextLength()
        {                    
            int textLength = " Total amount:".Length; // "Total amount:" is longest among the header items
            int i = 0;

            do  // Find the longest word among product Category, product Name and product Price.
            {
                if (ProductsList[i].Category.Length > textLength)
                {
                    textLength = ProductsList[i].Category.Length;
                }

                if (ProductsList[i].Name.Length > textLength)
                {
                    textLength = ProductsList[i].Name.Length;
                }

                if (ProductsList[i].Price.ToString().Length > textLength)
                {
                    textLength = ProductsList[i].Price.ToString().Length;
                }

                i++;
            } while (i < ProductsList.Count);

            return textLength;
        }
    }


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
