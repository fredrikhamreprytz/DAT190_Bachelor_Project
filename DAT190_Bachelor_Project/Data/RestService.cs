using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAT190_Bachelor_Project.Model;
using Newtonsoft.Json;

namespace DAT190_Bachelor_Project.Data
{
    public class RestService
    {
        HttpClient client;
        List<User> users;
        public static string RestUrl = "http://localhost:5000/api/values";

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<List<User>> RefreshDataAsync()
        {
            
            var uri = new Uri(string.Format(RestUrl, string.Empty));
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<User>>(content);
            }
            return users;

        }

        public async Task SaveUserAsync(User user)
        {
            
            var uri = new Uri(string.Format(RestUrl, string.Empty));

            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = null;
            if (user.Email == null)
            {
                responseMessage = await client.PostAsync(uri, content);
            }
            else
            {
                responseMessage = await client.PutAsync(uri, content);
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"User successfully saved");
            }

        }

        public async Task DeleteUserAsync(string email)
        {

            var uri = new Uri(string.Format(RestUrl, email));

            var response = await client.DeleteAsync(uri);  
        }

    }
}
