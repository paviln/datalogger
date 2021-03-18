using App.Views;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
        public ImageSource Image { get; set; }

        public ICommand RegisterPageNavCommand { get; set; }
        public ICommand TakePhotoCommand { get; set; }
       

        public RegisterViewModel(INavigation navigation) : base(navigation)
        {

              RegisterPageNavCommand = new Command(async () => await OnNavRegisterPage());
              TakePhotoCommand = new Command(async () => await TakePhoto());
        }

         private async Task OnNavRegisterPage()

        {
         await Navigation.PushAsync(new RegisterPage());

        }


       // Take Photo
        private async Task TakePhoto()
        {
           
           
        }
    }
}
