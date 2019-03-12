using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace West.Extensions.Xamarin
{
    /// <summary>
    /// Interface to display UI "MessageBox" style prompts.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Presents an alert dialog to the application user with a single cancel button.
        /// </summary>
        /// <param name="message">The body text of the alert dialog.</param>
        /// <param name="title">The title of the alert dialog.</param>
        /// <param name="buttonLabel">Text to be displayed on the 'Cancel' button.</param>
        Task ShowAlertAsync(string message, string title, string cancelLabel);

        /// <summary>
        /// Presents an alert dialog to the application user with an accept and a cancel button.
        /// </summary>
        /// <param name="message">The body text of the alert dialog.</param>
        /// <param name="title">The title of the alert dialog.</param>
        /// <param name="okLabel">Text to be displayed on the 'Accept' button.</param>
        /// <param name="cancelLabel">Text to be displayed on the 'Cancel' button.</param>
        /// <returns>
        /// A task that contains the user's choice as a Boolean value. true indicates that the user 
        /// accepted the alert. false indicates that the user cancelled the alert. 
        /// </returns>
        Task<bool> ShowConfirmAsync(string message, string title, string acceptLabel, string cancelLabel);

        /// <summary>
        /// Displays a native platform action sheet, allowing the application user to choose from several buttons.
        /// </summary>
        /// <param name="title">Title of the displayed action sheet.</param>
        /// <param name="buttons">Text labels for additional buttons.</param>
        /// <returns>An awaitable Task that displays an action sheet and returns the Text of the button pressed by the user.</returns>
        Task<string> SelectActionAsync(string message, string title, IEnumerable<string> buttons);

        /// <summary>
        /// Displays a native platform action sheet, allowing the application user to choose from several buttons.
        /// </summary>
        /// <param name="title">Title of the displayed action sheet.</param>
        /// <param name="cancelLabel">Text to be displayed in the 'Cancel' button.</param>
        /// <param name="buttons">Text labels for additional buttons.</param>
        /// <returns>An awaitable Task that displays an action sheet and returns the Text of the button pressed by the user.</returns>
        Task<string> SelectActionAsync(string message, string title, string cancelLabel, IEnumerable<string> buttons);

        /// <summary>
        /// Displays a native platform action sheet, allowing the application user to choose from several buttons.
        /// </summary>
        /// <param name="title">Title of the displayed action sheet.</param>
        /// <param name="cancelLabel">Text to be displayed in the 'Cancel' button.</param>
        /// <param name="destructiveLabel">Text to be displayed in the 'Destruct' button. </param>
        /// <param name="buttons">Text labels for additional buttons.</param>
        /// <returns>An awaitable Task that displays an action sheet and returns the Text of the button pressed by the user.</returns>
        Task<string> SelectActionAsync(string message, string title, string cancelLabel, string destructiveLabel, IEnumerable<string> buttons);
    }
}