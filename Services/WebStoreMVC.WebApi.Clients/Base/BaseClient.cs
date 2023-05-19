using System.Net.Http.Json;
using System.Net;

namespace WebStoreMVC.WebApi.Clients.Base;

public abstract class BaseClient : IDisposable
{
	private bool _disposed;

	protected HttpClient HttpClient { get; }
    protected string Address { get; }

    public BaseClient(HttpClient httpClient, string address)
    {
        HttpClient = httpClient;
        Address = address;
    }

	protected T? Get<T>(string url) => GetAsync<T>(url).Result;

	protected async Task<T?> GetAsync<T>(string url, CancellationToken cancel = default)
	{
		var response = await HttpClient.GetAsync(url, cancel).ConfigureAwait(false);

		switch (response.StatusCode)
		{
			case HttpStatusCode.NoContent:
			case HttpStatusCode.NotFound:
				return default(T?);
			default:
				var result = await response
					.Content
					.ReadFromJsonAsync<T?>(cancellationToken: cancel)
					.ConfigureAwait(false);
				return result;
		}
	}

	protected HttpResponseMessage Post<T>(string url, T value) => PostAsync<T>(url, value).Result!;

	protected async Task<HttpResponseMessage> PostAsync<T>(string url, T value, CancellationToken cancel = default)
	{
		var response = await HttpClient.PostAsJsonAsync(url, value, cancel).ConfigureAwait(false);
		return response.EnsureSuccessStatusCode();
	}

	protected HttpResponseMessage Put<T>(string url, T value) => PutAsync<T>(url, value).Result!;

	protected async Task<HttpResponseMessage> PutAsync<T>(string url, T value, CancellationToken cancel = default)
	{
		var response = await HttpClient.PutAsJsonAsync(url, value, cancel).ConfigureAwait(false);
		return response.EnsureSuccessStatusCode();
	}

	protected HttpResponseMessage Delete(string url) => DeleteAsync(url).Result!;

	protected async Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken cancel = default)
	{
		var response = await HttpClient.DeleteAsync(url, cancel).ConfigureAwait(false);
		return response;
	}

	public void Dispose()
	{
		Dispose(true);
	}

	protected virtual void Dispose(bool dispose)
	{
		if (_disposed)
			return;

		_disposed = true;

		if (dispose)
		{
			// удаляем все управляемые ресурсы - те ссылочные объекты, который реализуют интерфейс IDisposible и были созданы внутри 
			// текущего объекта, те НЕ БЫЛИ переданы через конструктор из вне !!!
		}

		// удаляем неуправляемые ресурсы, те как подключение к бд, считывание и запись файла, вызов удаленного метода и тд.
	}
}
