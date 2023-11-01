using System.Text;
using System.Text.Json;
using PayPals.UI.Interfaces;

namespace PayPals.UI.Services
{
    public class RestService : IRestService
    {
        public string RestUrl { get; }
        public HttpClient Client { get; }
        public JsonSerializerOptions SerializerOptions { get; }

        public RestService()
        {
            RestUrl = "http://192.168.0.107:5000";
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

        public string StorageDataSerializer<T>(T data)
        {
            var content = JsonSerializer.Serialize<T>(data, SerializerOptions);
            //var content = new StringContent(json, Encoding.UTF8, "application/json");
            return content;
        }

        public async Task<T> Deserializer<T>(HttpResponseMessage data)
        {
            var dataString = await data.Content.ReadAsStringAsync();
            var x = typeof(T).Name;
            var y = nameof(String);
            var zz = x.Equals(y);
            if (zz)
            {
                return (T)(object)dataString;
            }

            var content = JsonSerializer.Deserialize<T>(dataString, SerializerOptions);
            return content;
        }

        public T StorageDataDeserializer<T>(string data)
        {
            var toBeConvertedType = typeof(T).Name;
            var checkIfTypeString = toBeConvertedType.Equals(nameof(String));
            if (checkIfTypeString)
            {
                return (T)(object)data;
            }
            //var dataString = new StringContent(data, Encoding.UTF8, "application/json");
            var content = JsonSerializer.Deserialize<T>(data, SerializerOptions);
            return content;
        }
    }
}
