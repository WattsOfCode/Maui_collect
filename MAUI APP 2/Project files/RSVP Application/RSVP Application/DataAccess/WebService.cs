using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using RSVP_Application.Models;

namespace RSVP_Application.DataAccess
{
    public class WebService
    {
        HttpClient _client;
        string _baseUrl = "http://localhost:5000/api/events";

        public WebService()
        {
            _client = new HttpClient();
        }

        public async Task PostEventToServer(Event ev)
        {
            var json = JsonSerializer.Serialize(ev);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _client.PostAsync(_baseUrl, content);
        }
    }
}
