using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace West.Extensions.Xamarin
{
    public abstract class BaseViewModel : INotifyPropertyChanged, IViewModel
    {
        private bool _isBusy;

        protected BaseViewModel() { }

        /// <summary>
        /// Event to raise when a property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual Task InitializeAsync(object stateParameter) => Task.CompletedTask;

        public virtual void OnAppearing() { }

        public virtual void OnDisappearing() { }

        public virtual void OnResuming() { }

        public virtual void OnSleeping() { }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetAndRaisePropertyChanged(ref _isBusy, value);
        }

        protected void SetAndRaisePropertyChanged<TRef>(
            ref TRef field, TRef value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetAndRaisePropertyChangedIfDifferentValues<TRef>(
            ref TRef field, TRef value, [CallerMemberName] string propertyName = null)
            where TRef : class
        {
            if (field == null || !field.Equals(value))
            {
                SetAndRaisePropertyChanged(ref field, value, propertyName);
            }
        }
    }
}
