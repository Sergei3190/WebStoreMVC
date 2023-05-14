namespace WebStoreMVC.WebApi.Clients.Base;

public abstract class BaseClient
{
    protected HttpClient HttpClient { get; }
    protected string Address { get; }

    public BaseClient(HttpClient httpClient, string address)
    {
        HttpClient = httpClient;
        Address = address;
    }
}
