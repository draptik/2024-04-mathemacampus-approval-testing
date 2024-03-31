namespace ApprovalTesting.Demo1.Tests;

public class PersonTests
{
    [Fact]
    public Task PersonTest()
    {
        var now = new DateTime(2021, 10, 1);
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