using System.Net.Http.Headers;

namespace Checkpoint_2_Produktlista
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>();

            products.Add(AddProduct());

            do
            {                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nThe product was successfully added");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nTo enter another product just press \"Enter\".");
                Console.WriteLine("To list the products: Enter \"List\".");
                Console.WriteLine("To quit enter: \"Q\".");
                Console.ResetColor();

                string answer = Console.ReadLine();


                if (answer.ToLower().Trim() == "list")
                {
                    ShowProducts(products);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nTo enter another product just press \"Enter\"");
                    Console.WriteLine("To quit enter: \"Q\".");
                    Console.ResetColor();
                    string answer2 = Console.ReadLine();

                    if (answer2.ToLower().Trim() == "q")
                    {
                        break;
                    }
                    else if (string.IsNullOrEmpty(answer2))
                    {
                        products.Add(AddProduct());
                    }
                }
                else if (answer.ToLower().Trim() == "q")
                {
                    break;
                }
                else if (string.IsNullOrEmpty(answer))
                {
                    products.Add(AddProduct());
                }
            } while (true);
        }

        static Product AddProduct()
        {
            Product product = new Product();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nTo enter a new product - Follow the steps\n");
            Console.ResetColor();

            Console.Write("Enter a category: ");
            product.Category = Console.ReadLine();

            Console.Write("Enter a product name: ");
            product.Name = Console.ReadLine();

            Console.Write("Enter a price of the product: ");
            product.Price = int.Parse(Console.ReadLine());


            return product;
        }

        static void ShowProducts(List<Product> products)
        {
            Console.WriteLine("\n------------------------------------------------------------");
            

            int textLength = "Category".Length;
            int i = 0;

            do  // Find the longest word among product Category, product Name and product Price
            {
                if (products[i].Category.Length > textLength)
                {
                    textLength = products[i].Category.Length;
                }

                if (products[i].Name.Length > textLength)
                {
                    textLength = products[i].Name.Length;
                }

                if (products[i].Price.ToString().Length > textLength)
                {
                    textLength = products[i].Price.ToString().Length;
                }

                i++;
            } while (i < products.Count);

            int padding = textLength + 3;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Category".PadRight(padding) + "Product".PadRight(padding) + "Price".PadRight(padding));
            Console.ResetColor();
            // Write the products too a table
            foreach (Product product in products) {
                Console.WriteLine(product.Category.PadRight(padding) + product.Name.PadRight(padding) + product.Price.ToString().PadRight(padding));
            }

            Console.WriteLine("------------------------------------------------------------");
        }
    }

    class Product { 
        public string Category { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public Product() {
        }

        public Product(string category, string name, int price)
        {
            Category = category;
            Name = name;
            Price = price;
        }

    }
}
