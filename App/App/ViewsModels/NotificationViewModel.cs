using App.Interfaces;
using App.Models;
using App.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.ViewsModels
{
   public class NotificationViewModel : BaseViewModel
    {
        public INavigationService NavigationService { get; set; }
        public ICommand ShowNotificationCommand { get; set; }
       
        private string loggerId;
        public string LoggerId { get { return loggerId; } set { loggerId = value; OnPropertyChanged(); } }

        private List<Log> logs;
        public List<Log> Logs { get { return logs; } set { logs = value; OnPropertyChanged(); } }

        public NotificationViewModel(INavigationService navigationService) : base(navigationService)
        {
            NavigationService = navigationService;
            ShowNotificationCommand = new Command(async () => await ShowNotification());
        }

        private async Task ShowNotification()
        {
            if (!String.IsNullOrWhiteSpace(loggerId))
            {
                Logs = await LoggerService.GetWarnings(loggerId);
            }                             
        }
    }
}
