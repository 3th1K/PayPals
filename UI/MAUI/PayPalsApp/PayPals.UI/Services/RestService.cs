using System.Text;
using System.Text.Json;
using PayPals.UI.DTOs;
using PayPals.UI.Interfaces;

namespace PayPals.UI.Data
{
    public class RestService : IRestService
    {
        public string RestUrl { get; }
        public HttpClient Client { get; }
        public JsonSerializerOptions SerializerOptions { get; }

        public RestService()
        {
            RestUrl = "http://192.168.0.106:5000";
            Client = new HttpClient();
            Client.Timeout = TimeSpan.FromSeconds(15);
            SerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public StringContent Serializer<T>(T data)
        {
            var json = JsonSerializer.Serialize<T>(data, SerializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return content;
        }

        public async Task<T> Deserializer<T>(HttpResponseMessage data)
        {
            var dataString = await data.Content.ReadAsStringAsync();
            var x = typeof(T).Name;
            var y = nameof(String);
            var z = x == y;
            var zz = x.Equals(y);
            if (zz)
            {
                return (T)(object)dataString;
            }

            var content = JsonSerializer.Deserialize<T>(dataString, SerializerOptions);
            return content;
        }
    }
}
