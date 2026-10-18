namespace Lab6v15;

public class Shirt : Clothing
{
    private double _sleeveLength;

    public Shirt(string material, string size, double sleeveLength)
        : base(material, size)
    {
        _sleeveLength = sleeveLength;
    }

    public double SleeveLength
    {
        get => _sleeveLength;
        set => _sleeveLength = value;
    }

    public override void Wear()
    {
        base.Wear();
        Console.WriteLine($"[Shirt] Сорочка з довжиною рукава {SleeveLength} см одягнена.");
    }

    public void ButtonUp()
    {
        Console.WriteLine("[Shirt] Ви застібаєте сорочку на ґудзики.");
    }

    // Приховування: не override, виклик залежить від типу посилання
    public new string GetClothingType()
    {
        return "Сорочка";
    }
}