using System.Text.Json;

namespace PayPals.UI.Interfaces
{
    public interface IRestService
    {
        string RestUrl { get; }
        HttpClient Client { get; }
        JsonSerializerOptions SerializerOptions { get; }
        StringContent Serializer<T>(T data);
        Task<T> Deserializer<T>(HttpResponseMessage data);
        T StorageDataDeserializer<T>(string data);
        string StorageDataSerializer<T>(T data);
    }
}
