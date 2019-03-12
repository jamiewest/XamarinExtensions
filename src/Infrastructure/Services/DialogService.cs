using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace West.Extensions.Xamarin
{
    public class DialogService : IDialogService
    {
        public async Task ShowAlertAsync(string message, string title, string acceptLabel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, acceptLabel).ConfigureAwait(false);
        }

        public async Task<bool> ShowConfirmAsync(string message, string title, string acceptLabel, string cancelLabel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, acceptLabel, cancelLabel).ConfigureAwait(false);
        }

        public async Task<string> SelectActionAsync(string message, string title, IEnumerable<string> buttons)
        {
            return await Application.Current.MainPage.DisplayActionSheet(title, null, null, buttons.ToArray());
        }

        public async Task<string> SelectActionAsync(string message, string title, string cancelLabel, IEnumerable<string> buttons)
        {
            return await Application.Current.MainPage.DisplayActionSheet(title, cancelLabel, null, buttons.ToArray());
        }

        public async Task<string> SelectActionAsync(string message, string title, string cancelLabel, string destructiveLabel, IEnumerable<string> buttons)
        {
            return await Application.Current.MainPage.DisplayActionSheet(title, cancelLabel, destructiveLabel, buttons.ToArray());
        }
    }
}