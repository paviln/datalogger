using System;
using System.Net.Http;
using System.Threading.Tasks;
using App.Models;
using System.Net.Http.Headers;

namespace App.Services
{
    class LoggerService
    {
        static HttpClient client = new HttpClient();

        static LoggerService()
        {
            client.BaseAddress = new Uri("http://192.168.1.220:3000/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
       
        public static async Task<Uri> SavePlant(Plant plant)
        {
            var plantPoco = new
            {
                name = plant.Name,
                loggerId = plant.LoggerId
            };
            HttpResponseMessage response = await client.PostAsJsonAsync("plant", plantPoco);
            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }
    }
}
