using System;
using System.Collections.Generic;
using System.Linq;

namespace lab11v15
{
    // --- 1. Сутності ---
    public class Specification
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class Component
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Specification> Specifications { get; set; } = new List<Specification>();
    }

    public class Computer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Component> Components { get; set; } = new List<Component>();
    }

    // --- 2. Custom Extension Method ---
    public static class ComponentExtensions
    {
        // Метод розширення GetSpecification для Component, що повертає значення за ключем
        public static string GetSpecification(this Component component, string key)
        {
            var spec = component.Specifications.FirstOrDefault(s => s.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
            return spec != null ? spec.Value : "Характеристику не знайдено";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // --- 3. Генерація тестових даних ---
            var computers = new List<Computer>
            {
                new Computer
                {
                    Id = 1, Name = "Ігровий ПК",
                    Components = new List<Component>
                    {
                        new Component { Id = 1, Name = "Процесор", Specifications = new List<Specification> 
                            { new Specification { Id = 1, Key = "Модель", Value = "Intel Core i9" }, new Specification { Id = 2, Key = "Частота", Value = "5.0 GHz" } } },
                        new Component { Id = 2, Name = "Відеокарта", Specifications = new List<Specification> 
                            { new Specification { Id = 3, Key = "Модель", Value = "RTX 4090" }, new Specification { Id = 4, Key = "Пам'ять", Value = "24 GB" } } },
                        new Component { Id = 3, Name = "Блок живлення", Specifications = new List<Specification> 
                            { new Specification { Id = 5, Key = "Потужність", Value = "1000W" }, new Specification { Id = 6, Key = "Сертифікат", Value = "80+ Gold" } } }
                    }
                },
                new Computer
                {
                    Id = 2, Name = "Офісний Ноутбук",
                    Components = new List<Component>
                    {
                        new Component { Id = 4, Name = "Процесор", Specifications = new List<Specification> 
                            { new Specification { Id = 7, Key = "Модель", Value = "Intel Core i3" }, new Specification { Id = 8, Key = "Частота", Value = "3.2 GHz" } } },
                        new Component { Id = 5, Name = "Оперативна пам'ять", Specifications = new List<Specification> 
                            { new Specification { Id = 9, Key = "Об'єм", Value = "8 GB" }, new Specification { Id = 10, Key = "Тип", Value = "DDR4" } } },
                        new Component { Id = 6, Name = "Накопичувач", Specifications = new List<Specification> 
                            { new Specification { Id = 11, Key = "Тип", Value = "SSD" }, new Specification { Id = 12, Key = "Об'єм", Value = "256 GB" } } }
                    }
                },
                new Computer
                {
                    Id = 3, Name = "Сервер",
                    Components = new List<Component>
                    {
                        new Component { Id = 7, Name = "Процесор", Specifications = new List<Specification> 
                            { new Specification { Id = 13, Key = "Модель", Value = "AMD EPYC" }, new Specification { Id = 14, Key = "Ядра", Value = "64" } } },
                        new Component { Id = 8, Name = "Оперативна пам'ять", Specifications = new List<Specification> 
                            { new Specification { Id = 15, Key = "Об'єм", Value = "256 GB" }, new Specification { Id = 16, Key = "Тип", Value = "ECC DDR4" } } }
                    }
                }
            };

            // --- Запит 1: SelectMany ---
            // Отримати всі характеристики всіх компонентів всіх комп'ютерів в одному списку
            Console.WriteLine("=== 1. SelectMany: Всі характеристики ===\n");
            var allSpecifications = computers
                .SelectMany(c => c.Components)
                .SelectMany(comp => comp.Specifications)
                .ToList();

            foreach (var spec in allSpecifications)
            {
                Console.WriteLine($"[Id: {spec.Id}] {spec.Key}: {spec.Value}");
            }

            // --- Запит 2: Пагінація ---
            // Вивести всі компоненти по 4 компоненти на сторінку
            Console.WriteLine("\n=== 2. Пагінація: По 4 компоненти на сторінку ===\n");
            var allComponents = computers.SelectMany(c => c.Components).ToList();
            int pageSize = 4;
            int totalPages = (int)Math.Ceiling(allComponents.Count / (double)pageSize);

            for (int page = 1; page <= totalPages; page++)
            {
                Console.WriteLine($"--- Сторінка {page} з {totalPages} ---");
                var pagedComponents = allComponents
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                foreach (var comp in pagedComponents)
                {
                    Console.WriteLine($"- {comp.Name}");
                }
                Console.WriteLine();
            }

            // --- Запит 3: Custom Extension Method ---
            Console.WriteLine("=== 3. Custom Extension: Метод GetSpecification ===\n");
            var sampleComponent = computers[0].Components[0]; // Процесор ігрового ПК
            Console.WriteLine($"Компонент: {sampleComponent.Name}");
            
            // Демонстрація існуючої характеристики
            string modelSpec = sampleComponent.GetSpecification("Модель");
            Console.WriteLine($"Характеристика 'Модель': {modelSpec}");
            
            // Демонстрація неіснуючої характеристики
            string notFoundSpec = sampleComponent.GetSpecification("Температура");
            Console.WriteLine($"Характеристика 'Температура': {notFoundSpec}");
        }
    }
}