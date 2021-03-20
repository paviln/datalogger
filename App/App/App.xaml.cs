using App.Services;
using App.Interfaces;
using App.Views;
using App.ViewsModels;
using Xamarin.Forms;

namespace App
{
    public partial class App : Application
    {
        public static NavigationService NavigationService { get; set; }
        public App()
        {
            InitializeComponent();
            NavigationService = DependencyService.Get<INavigationService>() as NavigationService;
        }

        protected override void OnStart()
        {
            MainPage = new NavigationPage(new MainPage());
            NavigationService.Navigation = MainPage.Navigation;
            NavigationService.RegisterViewMapping(typeof(RegisterViewModel), typeof(RegisterPage));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
