namespace ApprovalTesting.Demo1;

public record Person(
    string FirstName,
    string LastName,
    int Age,
    Guid Id,
    DateTime CreatedAt,
    DateTime? UpdatedAt)
{
    public Person() : this(
        string.Empty, 
        string.Empty, 
        0, Guid.Empty, 
        DateTime.Now, 
        DateTime.Now)
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Age = 0;
        Id = Guid.Empty;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
}
