using App.ViewsModel;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new RegisterViewModel(Navigation);
        }
        //private async void TakePhoto(object sender, EventArgs e)
        //{
        //    await CrossMedia.Current.Initialize();
        //    if (CrossMedia.Current.IsTakePhotoSupported && !CrossMedia.Current.IsPickPhotoSupported)
        //    {
        //        await DisplayAlert("Message", "Photo capture and pick not supported", "Ok");
        //        return;
        //    }
        //    else
        //    {
        //        var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
        //        {
        //            SaveToAlbum = true,
        //            //Directory = "Images",
        //           // Name=DateTime.Now+".jpg"
        //        });
        //        if (file == null)
        //            return;
        //        await DisplayAlert("File path", file.Path, "Ok");
        //        MyImage.Source = ImageSource.FromStream(() =>
        //        {
        //            var stream = file.GetStream();
        //            file.Dispose();
        //            return stream;
        //        });
        //    }
        //}
    }
}
