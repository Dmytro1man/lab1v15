using System;

namespace Lab2
{
    // Варіант 15: Клас House
    public class House
    {
        // Приватні поля
        private string _address;
        private string _type;
        private int _floors;

        // Властивості з валідацією
        public string Address
        {
            get => _address;
            set => _address = string.IsNullOrWhiteSpace(value) ? "N/A" : value;
        }

        public string Type
        {
            get => _type;
            set => _type = string.IsNullOrWhiteSpace(value) ? "Невідомо" : value;
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

        // Конструктор за замовчуванням (викликає параметризований через : this())
        public House() : this("N/A", "Apartment", 1)
        {
            Console.WriteLine("[Constructor] Викликано конструктор за замовчуванням.");
        }

        // Додатковий параметризований конструктор (делегує основному)
        public House(string address, string type) : this(address, type, 1)
        {
            Console.WriteLine("[Constructor] Викликано перевантажений конструктор (address, type).");
        }

        // Основний параметризований конструктор
        public House(string address, string type, int floors)
        {
            Address = address;
            Type = type;
            Floors = floors;
            Console.WriteLine($"[Constructor] Створено об'єкт House: '{_address}'.");
        }

        // Метод класу
        public string GetHouseInfo()
        {
            return $"Будинок [Тип: {Type}, Адреса: {Address}, Поверхів: {Floors}]";
        }

        // Деструктор (Фіналізатор)
        ~House()
        {
            Console.WriteLine($"[Finalizer] Об'єкт House за адресою '{_address}' вилучено з пам'яті (GC).");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Створення об'єктів ===");

            // 1. Створення об'єкта через конструктор за замовчуванням
            House house1 = new House();
            Console.WriteLine(house1.GetHouseInfo());
            Console.WriteLine();

            // 2. Створення об'єкта через перевантажений конструктор (2 параметри)
            House house2 = new House("вул. Соборна, 12", "Private House");
            Console.WriteLine(house2.GetHouseInfo());
            Console.WriteLine();

            // 3. Створення об'єкта через повний параметризований конструктор (3 параметри)
            House house3 = new House("пр. Степана Бандери, 45", "Skyscraper", 16);
            Console.WriteLine(house3.GetHouseInfo());
            Console.WriteLine();

            // Демонстрація валідації властивості Floors
            try
            {
                Console.WriteLine("Спроба встановити некоректну кількість поверхів (-2)...");
                house1.Floors = -2;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}\n");
            }

            Console.WriteLine("=== Завершення роботи Main, підготовка до GC ===");

            // Обнулення посилань для звільнення об'єктів для Garbage Collector
            house1 = null;
            house2 = null;
            house3 = null;

            // Примусовий виклик збирача сміття (навчальний приклад)
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.WriteLine("=== Програму завершено ===");
        }
    }
}