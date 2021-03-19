using App.Views;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App.ViewsModels
{
   public class RegisterViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        public Command SavePlantCommand { get; set; }

        private ImageSource image;
        public ImageSource Image 
        { 
            get { return image; }
            set { image = value; OnPropertyChanged(); }
        }
        public string PhotoPath { get; set; }
        public ICommand RegisterPageNavCommand { get; set; }
        public ICommand TakePhotoCommand { get; set; }

        public RegisterViewModel(INavigation navigation) : base(navigation)
        {
            this.Navigation = navigation;
            RegisterPageNavCommand = new Command(async () => await OnNavRegisterPage());
            TakePhotoCommand = new Command(async () => await TakePhotoAsync());
            SavePlantCommand = new Command(async () => await SavePlant());
        }
         private async Task OnNavRegisterPage()
        {
         await Navigation.PushAsync(new RegisterPage());

        }
        async Task TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                var stream = await photo.OpenReadAsync();
                Image = ImageSource.FromStream(() => stream);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }
        async Task SavePlant()
        {

        }
    }
}
