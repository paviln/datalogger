using App.Interfaces;
using App.Services;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App.ViewsModels
{
    public class DataloggerViewModel : BaseViewModel
    {
        public INavigationService NavigationService { get; set; }
        private string loggerId;
        public string LoggerId 
        { 
            get { return loggerId; }
            set { loggerId = value; OnPropertyChanged(); }
        }
        public Command PairDataloggerCommand { get; set; }

        public DataloggerViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.NavigationService = navigationService;
            PairDataloggerCommand = new Command(async () => await PairDatalogger());
        }
        private async Task PairDatalogger()
        {
            if (!string.IsNullOrWhiteSpace(loggerId))
            {
                bool doesExist = await LoggerService.DoesLoggerExist(loggerId);

                if (doesExist)
                {
                    System.Console.WriteLine("Success");
                }
            } 
        }
    }
}
