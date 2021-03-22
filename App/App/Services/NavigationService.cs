using App.Interfaces;
using App.Services;
using App.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(NavigationService))]
namespace App.Services
{
    public class NavigationService : INavigationService
    {
        public INavigation Navigation { get; set; }
        readonly IDictionary<Type, Type> _viewMapping = new Dictionary<Type, Type>();

        // Register our ViewModel and View within our Dictionary
        public void RegisterViewMapping(Type viewModel, Type view)
        {
            _viewMapping.Add(viewModel, view);
        }

        // Removes the most recent Page from the navigation stack.
        public Task<Page> RemoveViewFromStack()
        {
            return Navigation.PopAsync();
        }

        // Returns to the Root Page after removing the current page from the navigation stack
        public Task BackToMainPage()
        {
            return Navigation.PopToRootAsync(true);
        }

        // Navigates navigates to a specific ViewModel 
        public async Task NavigateTo<TVM>()
            where TVM : BaseViewModel
        {
            await NavigateToView(typeof(TVM));
        }

        // Navigates to a specific ViewModel within our dictionary viewMapping
        public async Task NavigateToView(Type viewModelType)
        {
            Type viewType;

            if (!_viewMapping.TryGetValue(viewModelType, out viewType))
                throw new ArgumentException("No view found in View Mapping for " + viewModelType.FullName + ".");

            var constructor = viewType.GetTypeInfo()
                .DeclaredConstructors.FirstOrDefault(dc => !dc.GetParameters().Any());

            var view = constructor.Invoke(null) as Page;
            await Navigation.PushAsync(view, true);
        }
    }
}