namespace lab12v15;

/// <summary>
/// Узагальнений репозиторій для сутностей з ідентифікатором.
/// </summary>
/// <typeparam name="T">Тип сутності (має реалізовувати IEntity).</typeparam>
public interface IRepository<T> where T : IEntity
{
    /// <summary>Повертає елемент за Id або null, якщо його немає.</summary>
    T? GetById(int id);

    /// <summary>Повертає всі елементи.</summary>
    IEnumerable<T> GetAll();

    /// <summary>Додає новий елемент.</summary>
    void Add(T item);

    /// <summary>Оновлює існуючий елемент (пошук за Id).</summary>
    void Update(T item);

    /// <summary>Видаляє елемент за Id.</summary>
    void Delete(int id);
}