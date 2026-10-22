using System;
using System.Collections;
using System.Collections.Generic;



public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    
    public override string ToString()
    {
        return $"[Product] Товар: {Name}, Ціна: {Price} грн";
    }
}

public class Customer
{
    public string FullName { get; set; }

    
    public override string ToString()
    {
        return $"[Customer] Клієнт: {FullName}";
    }
}



public class MyCustomCollection : IEnumerable<string>
{
    private List<string> _items = new List<string>();

    public void Add(string item)
    {
        _items.Add(item);
    }

    
    public IEnumerator<string> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

   
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}