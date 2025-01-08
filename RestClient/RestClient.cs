namespace RestClient;

public class RestClient
{
    public string Hello()
    {
        var httpClient = new HttpClient();
        var response = httpClient.GetAsync("http://localhost:5000").Result;
        response.EnsureSuccessStatusCode();
        var result = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        return result;
    }
}