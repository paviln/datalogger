using App.Interfaces;
using App.Models;
using App.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using App.Helpers;

namespace App.ViewsModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public INavigationService NavigationService { get; set; }
        private string loggerId;
        public string LoggerId { get { return loggerId; } set { loggerId = value; OnPropertyChanged(); } }
        private string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged(); } }
        private byte[] file = null;
        private ImageSource image;
        public ImageSource Image { get { return image; } set { image = value; OnPropertyChanged(); } }
        public ICommand RegisterPageNavCommand { get; set; }
        public ICommand TakePhotoCommand { get; set; }
        public Command SavePlantCommand { get; set; }
        public RegisterViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.NavigationService = navigationService;
            RegisterPageNavCommand = new Command(async () => await OnNavRegisterPage());
            TakePhotoCommand = new Command(async () => await TakePhotoAsync());
            SavePlantCommand = new Command(async () => await SavePlant());
        }
        private async Task OnNavRegisterPage()
        {
            await NavigationService.NavigateTo<RegisterViewModel>();
        }
        async Task TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                var stream = await photo.OpenReadAsync();
                file = Conversion.StreamToByteArray(stream);
                stream.Position = 0;
                Image = ImageSource.FromStream(() => stream);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }
        async Task SavePlant()
        {
            if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(LoggerId) && file != null)
            {
                var plant = new Plant()
                {
                    Name = name,
                    LoggerId = loggerId,
                    File = file
                };
                var success = await LoggerService.SavePlant(plant);

                if (success)
                {
                    name = "";
                    loggerId = "";
                    file = null;
                }
            }
        }
    }
}
