namespace lab12v15;

/// <summary>
/// Сутність "Студент" (варіант 15).
/// </summary>
public class Student : IEntity
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateTime EnrollmentDate { get; set; }

    public Student()
    {
    }

    public Student(string firstName, string lastName, DateTime enrollmentDate)
    {
        FirstName = firstName;
        LastName = lastName;
        EnrollmentDate = enrollmentDate;
    }

    public override string ToString()
    {
        return $"Id: {Id}, {FirstName} {LastName}, зарахований: {EnrollmentDate:dd.MM.yyyy}";
    }
}