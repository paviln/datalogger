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
        public int Id { get; set; }
        public string Name { get; set; }
        public Command SavePlantCommand { get; set; }

        private ImageSource image;
        public ImageSource Image
        {
            get { return image; }
            set { image = value; OnPropertyChanged(); }
        }
        private byte[] _file;
        public ICommand RegisterPageNavCommand { get; set; }
        public ICommand TakePhotoCommand { get; set; }

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
                _file = Conversion.StreamToByteArray(stream);
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
            var plant = new Plant()
            {
                Name = "test2",
                LoggerId = "6056a026ba4eb84cc0cac957",
                File = _file
            };
            await LoggerService.SavePlant(plant);
        }
    }
}
