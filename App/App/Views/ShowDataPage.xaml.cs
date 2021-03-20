using App.Interfaces;
using App.ViewsModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowDataPage : ContentPage
    {
        public ShowDataPage()
        {
            InitializeComponent();
            this.BindingContext = new ShowDataViewModel(DependencyService.Get<INavigationService>());
        }
    }
}