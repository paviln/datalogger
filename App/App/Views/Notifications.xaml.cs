using App.Interfaces;
using App.ViewsModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Notifications : ContentPage
    {
        public Notifications()
        {
            InitializeComponent();
            this.BindingContext = new NotificationViewModel(DependencyService.Get<INavigationService>());
        }
    }
}