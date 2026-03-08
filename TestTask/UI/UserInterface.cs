using System;
using System.Collections.Generic;
using System.Text;
using TestTask.Services;

namespace TestTask.UI
{
    internal class UserInterface
    {
        public static void AddProduct(ProductService productService)
        {
            Console.Write("> Введите название товара: ");
            string name = Console.ReadLine();

            var product = productService.Add(name);
            Console.WriteLine($"> Товар добавлен: {product.Id} — {product.Name}");
        }
        public static void AddIncoming(ProductService productService, IncomingService incomingService)
        {
            ShowProducts(productService);

            Console.Write("> Введите ID товара: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("> Введите количество: ");
            int amount = int.Parse(Console.ReadLine());

            incomingService.Add(productId, amount);
            Console.WriteLine("Приход добавлен.");
        }
        public static void AddOutgoing(ProductService productService, OutgoingService outgoingService)
        {
            ShowProducts(productService);

            Console.Write("> Введите ID товара: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("> Введите количество: ");
            int amount = int.Parse(Console.ReadLine());

            outgoingService.Add(productId, amount);
            Console.WriteLine("Расход добавлен.");
        }
        public static void ShowStock(ProductService productService, IncomingService incomingService, OutgoingService outgoingService)
        {
            var products = productService.GetAll();
            var incoming = incomingService.GetAll();
            var outgoing = outgoingService.GetAll();

            Console.WriteLine("\n> ----- Остатки ----- <");

            foreach (var p in products)
            {
                int inc = incoming.Where(i => i.ProductId == p.Id).Sum(i => i.Amount);
                int outg = outgoing.Where(o => o.ProductId == p.Id).Sum(o => o.Amount);

                Console.WriteLine($"{p.Name}: {inc - outg}");
            }
        }
        public static void ShowProducts(ProductService productService)
        {
            var products = productService.GetAll();

            Console.WriteLine("\n> ----- Товары ----- <");
            foreach (var p in products)
                Console.WriteLine($"{p.Id}. {p.Name}");
        }
        public static void EditProduct(ProductService productService)
        {
            ShowProducts(productService);

            Console.Write("> Введите ID товара: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("> Новое название: ");
            string newName = Console.ReadLine();

            productService.Update(id, newName);
            Console.WriteLine("Товар обновлён.");
        }
        public static void DeleteProduct(ProductService productService)
        {
            ShowProducts(productService);

            Console.Write("> Введите ID товара: ");
            int id = int.Parse(Console.ReadLine());

            productService.Delete(id);
            Console.WriteLine("Товар удалён.");
        }
        public static void EditIncoming(ProductService productService, IncomingService incomingService)
        {
            var list = incomingService.GetAll();
            foreach (var i in list)
                Console.WriteLine($"{i.Id}. Товар {i.ProductId}, Кол-во {i.Amount}, Дата {i.Date}");

            Console.Write("> ID записи: ");
            int id = int.Parse(Console.ReadLine());

            ShowProducts(productService);
            Console.Write("> Новый ID товара: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("> Новое количество: ");
            int amount = int.Parse(Console.ReadLine());

            Console.Write("> Новая дата (yyyy-mm-dd): ");
            DateTime date = DateTime.Parse(Console.ReadLine());

            incomingService.Update(id, productId, amount, date);
            Console.WriteLine("Приход обновлён.");
        }
        public static void DeleteIncoming(IncomingService incomingService)
        {
            var list = incomingService.GetAll();
            foreach (var i in list)
                Console.WriteLine($"{i.Id}. Товар {i.ProductId}, Кол-во {i.Amount}, Дата {i.Date}");

            Console.Write("> ID записи: ");
            int id = int.Parse(Console.ReadLine());

            incomingService.Delete(id);
            Console.WriteLine("Приход удалён.");
        }
        public static void EditOutgoing(ProductService productService, OutgoingService outgoingService)
        {
            var list = outgoingService.GetAll();
            foreach (var o in list)
                Console.WriteLine($"{o.Id}. Товар {o.ProductId}, Кол-во {o.Amount}, Дата {o.Date}");

            Console.Write("> ID записи: ");
            int id = int.Parse(Console.ReadLine());

            ShowProducts(productService);
            Console.Write("> Новый ID товара: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("> Новое количество: ");
            int amount = int.Parse(Console.ReadLine());

            Console.Write("> Новая дата (yyyy-mm-dd): ");
            DateTime date = DateTime.Parse(Console.ReadLine());

            outgoingService.Update(id, productId, amount, date);
            Console.WriteLine("Расход обновлён.");
        }
        public static void DeleteOutgoing(OutgoingService outgoingService)
        {
            var list = outgoingService.GetAll();
            foreach (var o in list)
                Console.WriteLine($"{o.Id}. Товар {o.ProductId}, Кол-во {o.Amount}, Дата {o.Date}");

            Console.Write("> ID записи: ");
            int id = int.Parse(Console.ReadLine());

            outgoingService.Delete(id);
            Console.WriteLine("Расход удалён.");
        }
        public static void ShowIncomingByPeriod(ProductService productService, IncomingService incomingService)
        {
            Console.Write("> Дата начала (yyyy-mm-dd): ");
            DateTime from = DateTime.Parse(Console.ReadLine());

            Console.Write("> Дата конца (yyyy-mm-dd): ");
            DateTime to = DateTime.Parse(Console.ReadLine());

            var list = incomingService.GetByPeriod(from, to);

            Console.WriteLine("\n> ----- Приход за период ----- <");
            foreach (var i in list)
                Console.WriteLine($"{i.Date}: Товар {i.ProductId}, Кол-во {i.Amount}");
        }
        public static void ShowOutgoingByPeriod(ProductService productService, OutgoingService outgoingService)
        {
            Console.Write("> Дата начала (yyyy-mm-dd): ");
            DateTime from = DateTime.Parse(Console.ReadLine());

            Console.Write("> Дата конца (yyyy-mm-dd): ");
            DateTime to = DateTime.Parse(Console.ReadLine());

            var list = outgoingService.GetByPeriod(from, to);

            Console.WriteLine("\n> ----- Расход за период ----- <");
            foreach (var o in list)
                Console.WriteLine($"{o.Date}: Товар {o.ProductId}, Кол-во {o.Amount}");
        }
    }
}