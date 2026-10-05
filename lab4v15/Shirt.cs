using System;

namespace lab4v15
{
    public class Shirt : Clothing
    {
        public int SleeveLength { get; set; }

        public Shirt(string material, string size, int sleeveLength)
            : base(material, size)
        {
            SleeveLength = sleeveLength;
        }

        public override void Wear()
        {
            Console.WriteLine($"[Shirt.Wear]: Одягаємо сорочку (Матеріал: {Material}, Розмір: {Size}, Рукав: {SleeveLength} см).");
        }

        public void ButtonUp()
        {
            Console.WriteLine("[Shirt.ButtonUp]: Застібаємо ґудзики на сорочці.");
        }

        public new string GetClothingType()
        {
            return "Похідний тип: Сорочка";
        }
    }
}