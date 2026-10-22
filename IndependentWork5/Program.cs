using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== 1. Поліморфізм через System.Object ===");
        List<object> myObjects = new List<object>
        {
            new Product { Name = "Ноутбук", Price = 25000 },
            new Customer { FullName = "Іван Іванов" },
            "Звичайний рядок (теж Object!)"
        };

       
        foreach (object obj in myObjects)
        {
            Console.WriteLine(obj.ToString());
        }

        Console.WriteLine("\n=== 2. Поліморфізм через IEnumerable ===");
        MyCustomCollection collection = new MyCustomCollection();
        collection.Add("Рядок 1");
        collection.Add("Рядок 2");

       
        foreach (string item in collection)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("\n=== 3. Поліморфізм через абстрактний клас Stream ===");
        
        
        string testFilePath = "test_stream.txt";
        File.WriteAllText(testFilePath, "Hello from FileStream!");
        
        using (FileStream fs = new FileStream(testFilePath, FileMode.Open))
        {
            Console.Write("Читаємо з файлу: ");
            ProcessStream(fs);
        }

        
        byte[] memoryData = Encoding.UTF8.GetBytes("Hello from MemoryStream!");
        using (MemoryStream ms = new MemoryStream(memoryData))
        {
            Console.Write("Читаємо з пам'яті: ");
            ProcessStream(ms);
        }
    }

    
    public static void ProcessStream(Stream stream)
    {
        byte[] buffer = new byte[5]; 
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string result = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Console.WriteLine(result);
    }
}