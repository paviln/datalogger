using App.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.ViewsModel
{
   public class RegisterViewModel : BaseViewModel
    {
         public ICommand RegisterPageNavCommand { get; set; }

        public RegisterViewModel(INavigation navigation) : base(navigation)
        {

              RegisterPageNavCommand = new Command(async () => await OnNavRegisterPage());
        }

         private async Task OnNavRegisterPage()

        {
         await Navigation.PushAsync(new RegisterPage());

        }
    }
}
