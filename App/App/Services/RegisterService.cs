using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App.Commen;
using App.Models;

namespace App.Services
{
    class RegisterService
    {
        public static async Task<Plant> GetPlant()
        {
            var plant = await HttpClientWrapper<Plant>.Get("/api/logger");

            return plant;
        }
    }
}
