using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using MAUIREST.Models;

namespace MAUIREST.DataAccess;

public class ItemService 
{
    private readonly HttpClient _client;
    // This MUST match the port (62115) from your Web API and UserAuthentication.cs
    private readonly string _url = "http://localhost:62115/api/Item";

    public ItemService()
    {
        _client = new HttpClient();
    }

    public async Task<List<Item>> GetItemsAsync(string username, string password)
    {
        try
        {
            // Create the Basic Auth header
            var authData = Encoding.UTF8.GetBytes($"{username}:{password}");
            var authToken = Convert.ToBase64String(authData);

            // Set the Authorization header for this request
            var request = new HttpRequestMessage(HttpMethod.Get, _url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authToken);

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Item>>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Item>();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
        }
        return new List<Item>();
    }

    public async Task<bool> SaveItemAsync(Item item, string username, string password)
    {
        try
        {
            var authData = Encoding.UTF8.GetBytes($"{username}:{password}");
            var authToken = Convert.ToBase64String(authData);

            var json = JsonSerializer.Serialize(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Attach auth header to the POST request
            var request = new HttpRequestMessage(HttpMethod.Post, _url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authToken);
            request.Content = content;

            var response = await _client.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }
}