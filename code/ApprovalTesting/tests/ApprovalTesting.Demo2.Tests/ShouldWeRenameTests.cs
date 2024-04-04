using System.Text.Json;

namespace ApprovalTesting.Demo2.Tests;

public class ShouldWeRenameTests
{
    [Fact]
    public Task Why_prop_order_comparison_sucks_for_json()
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