using App.Interfaces;
using App.Models;
using App.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.ViewsModels
{
   public class NotificationViewModel : BaseViewModel
    {
        public INavigationService NavigationService { get; set; }
        public ICommand NotificationPageNavCommand { get; set; }
        public ICommand ShowNotificationCommand { get; set; }
       
        private string loggerId;
        public string LoggerId { get { return loggerId; } set { loggerId = value; OnPropertyChanged(); } }

        private Log[] logs;
        public Log[] Logs { get { return logs; } set { logs = value; OnPropertyChanged(); } }

        public NotificationViewModel(INavigationService navigationService) : base(navigationService)
        {
            NavigationService = navigationService;
            NotificationPageNavCommand = new Command(async () => await NotificationNavPage());
            ShowNotificationCommand = new Command(async () => await ShowNotification());

        }
        private async Task NotificationNavPage()
        {
            //await Navigation.PushAsync(new Notifications());
        }

        // GET Notifications
        private async Task ShowNotification()
        {
            if (!String.IsNullOrWhiteSpace(loggerId))
            {
                Logs = new Log[]
                {
                    new Log {Temperature = 20, AirHumidity = 15, SoilHumidity = 10, LoggerId = "2"},
                    new Log {Temperature = 20, AirHumidity = 15, SoilHumidity = 10, LoggerId = "2"}
                };
                //Logs = await LoggerService.GetWarnings(loggerId);
            }                             
        }
    }
}
