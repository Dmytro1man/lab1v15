using System;

// ==========================================
// СЦЕНАРІЙ 1: Система сповіщень (Інтерфейс)[cite: 20, 21]
// ==========================================

public interface INotifier
{
    void Send(string message);
}

public class EmailNotifier : INotifier
{
    public void Send(string message)
    {
        Console.WriteLine($"[Email]: Відправлено листа з текстом: {message}");
    }
}

public class SmsNotifier : INotifier
{
    public void Send(string message)
    {
        Console.WriteLine($"[SMS]: Відправлено SMS з текстом: {message}");
    }
}

public class PushNotifier : INotifier
{
    public void Send(string message)
    {
        Console.WriteLine($"[Push]: Відправлено Push-сповіщення: {message}");
    }
}

// ==========================================
// СЦЕНАРІЙ 2: Обробка файлів (Абстрактний клас)[cite: 20, 21]
// ==========================================

public abstract class FileProcessor
{
    public string FileName { get; }

    protected FileProcessor(string fileName)
    {
        FileName = fileName;
    }

    
    public void OpenFile()
    {
        Console.WriteLine($"Файл '{FileName}' успішно відкрито.");
    }

    public void CloseFile()
    {
        Console.WriteLine($"Файл '{FileName}' закрито.");
    }

    
    public abstract void ProcessContent();

    
    public void Process()
    {
        OpenFile();
        ProcessContent();
        CloseFile();
    }
}

public class TextFileProcessor : FileProcessor
{
    public TextFileProcessor(string fileName) : base(fileName) { }

    public override void ProcessContent()
    {
        Console.WriteLine($"   --> Читаємо та зчитуємо текстові рядки з {FileName}...");
    }
}

public class XmlFileProcessor : FileProcessor
{
    public XmlFileProcessor(string fileName) : base(fileName) { }

    public override void ProcessContent()
    {
        Console.WriteLine($"   --> Парсимо XML-дерево та теги з {FileName}...");
    }
}