namespace ApprovalTesting.Demo0.Tests;

public class PersonTests
{
    [Fact]
    public Task Test1()
    {
        var homer = new Person("Homer", "Simpson", 39);
        return Verify(homer);
    }
}