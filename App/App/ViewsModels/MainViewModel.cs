using App.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.ViewsModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(INavigation navigation) : base(navigation)
        {
            
            SecondPageNavCommand = new Command(async () => await OnNavSecondPage());
            RegisterPageNavCommand = new Command(async () => await OnNavRegisterPage());
            FindPlantPageNavCommand = new Command(async () => await OnNavFindPlantPage());
            NotificationPageNavCommand = new Command(async () => await NotificationNavPage());
        }
        //Show data page
        private async Task OnNavSecondPage()

        {
            await Navigation.PushAsync(new ShowData());

        }
        public Command SecondPageNavCommand { get; }

        // Register Page
        private async Task OnNavRegisterPage()

        {
            await Navigation.PushAsync(new RegisterPage());

        }
        public Command RegisterPageNavCommand { get; }

        // Find Plant Page
        private async Task OnNavFindPlantPage()

        {
            await Navigation.PushAsync(new FindPlantPage());

        }
        public Command FindPlantPageNavCommand { get; }

       
        // Notifications Page
        private async Task NotificationNavPage()

        {
            await Navigation.PushAsync(new Notifications());

        }
        public Command NotificationPageNavCommand { get; }
    }
}
