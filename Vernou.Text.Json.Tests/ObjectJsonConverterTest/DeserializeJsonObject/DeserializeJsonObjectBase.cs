using System.Text.Json;
using Shouldly;

namespace Vernou.Text.Json.Tests.ObjectJsonConverterTest.DeserializeJsonObject;

public abstract class DeserializeJsonObjectBase<T>
{
    [Fact]
    public void DeserializeEmptyObject()
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

    [Fact]
    public void DeserializeObjectWithNested()
    {
        // Arrange

        var options = new JsonSerializerOptions { Converters = { new ObjectJsonConverter() } };
        var json =
            """
            {
              "child1": {
                "child1-a": { },
                "child1-b": { }
              },
              "child2": {
                "child2-a": { },
                "child2-b": { }
              }
            }
            """;

        // Act

        var result = JsonSerializer.Deserialize<T>(json, options);

        // Assert

        var root = result.ShouldBeOfType<Dictionary<string, object>>();
        root.Count.ShouldBe(2);
        var child1 = root["child1"].ShouldBeOfType<Dictionary<string, object>>();
        child1.Count.ShouldBe(2);
        child1["child1-a"].ShouldBeOfType<Dictionary<string, object>>().ShouldBeEmpty();
        child1["child1-b"].ShouldBeOfType<Dictionary<string, object>>().ShouldBeEmpty();
        var child2 = root["child2"].ShouldBeOfType<Dictionary<string, object>>();
        child2.Count.ShouldBe(2);
        child2["child2-a"].ShouldBeOfType<Dictionary<string, object>>().ShouldBeEmpty();
        child2["child2-b"].ShouldBeOfType<Dictionary<string, object>>().ShouldBeEmpty();
    }
}
