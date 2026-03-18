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
            Console.WriteLine("\n To enter another product just press \"Enter\".");
            Console.WriteLine(" To list the products enter: \"L\".");
            Console.WriteLine(" To search for a product by name enter: \"S\".");
            Console.WriteLine(" To quit enter: \"Q\".");
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
}
