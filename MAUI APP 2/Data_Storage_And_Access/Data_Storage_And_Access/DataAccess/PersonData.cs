
using Data_Storage_And_Access.Models;
using SQLite;
using Newtonsoft.Json;

namespace Data_Storage_And_Access.DataAccess
{
    public class PersonData
    {
        private const string siteLink = "http://localhost:59961/api/Person";
        public async Task<List<Person>> GetPeopleAsync()
        {
            HttpClient client;
            try
            {
                client = new HttpClient();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "appliaction/json");
                List<Person> people = new List<Person>();

                var response = await client.GetAsync("http://localhost:59961/api/Person");
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(content))
                    {
                        people = JsonConvert.DeserializeObject<List<Person>>(content);
                    }
                }
                return people;
            }
            catch (Exception ex) { throw ex; }
        }
        public async Task<int> savePersonAsync(Person person)
        {
            HttpClient client;

            try
            {
                client = new HttpClient();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");

                var content = JsonConvert.SerializeObject(person);
                var buff = System.Text.Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buff);
                byteContent.Headers.ContentType =
                    new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                HttpResponseMessage response =
                    client.PostAsync(siteLink, byteContent).Result;

                return response.IsSuccessStatusCode ? 1 : 0;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task ClearAllPeropleAsync()
        {
            HttpClient client;
            try
            {
                client = new HttpClient();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");

                // Using .Result for the Delete action
                var response = client.DeleteAsync(siteLink).Result;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
