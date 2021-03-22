using App.Interfaces;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.ViewsModels
{
   public class NotificationViewModel : BaseViewModel
    {
        public INavigationService NavigationService { get; set; }

        public ICommand NotificationPageNavCommand { get; set; }
        public NotificationViewModel(INavigationService navigationService) : base(navigationService)
        {
            NavigationService = navigationService;
            NotificationPageNavCommand = new Command(async () => await NotificationNavPage());
        }
        private async Task NotificationNavPage()
        {
            //await Navigation.PushAsync(new Notifications());
        }
    }
}
