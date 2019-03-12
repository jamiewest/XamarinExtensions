using System.Threading.Tasks;

namespace West.Extensions.Xamarin
{
    /// <summary>
    /// Interface for ViewModels
    /// </summary>
    public interface IViewModel
    {
        Task InitializeAsync(object stateParameter);
        
        /// <summary>
        /// Called prior to a Page becoming visible. 
        /// </summary>
        void OnAppearing();

        /// <summary>
        /// Called when a Page disappears.
        /// </summary>
        void OnDisappearing();

        /// <summary>
        /// Called when the application is resumed.
        /// </summary>
        void OnResuming();

        /// <summary>
        /// Called when the application goes to the background.
        /// </summary>
        void OnSleeping();
    }
}
