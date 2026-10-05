using System;

namespace lab4v15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== 1. СТВОРЕННЯ ОБ'ЄКТІВ ТА ВИКЛИК УНІКАЛЬНИХ МЕТОДІВ ===");
            Shirt myShirt = new Shirt("Бавовна", "L", 65);
            Pants myPants = new Pants("Джинс", "M", 82);

            myShirt.ButtonUp();
            myPants.ZipUp();

            Console.WriteLine("\n=== 2. ДЕМОНСТРАЦІЯ ПОЛІМОРФІЗМУ (override) ===");
            Clothing[] wardrobe = new Clothing[]
            {
                new Clothing("Шовк", "S"),
                myShirt,
                myPants
            };

            foreach (var item in wardrobe)
            {
                item.Wear();
            }

            Console.WriteLine("\n=== 3. ДЕМОНСТРАЦІЯ РІЗНИЦІ МІЖ override ТА new ===");
            Shirt testShirt = new Shirt("Льон", "XL", 68);

            Shirt shirtRef = testShirt;
            Clothing clothingRef = testShirt;

            Console.WriteLine("-- Перевизначений метод Wear() (override):");
            Console.Write("Через Shirt:    "); shirtRef.Wear();
            Console.Write("Через Clothing: "); clothingRef.Wear();

            Console.WriteLine("\n-- Прихований метод GetClothingType() (new):");
            Console.WriteLine($"Через Shirt:    {shirtRef.GetClothingType()}");
            Console.WriteLine($"Через Clothing: {clothingRef.GetClothingType()}");
        }
    }
}