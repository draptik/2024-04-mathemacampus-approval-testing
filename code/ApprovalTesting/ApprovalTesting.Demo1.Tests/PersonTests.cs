namespace ApprovalTesting.Demo1.Tests;

public class PersonTests
{
    [Fact]
    public Task PersonTest()
    {
        var now = DateTime.Now;
        var homer = new Person(
            "Homer",
            "Simpson",
            39,
            Guid.NewGuid(),
            now,
            now);

        return Verify(homer);
    }
}