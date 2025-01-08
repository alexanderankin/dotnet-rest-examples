namespace RestClientTests;

using RestClient;

public class RestClientTest
{
    [Test]
    public void TestHello()
    {
        new RestClient().Hello();
    }
}