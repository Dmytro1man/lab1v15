using System;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("--- Демонстрація Book ---");
        Book myBook = new Book("B-001", "1984", 1949, "Джордж Оруелл");
        myBook.DisplayDetails();

        Console.WriteLine("\n--- Демонстрація Journal ---");
        Journal myJournal = new Journal("J-105", "National Geographic", 2023, 12);
        myJournal.DisplayDetails();

        Console.WriteLine("\n--- Демонстрація AudioBook ---");
        AudioBook myAudioBook = new AudioBook("AB-042", "Гаррі Поттер", 1997, "Дж. К. Роулінг", "Стівен Фрай");
        myAudioBook.DisplayDetails();
    }
}