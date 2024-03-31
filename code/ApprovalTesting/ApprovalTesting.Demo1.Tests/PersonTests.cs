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
            Guid.Parse("d9b3f0b4-3b3b-4b3b-8b3b-3b3b3b3b3b3b"),
            now,
            now);

        return Verify(homer);
    }
}