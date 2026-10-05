using System;
using System.Drawing;

namespace lab4v15
{
    public class Pants : Clothing
    {
        public int WaistSize { get; set; }

        public Pants(string material, string size, int waistSize)
            : base(material, size)
        {
            WaistSize = waistSize;
        }

        public override void Wear()
        {
            Console.WriteLine($"[Pants.Wear]: Одягаємо штани (Матеріал: {Material}, Розмір: {Size}, Талія: {WaistSize} см).");
        }

        public void ZipUp()
        {
            Console.WriteLine("[Pants.ZipUp]: Застібаємо блискавку на штанах.");
        }
    }
}