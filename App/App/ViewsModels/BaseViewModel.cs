using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace App.ViewsModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
       public INavigation Navigation { get; set; }
        public BaseViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
