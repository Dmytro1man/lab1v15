using System;

public class BankAccount
{
    
    private string _ownerName;
    private double _balance;

    
    public string OwnerName
    {
        get { return _ownerName; }
    }

    
    public double Balance
    {
        get { return _balance; }
        set { _balance = value; }
    }

   
    public BankAccount(string ownerName, double initialBalance)
    {
        _ownerName = ownerName;
        _balance = initialBalance;
    }

    
    public void Deposit(double amount)
    {
        if (amount > 0)
        {
            _balance += amount;
            Console.WriteLine($"Рахунок поповнено. Поточний баланс: {_balance} грн");
        }
    }
}

public class Book
{
    private string _title;
    private int _pages;

    public string Title
    {
        get { return _title; }
        set { _title = value; }
    }

    public Book(string title, int pages)
    {
        _title = title;
        _pages = pages;
    }

    
    public void PrintBookInfo()
    {
        string size = _pages > 300 ? "велика" : "невелика";
        Console.WriteLine($"Книга '{_title}' має {_pages} сторінок. Це {size} книга.");
    }
}