namespace Lab6v15;

public class Pants : Clothing
{
    private int _waistSize;

    public Pants(string material, string size, int waistSize)
        : base(material, size)
    {
        _waistSize = waistSize;
    }

    public int WaistSize
    {
        get => _waistSize;
        set => _waistSize = value;
    }

    public override void Wear()
    {
        base.Wear();
        Console.WriteLine($"[Pants] Штани з обхватом талії {WaistSize} см одягнені.");
    }

    public void ZipUp()
    {
        Console.WriteLine("[Pants] Ви застібаєте блискавку на штанах.");
    }
}