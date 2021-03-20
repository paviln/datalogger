using App.Interfaces;
using App.Services;
using App.ViewsModels;
using System;
using Xamarin.Forms;

namespace App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainViewModel(DependencyService.Get<INavigationService>());
        }
    }
}
