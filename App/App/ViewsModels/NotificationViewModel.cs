using App.Services;
using App.ViewsModels;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.ViewsModel
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
        public override async Task Init()
        {
            await Task.Factory.StartNew(() =>
            {
            });
        }
        private async Task NotificationNavPage()
        {
            //await Navigation.PushAsync(new Notifications());
        }
    }
}
