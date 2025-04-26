using System.Text.Json.Serialization;
using System.Text.Json;

namespace Vernou.Text.Json;

public class ObjectJsonConverter : JsonConverter<object?>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(object);
    }

    public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                return reader.GetString();
            case JsonTokenType.Null:
                return null;
            case JsonTokenType.False:
                return false;
            case JsonTokenType.True:
                return true;
            case JsonTokenType.Number:
                if (reader.TryGetInt64(out var result))
                {
                    return result;
                }
                return reader.GetDecimal();
            case JsonTokenType.StartObject:
                var dictionary = new Dictionary<string, object?>();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndObject)
                    {
                        return dictionary;
                    }

                    if (reader.TokenType != JsonTokenType.PropertyName)
                    {
                        throw new JsonException("JsonTokenType was not PropertyName");
                    }

                    var propertyName = reader.GetString();

                    if (string.IsNullOrWhiteSpace(propertyName))
                    {
                        throw new JsonException("Failed to get property name");
                    }

                    reader.Read();

                    dictionary.Add(propertyName!, Read(ref reader, typeToConvert, options));
                }

                return dictionary;
            case JsonTokenType.StartArray:
                var list = new List<object?>();
                while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                {
                    list.Add(Read(ref reader, typeToConvert, options));
                }
                return list;
            default:
                throw new JsonException($"'{reader.TokenType}' is not supported");
        }
    }

    public override void Write(Utf8JsonWriter writer, object? value, JsonSerializerOptions options)
    {
#pragma warning disable CA1869 // Cache and reuse 'JsonSerializerOptions' instances
        var o = new JsonSerializerOptions(options);
#pragma warning restore CA1869 // Cache and reuse 'JsonSerializerOptions' instances
        o.Converters.Remove(this);
        JsonSerializer.Serialize(writer, value, o);
    }
}
