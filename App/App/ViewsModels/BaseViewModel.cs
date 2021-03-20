using App.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace App.ViewsModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public INavigationService Navigation { get; set; }
        protected BaseViewModel(INavigationService navigationService)
        {
            Navigation = navigationService;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public abstract Task Init();
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
