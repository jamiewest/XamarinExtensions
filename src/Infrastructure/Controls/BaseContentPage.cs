using System.ComponentModel;
using Xamarin.Forms;

namespace West.Extensions.Xamarin
{
    [DesignTimeVisible(true)]
    public abstract class BaseContentPage<T> : ContentPage
        where T : BaseViewModel
    {
        protected virtual T ViewModel => BindingContext as T;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ViewModel.OnDisappearing();
        }
    }
}
