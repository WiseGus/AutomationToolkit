namespace Blazor.Services;

using System.Net.Http.Json;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5000/api/");
    }

    public async Task<T> Get<T>(string controller, string action)
    {
        var res = await _httpClient.GetFromJsonAsync<T>($"{controller}/{action}");
        return res;
    }

    public async Task<IEnumerable<T>> GetAll<T>(string controller)
    {
        var res = await _httpClient.GetFromJsonAsync<IEnumerable<T>>(controller);
        return res;
    }

    public async Task<T> GetSingle<T>(string controller, string id)
    {
        var res = await _httpClient.GetFromJsonAsync<T>($"{controller}/{id}");
        return res;
    }

    public async Task<T> Post<T>(string controller, T itemToUpdate)
    {
        var res = await _httpClient.PostAsJsonAsync(controller, itemToUpdate);
        return default;
    }

    public async Task<T> Update<T>(string controller, string id, T itemToUpdate)
    {
        var res = await _httpClient.PutAsJsonAsync($"{controller}/{id}", itemToUpdate);
        return default;
    }

    public async Task<T> Delete<T>(string controller, string id)
    {
        var res = await _httpClient.DeleteAsync($"{controller}/{id}");
        return default;
    }
}
