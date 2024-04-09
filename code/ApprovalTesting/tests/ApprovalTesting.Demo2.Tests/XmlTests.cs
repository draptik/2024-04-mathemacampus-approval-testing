using JetBrains.Annotations;

namespace ApprovalTesting.Demo2.Tests;

public class XmlTests
{
    private const string InvalidXml = "<body> foo ";

    [Fact(Skip = "This does not fail fast, because the input is an invalid XML")]
    public Task Invalid_xml_demo1() =>
        Verify(InvalidXml);

    [Fact(Skip = "This fails fast, because the input is an invalid XML")]
    public Task Invalid_xml_demo2() =>
        VerifyXml(InvalidXml);

    [Fact]
    public Task Valid_xml_using_language_injection() =>
        VerifyXml(SampleXml);

    [LanguageInjection(InjectedLanguage.XML)]
    const string SampleXml =
        """
        <Person>
          <FirstName>Homer</FirstName>
          <LastName>Simpson</LastName>
          <Age>39</Age>
          <Id>00000000-0000-0000-0000-000000000000</Id>
          <DateOfBirth>2021-10-01T00:00:00</DateOfBirth>
          <Address />
        </Person>
        """;
}