using Newtonsoft.Json;
using RSVP_Application.Models;
using System.Net.Http.Json;
using System.Text;

namespace RSVP_Application.DataAccess
{
    public class WebService
    {
        HttpClient _client;
        public WebService()
        {
            _client = new HttpClient();

            var authData = 
                Encoding.UTF8.GetBytes($"{ApiConstants.AuthUsername}:{ApiConstants.AuthPassword}");
            var authHeader = 
                Convert.ToBase64String(authData);

            _client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authHeader);
        }

        public async Task<bool> PostEventToServer(Event ev)
        {
            try
            {
                var json = JsonConvert.SerializeObject(ev);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync(ApiConstants.BaseUrl + "Event", content);
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        public async Task<bool> PostUserToServer(User user)
        {
            try
            {
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync(ApiConstants.BaseUrl + "User", content);
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }
        public async Task<bool> PostRSVPToServer(int eventId, string username)
        {
            try
            {
                var response = await _client.PostAsJsonAsync($"api/events/{eventId}/rsvp", new { Username = username });
                return response.IsSuccessStatusCode;
            } 
            catch { return false; }
        }
        public async Task<bool> DeleteEventFromServer(int eventId)
        {
            try
            {
                string url = $"https://localhost.com:57825/api/events/{eventId}";
                var response = await _client.DeleteAsync(url);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting event: {ex.Message}");
                return false;
            }
        }
    }
}
