using App.Interfaces;
using App.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App.ViewsModels
{
    public class DataloggerViewModel : BaseViewModel
    {
        public INavigationService NavigationService { get; set; }
        public Command PairDataloggerCommand { get; set; }

        public DataloggerViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.NavigationService = navigationService;
            PairDataloggerCommand = new Command(async () => await PairDatalogger());
        }
        private async Task PairDatalogger()
        {
            System.Console.WriteLine("lol");
            await LoggerService.PairLogger("60576fc9744f7644182093bc");
        }
    }
}
