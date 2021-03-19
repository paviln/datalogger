using System.Net.Http;
using App.Interfaces;
using App.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(HttpConfig))]
namespace App.Droid
{
    public class HttpConfig : IHttpClientHandler
    {
        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }
    }
}