using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace lab10v15
{
    // --- Сутності ---
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int SupplierId { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            // Заповнення колекцій (мінімум 10-20 елементів)
            var suppliers = new List<Supplier>
            {
                new Supplier { Id = 1, Name = "Global Supplies", Country = "USA" },
                new Supplier { Id = 2, Name = "TechParts Co", Country = "China" },
                new Supplier { Id = 3, Name = "EuroGoods", Country = "Germany" },
                new Supplier { Id = 4, Name = "LocalMakers", Country = "Ukraine" }
            };

            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Keyboard", Price = 50m, SupplierId = 1 },
                new Product { Id = 2, Name = "Mouse", Price = 25m, SupplierId = 1 },
                new Product { Id = 3, Name = "Monitor", Price = 300m, SupplierId = 2 },
                new Product { Id = 4, Name = "USB Cable", Price = 5m, SupplierId = 1 },
                new Product { Id = 5, Name = "Laptop", Price = 1200m, SupplierId = 2 },
                new Product { Id = 6, Name = "Headphones", Price = 80m, SupplierId = 3 },
                new Product { Id = 7, Name = "Webcam", Price = 110m, SupplierId = 1 },
                new Product { Id = 8, Name = "Desk Mat", Price = 15m, SupplierId = 4 },
                new Product { Id = 9, Name = "Flash Drive", Price = 12m, SupplierId = 1 },
                new Product { Id = 10, Name = "Chair", Price = 250m, SupplierId = 3 },
                new Product { Id = 11, Name = "Microphone", Price = 90m, SupplierId = 3 },
                new Product { Id = 12, Name = "Power Bank", Price = 40m, SupplierId = 2 },
                new Product { Id = 13, Name = "Adapter", Price = 8m, SupplierId = 1 },
                new Product { Id = 14, Name = "Cooling Pad", Price = 30m, SupplierId = 4 },
                new Product { Id = 15, Name = "SSD 1TB", Price = 95m, SupplierId = 2 }
            };

            Console.WriteLine("=== Лабораторна робота №10. LINQ в дії (Варіант 15) ===\n");

            // Запит 1: Знайти всі продукти від "Global Supplies" з ціною < 100
            MeasureExecution("Запит 1 (Синтаксис методів): Продукти Global Supplies < 100$", () =>
            {
                var targetSupplierId = suppliers.First(s => s.Name == "Global Supplies").Id;
                var res1 = products.Where(p => p.SupplierId == targetSupplierId && p.Price < 100m).ToList();
            });

            MeasureExecution("Запит 1 (Синтаксис запитів): Продукти Global Supplies < 100$", () =>
            {
                var targetSupplierId = (from s in suppliers where s.Name == "Global Supplies" select s.Id).First();
                var res1 = (from p in products
                            where p.SupplierId == targetSupplierId && p.Price < 100m
                            select p).ToList();
            });

            // Запит 2: Об'єднати продукти з їхніми постачальниками (назва продукту + назва постачальника)
            MeasureExecution("Запит 2 (Синтаксис методів): Join продуктів та постачальників", () =>
            {
                var res2 = products.Join(suppliers,
                    p => p.SupplierId,
                    s => s.Id,
                    (p, s) => new { p.Name, SupplierName = s.Name }).ToList();
            });

            MeasureExecution("Запит 2 (Синтаксис запитів): Join продуктів та постачальників", () =>
            {
                var res2 = (from p in products
                            join s in suppliers on p.SupplierId equals s.Id
                            select new { p.Name, SupplierName = s.Name }).ToList();
            });

            // Запит 3: Підрахувати кількість продуктів для кожного постачальника
            MeasureExecution("Запит 3 (Синтаксис методів): Кількість продуктів за постачальником (GroupJoin)", () =>
            {
                var res3 = suppliers.GroupJoin(products,
                    s => s.Id,
                    p => p.SupplierId,
                    (s, prodGroup) => new { s.Name, Count = prodGroup.Count() }).ToList();
            });

            MeasureExecution("Запит 3 (Синтаксис запитів): Кількість продуктів за постачальником (GroupJoin)", () =>
            {
                var res3 = (from s in suppliers
                            join p in products on s.Id equals p.SupplierId into prodGroup
                            select new { s.Name, Count = prodGroup.Count() }).ToList();
            });

            // Запит 4: Знайти найдешевший продукт
            MeasureExecution("Запит 4 (Синтаксис методів): Найдешевший продукт", () =>
            {
                var minPrice = products.Min(p => p.Price);
                var cheapestProduct = products.First(p => p.Price == minPrice);
            });

            MeasureExecution("Запит 4 (Синтаксис запитів): Найдешевший продукт", () =>
            {
                var minPrice = (from p in products select p.Price).Min();
                var cheapestProduct = (from p in products where p.Price == minPrice select p).First();
            });
            
            PrintConclusions();
        }

        // Допоміжний метод для вимірювання часу
        static void MeasureExecution(string queryName, Action action)
        {
            var sw = Stopwatch.StartNew();
            action();
            sw.Stop();
            Console.WriteLine($"{queryName,-80} | Час: {sw.ElapsedTicks} тіків ({sw.ElapsedMilliseconds} мс)");
        }

        static void PrintConclusions()
        {
            Console.WriteLine("\n=== Висновки щодо продуктивності ===");
            Console.WriteLine("1. Перший запит (будь-який) виконується трохи довше через накладні витрати JIT-компілятора на завантаження бібліотек LINQ.");
            Console.WriteLine("2. Синтаксис методів та синтаксис запитів компілюються в один і той самий IL-код, тому різниця в їх часі виконання мінімальна (в межах похибки).");
            Console.WriteLine("3. Найважчою операцією є Join/GroupJoin (Запити 2 і 3), оскільки вони вимагають зіставлення двох колекцій, на відміну від звичайної фільтрації (Where) чи агрегації (Min).");
            Console.WriteLine("4. Відкладене виконання (Deferred Execution) означає, що запит не займає часу на обробку даних до виклику ToList() або Min().");
        }
    }
}