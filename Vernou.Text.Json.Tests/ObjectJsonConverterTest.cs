using System.Text.Json;
using Shouldly;

namespace Vernou.Text.Json.Tests;

public class ObjectJsonConverterTest
{
    [Fact]
    public void DeserializeNull()
    {
        // Arrange

        var json = "null";
        var options = new JsonSerializerOptions { Converters = { new ObjectJsonConverter() } };

        // Act

        var result = JsonSerializer.Deserialize<object>(json, options);

        // Assert

        result.ShouldBeNull();
    }

    [Fact]
    public void DeserializeInteger()
    {
        // Arrange

        var json = "42";
        var options = new JsonSerializerOptions { Converters = { new ObjectJsonConverter() } };

        // Act

        var result = JsonSerializer.Deserialize<object>(json, options);

        // Assert

        result.ShouldBe(42L);
    }

    [Fact]
    public void DeserializeDecimal()
    {
        Deserialize(42.69m, "42.69");
    }

    [Fact]
    public void DeserializeString()
    {
        Deserialize("Hello", "\"Hello\"");
    }

    [Fact]
    public void DeserializeEmptyString()
    {
        Deserialize("", "\"\"");
    }

    [Fact]
    public void DeserializeFalse()
    {
        Deserialize(false, "false");
    }

    [Fact]
    public void DeserializeTrue()
    {
        Deserialize(true, "true");
    }

    [Fact]
    public void DeserializeEmptyArray()
    {
        Deserialize(new List<object>(), "[]");
    }

    [Fact]
    public void DeserializeArray()
    {
        Deserialize(
            new List<object?> { null, false, true, 42L, 42.69m, "Hello" },
            """
            [
              null,
              false,
              true,
              42,
              42.69,
              "Hello"
            ]
            """
        );
    }

    [Fact]
    public void DeserializeEmptyObject()
    {
        Deserialize(new Dictionary<string, object>(), "{}");
    }

    [Fact]
    public void DeserializeObject()
    {
        // Arrange

        var options = new JsonSerializerOptions { Converters = { new ObjectJsonConverter() } };
        var json =
            """
            {
              "null": null,
              "false": false,
              "true": true,
              "integer": 42,
              "decimal": 42.69,
              "string": "Hello",
              "array": [],
              "object": {}
            }
            """;

        // Act

        var result = JsonSerializer.Deserialize<object>(json, options);

        // Assert

        var dic = result.ShouldBeOfType<Dictionary<string, object?>>();
        dic.Count.ShouldBe(8);
        dic["null"].ShouldBeNull();
        dic["false"].ShouldBe(false);
        dic["true"].ShouldBe(true);
        dic["integer"].ShouldBe(42L);
        dic["decimal"].ShouldBe(42.69m);
        dic["string"].ShouldBe("Hello");
        dic["array"].ShouldBeOfType<List<object>>().Count.ShouldBe(0);
        dic["object"].ShouldBeOfType<Dictionary<string, object>>().Count.ShouldBe(0);
    }

    private static void Deserialize<E>(E expected, string json)
    {
        // Arrange

        var options = new JsonSerializerOptions { Converters = { new ObjectJsonConverter() } };

        // Act

        var result = JsonSerializer.Deserialize<object>(json, options);

        // Assert

        result.ShouldBeOfType<E>().ShouldBe(expected);
    }
}
