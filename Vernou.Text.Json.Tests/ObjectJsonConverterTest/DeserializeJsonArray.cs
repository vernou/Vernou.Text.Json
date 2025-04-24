using System.Text.Json;
using Shouldly;

namespace Vernou.Text.Json.Tests.ObjectJsonConverterTest;

public class DeserializeJsonArray
{

    [Fact]
    public void DeserializeEmptyJsonArrayToObject()
    {
        DeserializeEmptyJsonArrayTo<object>();
    }

    private void DeserializeEmptyJsonArrayTo<T>()
    {
        // Arrange

        var options = new JsonSerializerOptions { Converters = { new ObjectJsonConverter() } };
        var json = "[]";

        // Act

        var result = JsonSerializer.Deserialize<T>(json, options);

        // Assert

        result.ShouldBeOfType<List<object>>().ShouldBeEmpty();
    }

    [Fact]
    public void DeserializeJsonArrayToObject()
    {
        DeserializeJsonArrayTo<object, List<object>>();
    }

    [Fact]
    public void DeserializeJsonArrayToIEnumerable()
    {
        DeserializeJsonArrayTo<IEnumerable<object>, List<object>>();
    }

    [Fact]
    public void DeserializeJsonArrayToList()
    {
        DeserializeJsonArrayTo<List<object>>();
    }

    [Fact]
    public void DeserializeJsonArrayToArray()
    {
        DeserializeJsonArrayTo<object[]>();
    }

    private void DeserializeJsonArrayTo<T>()
        where T : IEnumerable<object>
    {
        DeserializeJsonArrayTo<T, T>();
    }

    private void DeserializeJsonArrayTo<T, E>()
        where E : IEnumerable<object>
    {
        // Arrange

        var options = new JsonSerializerOptions { Converters = { new ObjectJsonConverter() } };
        var json =
            """
            [
              null,
              false,
              true,
              42,
              42.69,
              "Hello",
              {}
            ]
            """;

        // Act

        var result = JsonSerializer.Deserialize<T>(json, options);

        // Assert

        var list = result.ShouldBeOfType<E>();
        list.Count().ShouldBe(7);
        list.ElementAt(0).ShouldBeNull();
        list.ElementAt(1).ShouldBe(false);
        list.ElementAt(2).ShouldBe(true);
        list.ElementAt(3).ShouldBe(42L);
        list.ElementAt(4).ShouldBe(42.69m);
        list.ElementAt(5).ShouldBe("Hello");
        list.ElementAt(6).ShouldBeOfType<Dictionary<string, object>>().ShouldBeEmpty();

    }
}
