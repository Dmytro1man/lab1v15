using lab12v15;
using Xunit;

namespace lab12v15.Tests;

public class InMemoryRepositoryTests
{
    private static Student CreateStudent(string firstName = "Іван", string lastName = "Петренко", int id = 0)
    {
        return new Student(firstName, lastName, new DateTime(2024, 9, 1)) { Id = id };
    }

    // ===== Add =====

    [Fact]
    public void Add_NewItem_IsStoredAndCanBeRetrieved()
    {
        var repository = new InMemoryRepository<Student>();
        var student = CreateStudent(id: 1);

        repository.Add(student);

        Assert.Single(repository.GetAll());
        Assert.Same(student, repository.GetById(1));
    }

    [Fact]
    public void Add_ItemWithoutId_AssignsUniqueIds()
    {
        var repository = new InMemoryRepository<Student>();
        var first = CreateStudent();
        var second = CreateStudent("Марія", "Коваль");

        repository.Add(first);
        repository.Add(second);

        Assert.Equal(1, first.Id);
        Assert.Equal(2, second.Id);
    }

    [Fact]
    public void Add_DuplicateId_ThrowsArgumentException()
    {
        var repository = new InMemoryRepository<Student>();
        repository.Add(CreateStudent(id: 5));

        Assert.Throws<ArgumentException>(() => repository.Add(CreateStudent("Марія", "Коваль", 5)));
    }

    [Fact]
    public void Add_Null_ThrowsArgumentNullException()
    {
        var repository = new InMemoryRepository<Student>();

        Assert.Throws<ArgumentNullException>(() => repository.Add(null!));
    }

    // ===== GetById =====

    [Fact]
    public void GetById_ExistingId_ReturnsItem()
    {
        var repository = new InMemoryRepository<Student>();
        repository.Add(CreateStudent(id: 1));
        repository.Add(CreateStudent("Марія", "Коваль", 2));

        Student? result = repository.GetById(2);

        Assert.NotNull(result);
        Assert.Equal("Марія", result.FirstName);
    }

    [Fact]
    public void GetById_NonExistingId_ReturnsNull()
    {
        var repository = new InMemoryRepository<Student>();
        repository.Add(CreateStudent(id: 1));

        Assert.Null(repository.GetById(99));
    }

    // ===== GetAll =====

    [Fact]
    public void GetAll_EmptyRepository_ReturnsEmptyCollection()
    {
        var repository = new InMemoryRepository<Student>();

        Assert.Empty(repository.GetAll());
    }

    [Fact]
    public void GetAll_ReturnsAllAddedItems()
    {
        var repository = new InMemoryRepository<Student>();
        repository.Add(CreateStudent(id: 1));
        repository.Add(CreateStudent("Марія", "Коваль", 2));
        repository.Add(CreateStudent("Олег", "Шевчук", 3));

        var all = repository.GetAll().ToList();

        Assert.Equal(3, all.Count);
        Assert.Equal(new[] { 1, 2, 3 }, all.Select(s => s.Id));
    }

    // ===== Update =====

    [Fact]
    public void Update_ExistingItem_ChangesData()
    {
        var repository = new InMemoryRepository<Student>();
        repository.Add(CreateStudent(id: 1));
        var updated = CreateStudent("Іванко", "Петренко-Коваль", 1);

        repository.Update(updated);

        Student? result = repository.GetById(1);
        Assert.NotNull(result);
        Assert.Equal("Іванко", result.FirstName);
        Assert.Equal("Петренко-Коваль", result.LastName);
        Assert.Single(repository.GetAll());
    }

    [Fact]
    public void Update_NonExistingItem_ThrowsKeyNotFoundException()
    {
        var repository = new InMemoryRepository<Student>();
        repository.Add(CreateStudent(id: 1));

        Assert.Throws<KeyNotFoundException>(() => repository.Update(CreateStudent("Марія", "Коваль", 99)));
    }

    // ===== Delete =====

    [Fact]
    public void Delete_ExistingItem_RemovesIt()
    {
        var repository = new InMemoryRepository<Student>();
        repository.Add(CreateStudent(id: 1));
        repository.Add(CreateStudent("Марія", "Коваль", 2));

        repository.Delete(1);

        Assert.Null(repository.GetById(1));
        Assert.Single(repository.GetAll());
    }

    [Fact]
    public void Delete_NonExistingItem_ThrowsKeyNotFoundException()
    {
        var repository = new InMemoryRepository<Student>();
        repository.Add(CreateStudent(id: 1));

        Assert.Throws<KeyNotFoundException>(() => repository.Delete(99));
        Assert.Single(repository.GetAll());
    }
}