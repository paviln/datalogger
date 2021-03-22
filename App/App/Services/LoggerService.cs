using System;
using System.Text.Json;
using System.Threading.Tasks;
using App.Helpers;
using App.Models;
using RestSharp;

namespace App.Services
{
    class LoggerService
    {
        private static readonly string apiBaseUrl = Config.getApiBaseUrl();
        public static async Task<bool> DoesLoggerExist(string loggerId)
        {
            var client = new RestClient("http://10.0.2.2:3000/api/");

            var request = new RestRequest("logger", Method.GET);
            request.AddHeader("Content-type", "application/json");
            var body = new
            {
                _id = loggerId
            };
            request.AddJsonBody(body);
            var response = await client.ExecuteAsync(request);
            Console.WriteLine(JsonSerializer.Deserialize<IRestResponse>(response.Content));

            if ((int)response.ResponseStatus == 200)
            {
                return true;
            }

            return false;
        }
        public static async Task<bool> SavePlant(Plant plant)
        {
            var client = new RestClient(apiBaseUrl);

            var request = new RestRequest("plant", Method.POST);
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddParameter("name", plant.Name);
            request.AddParameter("loggerId", plant.LoggerId);
            request.AddFile("image", plant.File, "image.jpeg");

            var response = await client.ExecutePostAsync(request);

            return response.IsSuccessful;
        }
    }
}
