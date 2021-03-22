using App.Views;
using App.ViewsModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.ViewsModel
{
   public class NotificationViewModel : BaseViewModel
    {
        public ICommand NotificationPageNavCommand { get; set; }
        public NotificationViewModel(INavigation navition) : base(navition)
        {
            NotificationPageNavCommand = new Command(async () => await NotificationNavPage());
        }
        private async Task NotificationNavPage()

        {
            await Navigation.PushAsync(new Notifications());

        }
    }
}
