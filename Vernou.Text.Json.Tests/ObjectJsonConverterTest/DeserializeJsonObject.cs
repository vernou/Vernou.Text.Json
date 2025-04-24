using System.Text.Json;
using Shouldly;

namespace Vernou.Text.Json.Tests.ObjectJsonConverterTest;

public class DeserializeJsonObject
{
    [Fact]
    public void DeserializeEmptyObjectToObject()
    {
        DeserializeEmptyObjectTo<object>();
    }

    [Fact]
    public void DeserializeEmptyObjectToDictionary()
    {
        DeserializeEmptyObjectTo<Dictionary<string, object>>();
    }

    [Fact]
    public void DeserializeEmptyObjectToIDictionary()
    {
        DeserializeEmptyObjectTo<IDictionary<string, object>>();
    }

    private static void DeserializeEmptyObjectTo<T>()
    {
        // Arrange

        var options = new JsonSerializerOptions { Converters = { new ObjectJsonConverter() } };
        var json = "{}";

        // Act

        var result = JsonSerializer.Deserialize<T>(json, options);

        // Assert

        result.ShouldBeOfType<Dictionary<string, object>>().ShouldBeEmpty();
    }

    [Fact]
    public void DeserializeObjectToObject()
    {
        DeserializeObjectTo<object>();
    }

    [Fact]
    public void DeserializeObjectToDictionary()
    {
        DeserializeObjectTo<Dictionary<string, object>>();
    }

    [Fact]
    public void DeserializeObjectToIDictionary()
    {
        DeserializeObjectTo<IDictionary<string, object>>();
    }

    private static void DeserializeObjectTo<T>()
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

        var result = JsonSerializer.Deserialize<T>(json, options);

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
}
