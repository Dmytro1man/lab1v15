using System;


public class LibraryItem
{
    private string _uniqueId;
    private string _title;
    private int _publicationYear;

    public string UniqueId { get { return _uniqueId; } }
    public string Title { get { return _title; } set { _title = value; } }
    public int PublicationYear { get { return _publicationYear; } set { _publicationYear = value; } }

    
    public LibraryItem(string uniqueId, string title, int publicationYear)
    {
        _uniqueId = uniqueId;
        _title = title;
        _publicationYear = publicationYear;
    }

    public virtual void DisplayDetails()
    {
        Console.WriteLine($"[ID: {UniqueId}] Назва: {Title}, Рік видання: {PublicationYear}");
    }
}


public class Book : LibraryItem
{
    private string _author;
    public string Author { get { return _author; } set { _author = value; } }

    
    public Book(string uniqueId, string title, int publicationYear, string author) 
        : base(uniqueId, title, publicationYear)
    {
        _author = author;
    }

    public override void DisplayDetails()
    {
        base.DisplayDetails();
        Console.WriteLine($"   Автор: {Author}");
    }
}


public class Journal : LibraryItem
{
    private int _issueNumber;
    public int IssueNumber { get { return _issueNumber; } set { _issueNumber = value; } }

    public Journal(string uniqueId, string title, int publicationYear, int issueNumber) 
        : base(uniqueId, title, publicationYear)
    {
        _issueNumber = issueNumber;
    }

    public override void DisplayDetails()
    {
        base.DisplayDetails();
        Console.WriteLine($"   Номер випуску: {IssueNumber}");
    }
}


public class AudioBook : Book
{
    private string _narrator;
    public string Narrator { get { return _narrator; } set { _narrator = value; } }

    public AudioBook(string uniqueId, string title, int publicationYear, string author, string narrator) 
        : base(uniqueId, title, publicationYear, author)
    {
        _narrator = narrator;
    }

    public override void DisplayDetails()
    {
        base.DisplayDetails();
        Console.WriteLine($"   Оповідач (голос): {Narrator}");
    }
}