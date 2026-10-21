using System;

public class Program
{
    public static void Main(string[] args)
    {
        
        BankAccount account = new BankAccount("Іван Франко", 1500.50);
        Console.WriteLine($"Власник рахунку: {account.OwnerName}");
        account.Deposit(500.25);

        Console.WriteLine("-------------------");

        
        Book myBook = new Book("Тіні забутих предків", 250);
        myBook.PrintBookInfo();
    }
}