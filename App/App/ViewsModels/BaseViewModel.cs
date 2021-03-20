using App.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
