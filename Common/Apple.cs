using System.Text.Json.Serialization;

namespace Common;

public class Apple(string name)
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = name;
}
