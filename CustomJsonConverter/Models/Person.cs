using CustomJsonConverter.Converters;
using System.Text.Json.Serialization;

namespace CustomJsonConverter.Models;

[JsonConverter(typeof(PersonJsonConverter))]
public record Person(string FirstName, string LastName, DateOnly Birthday);