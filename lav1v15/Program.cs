using System;

public class House
{
    private string address;
    private string type;

    public string Address
    {
        get { return address; }
        set { address = value; }
    }

    public string Type
    {
        get { return type; }
        set { type = value; }
    }

    public int Floors { get; set; }

    public House(string address, string type, int floors)
    {
        this.address = address;
        this.type = type;
        Floors = floors;
    }

    ~House()
    {
    }

    public string GetInfo()
    {
        return $"Адреса: {address}, Тип: {type}, Кількість поверхів: {Floors}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        House house1 = new House("вул. Соборна, 15", "Багатоквартирний будинок", 9);
        House house2 = new House("вул. Київська, 42", "Приватний котедж", 2);
        House house3 = new House("просп. Миру, 100", "Офісний центр", 14);

        Console.WriteLine(house1.GetInfo());
        Console.WriteLine(house2.GetInfo());
        Console.WriteLine(house3.GetInfo());
    }
}