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
            NavigationService.RegisterViewMapping(typeof(DataloggerViewModel), typeof(DataloggerPage));
            NavigationService.RegisterViewMapping(typeof(RegisterViewModel), typeof(RegisterPage));
            NavigationService.RegisterViewMapping(typeof(NotificationViewModel), typeof(NotificationPage));
            NavigationService.RegisterViewMapping(typeof(ShowDataViewModel), typeof(ShowDataPage));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
