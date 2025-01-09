using Common;

namespace RestClientTests;

using RestClient;

public class RestClientTest
{
    [Test]
    public void TestHello()
    {
        new RestClient().Hello();
    }

    /// <summary>
    /// test case - can we do Create/Read/Update/Delete on the "Apples" resource
    /// </summary>
    ///
    /// the actual implementation can be improved
    [Test]
    public void TestApplesCrud()
    {
        var prefix = "TestApplesCrud";
        var client = new RestClient();
        {
            // ReSharper disable once JoinDeclarationAndInitializer
            List<Apple> apples;
            apples = client.GetApples();
            while (apples.Count > 0)
            {
                client.DeleteApple(apples[0]);
                apples = client.GetApples();
            }
        }
        Assert.That(client.GetApples().Count == 0);
        var created = client.CreateApple(new Apple($"{prefix}.1"));
        Assert.That(client.GetApples().Count == 1);
        client.UpdateApple(created.Name, new Apple($"{prefix}.2"));
        Assert.That(client.GetApples().Count == 1);
        Assert.That(client.GetApples()[0].Name.Equals($"{prefix}.2"));
        client.DeleteApple(client.GetApples()[0]);
        Assert.That(client.GetApples().Count == 0);
    }
}
