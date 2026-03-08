using TestTask.Services;
using TestTask.Settings;
using TestTask.UI;

namespace TestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new DataBaseService(Config.ConnectionString);

            var productService = new ProductService(db);
            var incomingService = new IncomingService(db);
            var outgoingService = new OutgoingService(db);

            while (true)
            {
                Console.Clear();

                Console.WriteLine("\n> -----  МЕНЮ ----- <");
                Console.WriteLine("> 1. Добавить товар");
                Console.WriteLine("> 2. Добавить приход");
                Console.WriteLine("> 3. Добавить расход");
                Console.WriteLine("> 4. Показать остатки");
                Console.WriteLine("> 5. Показать товары");
                Console.WriteLine("> 6. Изменить товар");
                Console.WriteLine("> 7. Удалить товар");
                Console.WriteLine("> 8. Изменить приход");
                Console.WriteLine("> 9. Удалить приход");
                Console.WriteLine("> 10. Изменить расход");
                Console.WriteLine("> 11. Удалить расход");
                Console.WriteLine("> 12. Показать приход за период");
                Console.WriteLine("> 13. Показать расход за период");
                Console.WriteLine("> 0. Выход\n");
                Console.Write("> Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        UserInterface.AddProduct(productService);
                        break;

                    case "2":
                        UserInterface.AddIncoming(productService, incomingService);
                        break;

                    case "3":
                        UserInterface.AddOutgoing(productService, outgoingService);
                        break;

                    case "4":
                        UserInterface.ShowStock(productService, incomingService, outgoingService);
                        break;

                    case "5":
                        UserInterface.ShowProducts(productService);
                        break;
                    case "6":
                        UserInterface.EditProduct(productService);
                        break;

                    case "7":
                        UserInterface.DeleteProduct(productService);
                        break;

                    case "8":
                        UserInterface.EditIncoming(productService, incomingService);
                        break;

                    case "9":
                        UserInterface.DeleteIncoming(incomingService);
                        break;

                    case "10":
                        UserInterface.EditOutgoing(productService, outgoingService);
                        break;

                    case "11":
                        UserInterface.DeleteOutgoing(outgoingService);
                        break;

                    case "12":
                        UserInterface.ShowIncomingByPeriod(productService, incomingService);
                        break;

                    case "13":
                        UserInterface.ShowOutgoingByPeriod(productService, outgoingService);
                        break;
                    case "0":
                        return;

                    default:
                        Console.WriteLine("Нет такого варианта на дисплее!");
                        break;
                }
                Console.WriteLine("Нажмите на любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }
    }
}