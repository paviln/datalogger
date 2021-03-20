using App.Services;
using App.ViewsModels;
using System;
using Xamarin.Forms;

namespace App
{
    public partial class MainPage : ContentPage
    {
        // Return the Binding Context for the ViewModel
        MainViewModel _viewModel => BindingContext as MainViewModel;

        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainViewModel(DependencyService.Get<INavigationService>());
        }
        // Instance method to call the WalkEntryPage to add a Walk Entry
        public async void Click(object sender, EventArgs e)
        {
            await _viewModel.Navigation.NavigateTo<RegisterViewModel>();
        }
        // Method to initialise our View Model when the ContentPage appears
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel != null)
            {
                // Call the Init method to initialise the ViewModel
                await _viewModel.Init();
            }
        }
    }
}
