using App.Models;
using App.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.ViewsModels
{
   public class ShowDataViewModel : BaseViewModel
    {
        public ICommand SecondPageNavCommand { get; set; }
        public ShowDataViewModel(INavigation navigation): base(navigation)
        {
            SecondPageNavCommand = new Command(async () => await OnNavSecondPage());
        }
        private async Task OnNavSecondPage()

        {
            await Navigation.PushAsync(new ShowData());

        }
        //Http GET
        private async void GetData()
        {
            HttpClient client = new HttpClient();
            var response =  await client.GetStringAsync("");//pass url for api
            var plants = JsonConvert.DeserializeObject<List<Plant>>(response);
            PlantDataListView.ItemsSource = plants;
        }
    }
}
