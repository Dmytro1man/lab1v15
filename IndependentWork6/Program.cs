using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== СЦЕНАРІЙ 1: Демонстрація через Інтерфейс INotifier ===\n");
        
        List<INotifier> notifiers = new List<INotifier>
        {
            new EmailNotifier(),
            new SmsNotifier(),
            new PushNotifier()
        };

        foreach (INotifier notifier in notifiers)
        {
            notifier.Send("Системне оновлення успішно завершено!");
        }

        Console.WriteLine("\n========================================================");
        Console.WriteLine("=== СЦЕНАРІЙ 2: Демонстрація через Абстрактний клас FileProcessor ===\n");

        List<FileProcessor> processors = new List<FileProcessor>
        {
            new TextFileProcessor("document.txt"),
            new XmlFileProcessor("config.xml")
        };

        foreach (FileProcessor processor in processors)
        {
            processor.Process();
            Console.WriteLine(); 
        }
    }
}