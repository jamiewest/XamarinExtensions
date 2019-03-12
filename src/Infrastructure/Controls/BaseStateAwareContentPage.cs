using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace West.Extensions.Xamarin
{
    public abstract class BaseStateAwareContentPage<TViewModel, TEnum> : BaseContentPage<TViewModel>
        where TViewModel : BaseStateAwareViewModel<TEnum>
        where TEnum : struct
    {
        private readonly List<VisualElement> _statefullVisualElements = new List<VisualElement>();

        protected override void OnAppearing()
        {
            _statefullVisualElements.AddRange(GetStateAwareVisualElements());
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
            _statefullVisualElements.Clear();

            base.OnDisappearing();
        }

        internal abstract IEnumerable<VisualElement> GetStateAwareVisualElements();

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.CurrentState))
            {
                UpdateVisualStateManager(ViewModel.CurrentState.ToString());
            }
        }

        private void UpdateVisualStateManager(string name)
        {
            foreach (var item in _statefullVisualElements)
            {
                VisualStateManager.GoToState(item, name);
            }
        }
    }
}
