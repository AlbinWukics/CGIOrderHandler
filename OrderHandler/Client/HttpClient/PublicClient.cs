namespace OrderHandler.Client.HttpClient;
using System.Net.Http;

public class PublicClient
{
    public HttpClient Client { get; set; }

    public PublicClient(HttpClient client)
    {
        Client = client;
    }
}