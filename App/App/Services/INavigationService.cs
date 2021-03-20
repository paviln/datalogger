﻿using App.ViewsModels;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App.Services
{
    public interface INavigationService
    {
        Task<Page> RemoveViewFromStack();
        Task BackToMainPage();
        Task NavigateTo<TVM>() where TVM : BaseViewModel;
    }
}
