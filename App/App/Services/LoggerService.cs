using System;
using System.Threading.Tasks;
using App.Models;
using RestSharp;

namespace App.Services
{
    class LoggerService
    {
        public static async Task<Uri> PairLogger(string loggerId)
        {
            var client = new RestClient("http://192.168.1.220:3000/api/");

            var request = new RestRequest("logger", Method.GET);
            request.AddHeader("Content-type", "application/json");
            var body = new
            {
                _id = loggerId
            };
            request.AddJsonBody(body);
            var response = await client.ExecuteAsync(request);

            return response.ResponseUri;
        }
        //static HttpClient client = new HttpClient();

        //static LoggerService()
        //{
        //    client.BaseAddress = new Uri("http://192.168.1.220:3000/api/");
        //}

        //public static async Task<Uri> SavePlant(Plant plant)
        //{
        //    var requestContent = new MultipartFormDataContent();
        //    requestContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

        //    var imageContent = new ByteArrayContent(plant.File);
        //    requestContent.Add(imageContent, "image");

        //    StringContent name = new StringContent(plant.Name);
        //    requestContent.Add(name, "name");

        //    StringContent loggerId = new StringContent(plant.LoggerId);
        //    requestContent.Add(loggerId, "loggerId");

        //    HttpResponseMessage response = await client.PostAsJsonAsync("plant", requestContent);
        //    response.EnsureSuccessStatusCode();

        //    return response.Headers.Location;
        //}

        public static async Task<Uri> SavePlant(Plant plant)
        {
            var client = new RestClient("http://192.168.1.220:3000/api/");

            var request = new RestRequest("plant", Method.POST);
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddParameter("name", plant.Name);
            request.AddParameter("loggerId", plant.LoggerId);
            request.AddFile("image", plant.File, "image.jpeg");

            var response = await client.ExecutePostAsync(request);

            return response.ResponseUri;
        }
    }
}
