using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace MAUIREST.DataAccess
{
    public class UserAuthentication
    {
        public bool AuthenticateUser(string username, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:62115/");

                var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
                var endpoint = "api/Item";
                var response = client.GetAsync(endpoint).Result;
                System.Diagnostics.Debug.WriteLine($"SERVER RESPONSE: {response.StatusCode}");
                return response.IsSuccessStatusCode;
            }
        }
    }
}
