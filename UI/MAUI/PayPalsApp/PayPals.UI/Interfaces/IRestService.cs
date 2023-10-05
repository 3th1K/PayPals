using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PayPals.UI.Interfaces
{
    public interface IRestService
    {
        string RestUrl { get; }
        HttpClient Client { get; }
        JsonSerializerOptions SerializerOptions { get; }
        StringContent Serializer<T>(T data);
        Task<T> Deserializer<T>(HttpResponseMessage data);
    }
}
