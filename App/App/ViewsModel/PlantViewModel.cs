using App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace App.ViewsModel
{
    public class PlantViewModel 
    {
        public Command SavePlantCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Plant plant = new Plant();
                    plant.Id = Id;
                    plant.Name = Name;

                    string url = "";
                    HttpClient client = new HttpClient();
                    string jsonData = JsonConvert.SerializeObject(plant);
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    string result = await response.Content.ReadAsStringAsync();
                    Response responseData = JsonConvert.DeserializeObject<Response>(result);
                    if (responseData.Status == 1)
                    {
                        await Navigation.PopAsync();
                    }
                    else
                    {

                    }
                });
            }
        }

        string _id;
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                if(value != null)
                {
                    _id = value;
                    
                }
            }
        }

        string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != null)
                {
                    _name = value;

                }
            }
        }
    }
}
