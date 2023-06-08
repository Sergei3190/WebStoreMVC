using System.Net;
using System.Net.Http.Json;

using WebStoreMVC.Interfaces.TestApi;
using WebStoreMVC.WebApi.Clients.Base;

namespace WebStoreMVC.WebApi.Clients.Values;

public class ValuesClient : BaseClient, IValueService
{
    public ValuesClient(HttpClient httpClient)
        : base(httpClient, "api/values")
    {

    }

    public IEnumerable<string> GetValues()
    {
        var response = HttpClient.GetAsync(Address).Result;

        if (response.StatusCode == HttpStatusCode.NoContent)
            return Enumerable.Empty<string>();

        if (response.IsSuccessStatusCode)
            return response.Content.ReadFromJsonAsync<IEnumerable<string>>().Result!;

        return Enumerable.Empty<string>();
    }

    public string? GetById(int id)
    {
        var response = HttpClient.GetAsync($"{Address}/{id}").Result;

        if (response.IsSuccessStatusCode)
            return response.Content.ReadFromJsonAsync<string?>().Result!;

        return null;
    }

    public void Add(string value)
    {
        var response = HttpClient.PostAsJsonAsync(Address, value).Result;
        response.EnsureSuccessStatusCode();
    }

    public void Edit(int id, string value)
    {
        var response = HttpClient.PostAsJsonAsync($"{Address}/{id}", value).Result;
        response.EnsureSuccessStatusCode();
    }

    public bool Delete(int id)
    {
        var response = HttpClient.DeleteAsync($"{Address}/{id}").Result;

        if (response.StatusCode == HttpStatusCode.NoContent)
            return false;

        if (response.IsSuccessStatusCode)
            return true;

        response.EnsureSuccessStatusCode();

        throw new InvalidOperationException();
    }
}
