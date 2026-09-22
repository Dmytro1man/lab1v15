using System;

public class House
{
    private string _address;
    private string _type;
    private int _floors;

    public string Address
    {
        get => _address;
        set => _address = value;
    }

    public string Type
    {
        get => _type;
        set => _type = value;
    }

    public int Floors
    {
        get => _floors;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Кількість поверхів повинна бути більшою за 0.");
            }
            _floors = value;
        }
    }

    public House() : this("N/A", "Apartment", 1)
    {
    }

    public House(string address, string type, int floors)
    {
        _address = address;
        _type = type;
        Floors = floors;
    }

    ~House()
    {
        Console.WriteLine($"[Деструктор] Об'єкт за адресою '{_address}' знищено збирачем сміття.");
    }

    public string GetHouseInfo()
    {
        return $"Адреса: {_address}, Тип: {_type}, Кількість поверхів: {_floors}";
    }
}

class Program
{
    static void CreateAndUseObjects()
    {
        House house1 = new House();
        House house2 = new House("вул. Соборна, 15", "Багатоквартирний будинок", 9);
        House house3 = new House("вул. Київська, 42", "Приватний котедж", 2);

        Console.WriteLine(house1.GetHouseInfo());
        Console.WriteLine(house2.GetHouseInfo());
        Console.WriteLine(house3.GetHouseInfo());
    }

    static void Main(string[] args)
    {
        Console.WriteLine("--- Створення об'єктів ---");
        CreateAndUseObjects();

        Console.WriteLine("\n--- Завершення Main, підготовка до GC ---");

        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}
