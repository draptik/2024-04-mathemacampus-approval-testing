using System.Text.Json;

namespace ApprovalTesting.Demo2.Tests;

public class JsonTests
{
    [Fact]
    public Task Person_serialization_demo()
    {
        var person = new Person(
            "Homer",
            "Simpson",
            39,
            Guid.NewGuid(),
            DateTime.UtcNow,
            null);

        var json = JsonSerializer.Serialize(person);
        return VerifyJson(json);
    }
}