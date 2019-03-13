namespace West.Extensions.Xamarin
{
    public abstract class BaseStateAwareViewModel<T> : BaseViewModel
        where T : struct
    {
        private T _currentState;

        public T CurrentState
        {
            get => _currentState;
            set => SetAndRaisePropertyChanged(ref _currentState, value);
        }
    }
}
