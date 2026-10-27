namespace lab12v15;

/// <summary>
/// Реалізація репозиторія, що зберігає дані в пам'яті (List).
/// </summary>
public class InMemoryRepository<T> : IRepository<T> where T : IEntity
{
    private readonly List<T> _items = new List<T>();

    public T? GetById(int id)
    {
        return _items.FirstOrDefault(item => item.Id == id);
    }

    public IEnumerable<T> GetAll()
    {
        // Повертаємо копію, щоб зовнішній код не міг змінити внутрішній список
        return _items.ToList();
    }

    public void Add(T item)
    {
        ArgumentNullException.ThrowIfNull(item);

        // Якщо Id не задано, призначаємо наступний вільний
        if (item.Id == 0)
        {
            item.Id = _items.Count == 0 ? 1 : _items.Max(x => x.Id) + 1;
        }
        else if (_items.Any(x => x.Id == item.Id))
        {
            throw new ArgumentException($"Елемент з Id {item.Id} вже існує.", nameof(item));
        }

        _items.Add(item);
    }

    public void Update(T item)
    {
        ArgumentNullException.ThrowIfNull(item);

        int index = _items.FindIndex(x => x.Id == item.Id);
        if (index < 0)
        {
            throw new KeyNotFoundException($"Елемент з Id {item.Id} не знайдено.");
        }

        _items[index] = item;
    }

    public void Delete(int id)
    {
        int removed = _items.RemoveAll(x => x.Id == id);
        if (removed == 0)
        {
            throw new KeyNotFoundException($"Елемент з Id {id} не знайдено.");
        }
    }
}