using System;
using System.Threading.Tasks;

namespace West.Extensions.Xamarin
{
    public  interface INavigationService
    {
        Task NavigateToAsync(Type destination);
        Task NavigateToAsync(Type destination, object parameter);
        Task NavigateToAsync<T>() where T : class;
        Task NavigateToAsync<T>(object parameter) where T : class;
        Task NavigateBackAsync();
    }
}