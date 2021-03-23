using App.Interfaces;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App.ViewsModels
{
    public class MainViewModel : BaseViewModel
    {
        public INavigationService NavigationService { get; set; }
        public Command SecondPageNavCommand { get; }
        public Command RegisterPageNavCommand { get; }
        public Command NotificationPageNavCommand { get; }
        public MainViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.NavigationService = navigationService;
            SecondPageNavCommand = new Command(async () => await OnNavSecondPage());
            RegisterPageNavCommand = new Command(async () => await OnNavRegisterPage());
            NotificationPageNavCommand = new Command(async () => await NotificationNavPage());
        }       
        // Register Page
        private async Task OnNavRegisterPage()
        {
            await NavigationService.NavigateTo<RegisterViewModel>();
        }
        //Show data page
        private async Task OnNavSecondPage()
        {
            await NavigationService.NavigateTo<ShowDataViewModel>();
        }
        // Notifications Page
        private async Task NotificationNavPage()
        {
            await NavigationService.NavigateTo<NotificationViewModel>();
        }
    }
}
