using System.Net.Http.Headers;
using System.Text;

namespace MauiBasicAuth.DataAccess
{
    public class UserAuthentication{
        public bool AuthenticateUser(string username, string password)
        {
            using (var client = new HttpClient()) {
                // Set the base address of the API
                client.BaseAddress = new Uri("http://localhost:51966/");

                // Create the authentication header value
                var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

                // Set the endpoint to use the Values controller
                var endpoint = "api/Values";

                // Send a GET request
                var response = client.GetAsync(endpoint).Result;

                // Return true if authentication succeeded
                return response.IsSuccessStatusCode;
            }
        }
    }
}
