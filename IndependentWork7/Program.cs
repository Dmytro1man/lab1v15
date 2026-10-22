using System;

namespace IndependentWork7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Демонстрація роботи Cache<string, string> (FIFO) ===\n");

            Cache<string, string> cache = new Cache<string, string>(3); // Ємність = 3

            cache.Add("key1", "Значення 1");
            cache.Add("key2", "Значення 2");
            cache.Add("key3", "Значення 3");

            Console.WriteLine($"Кількість елементів у кеші: {cache.Count}"); // 3
            Console.WriteLine($"key1: {cache.Get("key1")}");

            Console.WriteLine("\n--> Додаємо 4-й елемент ('key4')...");
            cache.Add("key4", "Значення 4"); // 'key1' має видалитися за FIFO[cite: 23]

            Console.WriteLine($"Чи містить кеш 'key1'? {cache.ContainsKey("key1")}"); // False
            Console.WriteLine($"Чи містить кеш 'key4'? {cache.ContainsKey("key4")}"); // True
            Console.WriteLine($"Поточна кількість елементів: {cache.Count}"); // 3
        }
    }
}