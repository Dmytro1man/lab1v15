namespace Lab6v15;

public class Clothing
{
    private string _material;
    private string _size;

    public Clothing(string material, string size)
    {
        _material = material;
        _size = size;
    }

    public string Material
    {
        get => _material;
        set => _material = value;
    }

    public string Size
    {
        get => _size;
        set => _size = value;
    }

    // Віртуальний метод: похідні класи можуть його перевизначити
    public virtual void Wear()
    {
        Console.WriteLine($"[Clothing] Ви одягаєте річ: матеріал — {Material}, розмір — {Size}.");
    }

    // Звичайний метод: буде приховано в Shirt через new
    public string GetClothingType()
    {
        return "Загальний одяг";
    }
}