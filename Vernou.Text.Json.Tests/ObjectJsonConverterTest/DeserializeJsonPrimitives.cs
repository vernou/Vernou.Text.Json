using System.Text.Json;
using Shouldly;

namespace Vernou.Text.Json.Tests.ObjectJsonConverterTest;

public class DeserializeJsonPrimitives
{
    [Fact]
    public void DeserializeJsonNullToNull()
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
    public void DeserializeJsonIntegerToInt64()
    {
        Deserialize(42L, "42");
    }

    [Fact]
    public void DeserializeJsonDecimalToDecimal()
    {
        Deserialize(42.69m, "42.69");
    }

    [Fact]
    public void DeserializeJsonStringToString()
    {
        Deserialize("Hello", "\"Hello\"");
    }

    [Fact]
    public void DeserializeEmptyJsonStringToString()
    {
        Deserialize("", "\"\"");
    }

    [Fact]
    public void DeserializeJsonFalseToBoolean()
    {
        Deserialize(false, "false");
    }

    [Fact]
    public void DeserializeJsonTrueToBoolean()
    {
        Deserialize(true, "true");
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
