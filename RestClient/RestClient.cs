using System.Text;
using Common;
using System.Text.Json;

namespace RestClient;

public class RestClient
{
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    readonly Configuration _settings;
    readonly HttpClient _client;

    public RestClient() : this(new Configuration())
    {
    }

    public RestClient(Configuration settings)
    {
        _settings = settings;
        _client = new HttpClient();
        _client.BaseAddress = new Uri(_settings.BaseUrl);
    }

    public string Hello()
    {
        var response = _client.GetAsync("/").Result;
        response.EnsureSuccessStatusCode();
        var result = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        return result;
    }

    public List<Apple> GetApples()
    {
        var response = _client.GetAsync("/apples");
        var result = response.ConfigureAwait(false).GetAwaiter().GetResult();
        result.EnsureSuccessStatusCode();
        var responseBody = result.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        var responseData = JsonSerializer.Deserialize<List<Apple>>(responseBody);
        if (responseData == null)
            throw new ApplicationException("Response is null");
        return responseData;
    }

    StringContent JsonBody(object body) => new(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

    public Apple CreateApple(Apple apple)
    {
        var response = _client.PostAsync("/apples", JsonBody(apple));
        var result = response.ConfigureAwait(false).GetAwaiter().GetResult();
        result.EnsureSuccessStatusCode();
        var responseBody = result.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        var responseData = JsonSerializer.Deserialize<Apple>(responseBody);
        if (responseData == null)
            throw new ApplicationException("Response is null");
        return responseData;
    }

    public Apple GetApple(string name)
    {
        var response = _client.GetAsync($"/apples/{name}");
        var result = response.ConfigureAwait(false).GetAwaiter().GetResult();
        result.EnsureSuccessStatusCode();
        var responseBody = result.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        var responseData = JsonSerializer.Deserialize<Apple>(responseBody);
        if (responseData == null)
            throw new ApplicationException("Response is null");
        return responseData;
    }

    public Apple UpdateApple(string id, Apple apple)
    {
        var response = _client.PutAsync($"/apples/{id}", JsonBody(apple));
        var result = response.ConfigureAwait(false).GetAwaiter().GetResult();
        result.EnsureSuccessStatusCode();
        var responseBody = result.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        var responseData = JsonSerializer.Deserialize<Apple>(responseBody);
        if (responseData == null)
            throw new ApplicationException("Response is null");
        return responseData;
    }

    public Apple DeleteApple(Apple apple)
    {
        var response = _client.DeleteAsync($"/apples/{apple.Name}");
        var result = response.ConfigureAwait(false).GetAwaiter().GetResult();
        result.EnsureSuccessStatusCode();
        var responseBody = result.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        var responseData = JsonSerializer.Deserialize<Apple>(responseBody);
        if (responseData == null)
            throw new ApplicationException("Response is null");
        return responseData;
    }

    public class Configuration
    {
        // ReSharper disable once PropertyCanBeMadeInitOnly.Global
        public string BaseUrl { get; set; } = "http://localhost:5000";
    }
}
