# Vernou.Text.Json

This library extends **System.Text.Json**.

## Getting started

Install the package [Vernou.Text.Json](https://www.nuget.org/packages/Vernou.Text.Json) :

```sh
dotnet add package Vernou.Text.Json
```

## Json Converter

`JsonConverter` extends the serializer to convert an object or value to or from JSON.

### OjbectJsonConverter

`OjbectJsonConverter` is a `JsonConverter` that deserialize from json to `object`. It is mainly used to deserialize a collection containing multiple types, such `IEnumerable<object>` or `IDictionary<string, object>`.

Example :
```
var json =
    """
    {
      "null": null,
      "integer": 42,
      "decimal": 42.69,
      "boolean": true,
      "string": "Hello!",
      "object": {},
      "array": []
    }
    """;
var options = new JsonSerializerOptions { Converters = { new DictionaryJsonConverter() } };
var dic = JsonSerializer.Deserialize<Dictionary<string, object>>(json, options);
Console.WriteLine(dic["null"] ?? "<null>");  // Output: <null>
Console.WriteLine(dic["integer"].GetType()); // Output: System.Int64
Console.WriteLine(dic["decimal"].GetType()); // Output: System.Decimal
Console.WriteLine(dic["boolean"].GetType()); // Output: System.Boolean
Console.WriteLine(dic["string"].GetType());  // Output: System.String
Console.WriteLine(dic["object"].GetType());  // Output: System.Collections.Generic.Dictionary`2[System.String,System.Object]
Console.WriteLine(dic["array"].GetType());   // Output: System.Collections.Generic.List`1[System.Object]
```

> `ObjectJsonConverter` has no impact on the serialization.

## Contributing

All contributions are welcome. See [CONTRIBUTING.md](CONTRIBUTING.md) for details on how to contribute to this project.

## License

This project is licensed under the [MIT](LICENSE) License.