using App.Services;
using App.ViewsModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowData : ContentPage
    {
        public ShowData()
        {
            InitializeComponent();
            this.BindingContext = new ShowDataViewModel(DependencyService.Get<INavigationService>());
        }
    }
}