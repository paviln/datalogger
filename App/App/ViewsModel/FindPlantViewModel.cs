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
   public class FindPlantViewModel : BaseViewModel
    {
         public ICommand FindPlantPageNavCommand { get; set; }

        public FindPlantViewModel(INavigation navigation) : base(navigation)
        {

            FindPlantPageNavCommand = new Command(async () => await OnNavFindPlantPage());
        }

         private async Task OnNavFindPlantPage()

        {
         await Navigation.PushAsync(new FindPlantPage());

        }
    }
}
