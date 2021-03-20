using App.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App.ViewsModels
{
    public class MainViewModel : BaseViewModel
    {
        public INavigationService NavigationService { get; set; }

        public MainViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.NavigationService = navigationService;
            SecondPageNavCommand = new Command(async () => await OnNavSecondPage());
            RegisterPageNavCommand = new Command(async () => await OnNavRegisterPage());
            FindPlantPageNavCommand = new Command(async () => await OnNavFindPlantPage());
            NotificationPageNavCommand = new Command(async () => await NotificationNavPage());
        }
        // Instance method to initialise the TwitterSignInPageViewModel
        public override async Task Init()
        {
            await Task.Factory.StartNew(() =>
            {
            });
        }
        //Show data page
        private async Task OnNavSecondPage()
        {
            //await Navigation.PushAsync(new ShowData());
        }
        public Command SecondPageNavCommand { get; }

        // Register Page
        private async Task OnNavRegisterPage()
        {
            await NavigationService.NavigateTo<RegisterViewModel>();
        }
        public Command RegisterPageNavCommand { get; }

        // Find Plant Page
        private async Task OnNavFindPlantPage()
        {
            //await Navigation.PushAsync(new FindPlantPage());
        }
        public Command FindPlantPageNavCommand { get; }

       
        // Notifications Page
        private async Task NotificationNavPage()
        {
            //await Navigation.PushAsync(new Notifications());
        }
        public Command NotificationPageNavCommand { get; }
    }
}
