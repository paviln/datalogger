﻿using App.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

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
        public string PhotoPath { get; set; }
        public ICommand RegisterPageNavCommand { get; set; }
        public ICommand TakePhotoCommand { get; set; }

        public RegisterViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.NavigationService = navigationService;
            RegisterPageNavCommand = new Command(async () => await OnNavRegisterPage());
            TakePhotoCommand = new Command(async () => await TakePhotoAsync());
            SavePlantCommand = new Command(async () => await SavePlant());
        }
        // Instance method to initialise the TwitterSignInPageViewModel
        public override async Task Init()
        {
            await Task.Factory.StartNew(() =>
            {
            });
        }
        private async Task OnNavRegisterPage()
        {
         await NavigationService.NavigateTo<RegisterViewModel>();

        }
        async Task TakePhotoAsync()
        {
            var item = await RegisterService.GetPlant();
            Console.WriteLine(item);

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
