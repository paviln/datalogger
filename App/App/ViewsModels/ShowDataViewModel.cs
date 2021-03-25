using App.Interfaces;
using App.Models;
using App.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.ViewsModels
{
   public class ShowDataViewModel : BaseViewModel
    {
        public INavigationService NavigationService { get; set; }
        public ICommand ShowDataCommand { get; set; }
        public string LoggerId { get; set; }

        private Plant plant;

        public Plant Plant
        {
            get { return plant; }
            set { plant = value; OnPropertyChanged(); }
        }
        private string soilType;
        public string SoilType { get { return soilType; } set { soilType = value; OnPropertyChanged(); } }

        private ImageSource image;
        public ImageSource Image { get { return image; } set { image = value; OnPropertyChanged(); } }

        public ShowDataViewModel(INavigationService navigationService) : base(navigationService)
        {
            NavigationService = navigationService;
            ShowDataCommand = new Command(async () => await GetData());
        }

        private async Task GetData()
        {
            Plant = await LoggerService.GetPlant(LoggerId);
            SoilType = Plant.SoilType.ToString();
            if (Plant != null)
            {
                var plantImage = await LoggerService.GetImage(Plant.Id);
                Image = ImageSource.FromStream(() => new MemoryStream(plantImage.Data.data));
            }
        }
    }
}
