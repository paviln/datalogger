using App.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.ViewsModel
{
   public class ShowDataViewModel : BaseViewModel
    {
        public ICommand SecondPageNavCommand { get; set; }
        public ShowDataViewModel(INavigation navition): base(navition)
        {
            SecondPageNavCommand = new Command(async () => await OnNavSecondPage());
        }
        private async Task OnNavSecondPage()

        {
            await Navigation.PushAsync(new ShowData());

        }
    }
}
