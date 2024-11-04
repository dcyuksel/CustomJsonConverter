using System.Text.Json;
using System.Text.Json.Serialization;
using CustomJsonConverter.Models;

namespace CustomJsonConverter.Converters;

public class PersonJsonConverter : JsonConverter<Person>
{
    public override Person? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        using var jsonDocument = JsonDocument.ParseValue(ref reader);
        var firstName = jsonDocument.RootElement.GetProperty("firstName").GetString();
        var lastName = jsonDocument.RootElement.GetProperty("lastName").GetString();
        var birthday = DateOnly.Parse(jsonDocument.RootElement.GetProperty("birthday").GetString()!);

        return new Person(firstName!, lastName!, birthday);
    }

    public override void Write(Utf8JsonWriter writer, Person person, JsonSerializerOptions options)
    {
        var name = person.FirstName + " " + person.LastName;
        var age = DateTime.Now.Year - person.Birthday.Year;

        writer.WriteStartObject();
        writer.WriteString(nameof(name), name);
        writer.WriteNumber(nameof(age), age);
        writer.WriteEndObject();
    }
}
