using App.Services;
using App.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.ViewsModels
{
   public class ShowDataViewModel : BaseViewModel
    {
        public INavigationService NavigationService { get; set; }
        public ICommand SecondPageNavCommand { get; set; }
        public ShowDataViewModel(INavigationService navigationService) : base(navigationService)
        {
            NavigationService = navigationService;
            SecondPageNavCommand = new Command(async () => await OnNavSecondPage());
        }
        public override async Task Init()
        {
            await Task.Factory.StartNew(() =>
            {
            });
        }
        private async Task OnNavSecondPage()
        {
            //await Navigation.PushAsync(new ShowData());
        }
    }
}
