using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using App.Helpers;
using App.Models;
using RestSharp;
using RestSharp.Serializers.SystemTextJson;

namespace App.Services
{
    class LoggerService
    {
        private static readonly string apiBaseUrl = Config.getApiBaseUrl();
        public static async Task<bool> DoesLoggerExist(string loggerId)
        {
            var client = new RestClient(apiBaseUrl);

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
            request.AddParameter("loggerId", plant.LoggerId);
            request.AddParameter("name", plant.Name);
            request.AddParameter("minimumTemperature", plant.MinimumTemperature);
            request.AddParameter("soilType", (int) plant.SoilType);
            request.AddFile("image", plant.Img.Data.data, "image.jpeg");

            var response = await client.ExecutePostAsync(request);

            return response.IsSuccessful;
        }
        public static async Task<Logger[]> GetLoggers()
        {
            var client = new RestClient(apiBaseUrl);

            var request = new RestRequest("logger", Method.GET);

            var response = await client.ExecuteAsync<Logger[]>(request);

            return response.Data;
        }
        public static async Task<List<Log>> GetWarnings(string loggerId)
        {
            var client = new RestClient(apiBaseUrl);

            var request = new RestRequest("logger/warnings/" + loggerId, Method.GET);

            var response = await client.ExecuteAsync<List<Log>>(request);

            return response.Data;
        }   
        public static async Task<Plant> GetPlant(string loggerId)
        {
            var client = new RestClient(apiBaseUrl);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            client.UseSystemTextJson(options);

            var request = new RestRequest("logger/active/" + loggerId, Method.GET);

            var response = await client.ExecuteAsync<Plant>(request);

            return response.Data;
        }
        public static async Task<Image> GetImage(string plantId)
        {
            var client = new RestClient(apiBaseUrl);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            client.UseSystemTextJson(options);

            var request = new RestRequest("plant/getfile/" + plantId, Method.GET);

            var response = await client.ExecuteAsync<Image>(request);

            return response.Data;
        }
    }
}
