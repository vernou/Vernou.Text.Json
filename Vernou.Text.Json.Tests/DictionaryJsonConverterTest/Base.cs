using System.Text.Json;
using Shouldly;

namespace Vernou.Text.Json.Tests.DictionaryJsonConverterTest;

public abstract class Base<T>
    where T : class, IDictionary<string, object>
{
    private static readonly JsonSerializerOptions _options;

    static Base()
    {
        _options = new()
        {
            TypeInfoResolver = JsonSerializerOptions.Default.TypeInfoResolver,
            Converters = { new DictionaryJsonConverter() }
        };
        _options.MakeReadOnly();
    }

    [Fact]
    public void DeserializeNull()
    {
        // Arrange

        var json = """{ "value": null }""";

        // Act

        var result = JsonSerializer.Deserialize<T>(json, _options);

        // Assert

        result.ShouldNotBeNull()["value"].ShouldBeNull();
    }

    [Fact]
    public void DeserializeInteger()
    {
        Deserialize(42L, """{ "value": 42 }""");
    }

    [Fact]
    public void DeserializeDecimal()
    {
        Deserialize(42.69m, """{ "value": 42.69 }""");
    }

    [Fact]
    public void DeserializeFalse()
    {
        Deserialize(false, """{ "value": false }""");
    }

    [Fact]
    public void DeserializeTrue()
    {
        Deserialize(true, """{ "value": true }""");
    }

    [Fact]
    public void DeserializeString()
    {
        Deserialize("Some Literral", """{ "value": "Some Literral" }""");
    }

    [Fact]
    public void DeserializeDateTime()
    {
        Deserialize(new DateTime(2000, 04, 1, 7, 31, 42), """{ "value": "2000-04-01T07:31:42" }""");
    }

    [Fact]
    public void DeserializeArray()
    {
        Deserialize(
            new List<object?> { null, false, true, 42L, 42.69m, "abcdefgh", new DateTime(2000, 04, 1, 7, 31, 42) },
            """
            {
              "value": [
                null,
                false,
                true,
                42,
                42.69,
                "abcdefgh",
                "2000-04-01T07:31:42"
              ]
            }
            """
        );
    }

    private static void Deserialize<E>(E expected, string json)
    {
        // Act

        var result = JsonSerializer.Deserialize<T>(json, _options);

        // Assert

        result.ShouldNotBeNull()["value"].ShouldBeOfType<E>().ShouldBe(expected);
    }
}
