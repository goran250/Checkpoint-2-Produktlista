using System;

namespace Checkpoint_2_Produktlista
{
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

        // Shows all the added products in a table. If sorted = true then ProductsList is sorted by price.
        public void ShowProducts(bool sorted)
        {
            Console.WriteLine("\n --------------------------------------------------------------------------");

            // GetLongestTextLength() finds the longest word among product Category, product Name and product Price.      
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

            Console.WriteLine("\n" + " Total Amount:  ".PadLeft(2 * textLength) + totalPrice.ToString().PadLeft(totalPrice.ToString().Length + 2));

            Console.WriteLine(" --------------------------------------------------------------------------");
        }

        // Performs a search for the first product with a name given by the user.
        public void SearchForProductByName()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\n Enter a product name to search for: ");
            Console.ResetColor();

            bool found = false;

            do
            {
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

        // Gets a atring from the console and validates it so its not empty
        private string GetValidatedStringFromConsole(string variableName)
        {
            Console.Write(" Enter a " + variableName + ": ");
            string result = Console.ReadLine();

            while (String.IsNullOrEmpty(result))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" " + variableName + " can't be an empty string");
                Console.ResetColor();
                Console.Write(" Enter a " + variableName + ": ");
                result = Console.ReadLine();
            }

            return result;
        }

        // Returns the sum of the products prices 
        private int GetTotalPrice()
        {
            int totalPrice = 0;
            foreach (Product product in ProductsList)
            {
                totalPrice += product.Price;
            }

            return totalPrice;
        }

        // GetLongestTextLength() finds the longest word among product Category, product Name and product Price.
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
}

