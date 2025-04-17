# Vernou.Text.Json

This library extends **System.Text.Json**.

## Getting started

Install the package [Vernou.Text.Json](https://www.nuget.org/packages/Vernou.Text.Json) :

```sh
dotnet add package Vernou.Text.Json
```

## Json Converter

`JsonConverter` extends the serializer to convert an object or value to or from JSON.
### DictionaryJsonConverter

By default, `object` is deserialized to `JsonElement` :
```
var json = """{ "integer": 42 }""";
var dic = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
Console.WriteLine(dic["integer"].GetType());
// Output: System.Text.Json.JsonElement
```

With `DictionaryJsonConverter`, `object` is deserialized to the CLR type :
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

> `DictionaryJsonConverter` doesn't modify the serializer.
