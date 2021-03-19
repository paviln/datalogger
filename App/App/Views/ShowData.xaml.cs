using App.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            BindingContext = new ShowDataViewModel(Navigation);
        }
    }
}