using Lesson35DemoMauiApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.JSInterop;

namespace Lesson35DemoMauiApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnTapMeClicked(object sender, EventArgs e)
        {
            //await DisplayAlertAsync("MAUI Button", "You tapped the native MAUI button!", "OK");

            //bool answer = await DisplayAlertAsync(
            //        "Confirm Action",
            //        "Do you want to continue?",
            //        "Yes",
            //        "No");

            //if (answer)
            //{
            //    await DisplayAlertAsync("Result", "You chose YES.", "OK");
            //}
            //else
            //{
            //    await DisplayAlertAsync("Result", "You chose NO.", "OK");
            //}

            /*
             * The method signature is:
             *      DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
             *  Meaning:
             *      title → title of the sheet
             *      cancel → label for the cancel button
             *      destruction → destructive action (shows visually distinct on mobile)
             *      buttons → list of regular options
             *  The return value is a string representing the button the user clicked.
             *    
             */

            //string result = await DisplayActionSheetAsync(
            //    "Choose an option",
            //    "Cancel",
            //    "Delete",
            //    "Option 1",
            //    "Option 2",
            //    "Option 3");

            //await DisplayActionSheetAsync("You selected", result ?? "No selection", "OK");

            //string choice = await DisplayActionSheetAsync(
            //    "Pick something",
            //    "Cancel",
            //    null,
            //    "Option A",
            //    "Option B",
            //    "Option C");

            //if (choice != "Cancel")
            //{
            //    await DisplayAlertAsync("You picked",choice, "OK");
            //}

            bool answer = await DisplayAlertAsync(
                "Confirm Action",
                "Do you want to continue?",
                "Yes",
                "No");

            // Send the result into the shared bridge
            ConfirmationBridge.Instance.PublishResult(answer);
        }

    }
}
