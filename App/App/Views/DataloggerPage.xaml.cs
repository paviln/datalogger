using App.Interfaces;
using App.ViewsModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataloggerPage : ContentPage
    {
        public DataloggerPage()
        {
            InitializeComponent();
            this.BindingContext = new DataloggerViewModel(DependencyService.Get<INavigationService>());
        }
    }
}