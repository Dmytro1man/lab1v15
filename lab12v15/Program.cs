using lab12v15;

Console.OutputEncoding = System.Text.Encoding.UTF8;

IRepository<Student> repository = new InMemoryRepository<Student>();

// Додавання
repository.Add(new Student("Іван", "Петренко", new DateTime(2024, 9, 1)));
repository.Add(new Student("Марія", "Коваль", new DateTime(2023, 9, 1)));
repository.Add(new Student("Олег", "Шевчук", new DateTime(2025, 9, 1)));

Console.WriteLine("=== Усі студенти після додавання ===");
PrintAll(repository);

// Отримання за Id
Console.WriteLine();
Console.WriteLine("=== GetById(2) ===");
Console.WriteLine(repository.GetById(2));

Console.WriteLine("=== GetById(99) (неіснуючий) ===");
Console.WriteLine(repository.GetById(99) is null ? "Студента не знайдено (null)" : "Знайдено");

// Оновлення
Console.WriteLine();
Console.WriteLine("=== Оновлення студента з Id 2 ===");
repository.Update(new Student("Марія", "Коваленко", new DateTime(2023, 9, 1)) { Id = 2 });
Console.WriteLine(repository.GetById(2));

// Видалення
Console.WriteLine();
Console.WriteLine("=== Видалення студента з Id 1 ===");
repository.Delete(1);
PrintAll(repository);

// Неіснуючий елемент
Console.WriteLine();
Console.WriteLine("=== Спроба видалити неіснуючого студента ===");
try
{
    repository.Delete(99);
}
catch (KeyNotFoundException ex)
{
    Console.WriteLine($"Помилка: {ex.Message}");
}

static void PrintAll(IRepository<Student> repo)
{
    foreach (Student student in repo.GetAll())
    {
        Console.WriteLine(student);
    }
}