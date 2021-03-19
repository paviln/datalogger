using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using App.Models;
using App.Interfaces;
using Xamarin.Forms;

namespace App.Services
{
    class RegisterService
    {
        public static async Task<Plant> GetPlant()
        {
            Plant plant = null;
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            IHttpClientHandler httpClientHandler = DependencyService.Get<IHttpClientHandler>();
            using (var httpClient = new HttpClient(clientHandler))
            {
                try
                {
                    httpClient.BaseAddress = new Uri("http://localhost:3000/");
                    var response = httpClient.GetAsync(new Uri("/api/logger")).Result;
                    response.EnsureSuccessStatusCode();
                    await response.Content.ReadAsStringAsync().ContinueWith((Task<string> data) =>
                    {
                        if (data.IsFaulted)
                            throw data.Exception;

                        plant = JsonSerializer.Deserialize<Plant>(data.Result);
                    });

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return plant;
        }
    }
}
