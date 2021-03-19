using System.Net.Http;

namespace App.Interfaces
{
    public interface IHttpClientHandler
    {
        HttpClientHandler GetInsecureHandler();
    }
}
