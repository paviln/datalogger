using App.Interfaces;
using App.Models;
using App.Services;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.ViewsModels
{
   public class ShowDataViewModel : BaseViewModel
    {
        public INavigationService NavigationService { get; set; }
        public ICommand SecondPageNavCommand { get; set; }
        public ICommand ShowDataCommand { get; set; }
        private Logger[] loggers;
        public Logger[] Loggers { get { return loggers; } set { loggers = value; OnPropertyChanged(); } } 
        public ShowDataViewModel(INavigationService navigationService) : base(navigationService)
        {
            NavigationService = navigationService;
            SecondPageNavCommand = new Command(async () => await OnNavSecondPage());
            ShowDataCommand = new Command(async () => await GetData());

        }
        private async Task OnNavSecondPage()
        {
            //await Navigation.PushAsync(new ShowData());
        }
        //Http GET
        private async Task GetData()
        {
            var loggers = await LoggerService.GetLoggers();
            Loggers = new Logger[]
            {
                new Logger(){MinimumTemperature = 5},
                new Logger(){MinimumTemperature = 5}

            };
            
        }
    }
}
