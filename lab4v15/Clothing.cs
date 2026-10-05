using System;

namespace lab4v15
{
    public class Clothing
    {
        public string Material { get; set; }
        public string Size { get; set; }

        public Clothing(string material, string size)
        {
            Material = material;
            Size = size;
        }

        public virtual void Wear()
        {
            Console.WriteLine($"[Clothing.Wear]: Одягаємо базовий одяг (Матеріал: {Material}, Розмір: {Size}).");
        }

        public string GetClothingType()
        {
            return "Базовий тип: Одяг";
        }
    }
}