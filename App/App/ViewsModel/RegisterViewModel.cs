using App.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.ViewsModel
{
   public class RegisterViewModel : BaseViewModel
    {
       public ICommand SecondPageNavCommand { get; set; }
        //public ObservableCollection<string> ShowData { get; set; }
        public RegisterViewModel(INavigation navigation) : base(navigation)
        {
           // SecondPageNavCommand = new Command(SecondPageNavCommand);
            //ShowData = new ObservableCollection<string>();
            SecondPageNavCommand = new Command(async () => await OnNavSecondPage());
        }

        private async Task OnNavSecondPage()

        {
            await Navigation.PushAsync(new ShowData());
            
        }
    }
}
