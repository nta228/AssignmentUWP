using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using T2009M1HelloUWP.Entities;
using T2009M1HelloUWP.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T2009M1HelloUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private AccountService accountService = new AccountService();
        public LoginPage()
        {
            this.InitializeComponent();
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(460, 400));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var loginInformation = new LoginInformation()
            {
                email = txtEmail.Text,
                password = txtPassword.Password.ToString()
            };
            var credential = await accountService.LoginAsync(loginInformation);
            if (credential == null)
            {
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Action fails";
                contentDialog.Content = $"Please try again later!";
                contentDialog.PrimaryButtonText = "Got it";
                await contentDialog.ShowAsync();
            }
            else {
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Action success";
                contentDialog.Content = $"Welcome back";
                contentDialog.PrimaryButtonText = "Got it";
                App.CurrentAccount = await accountService.GetInformationAsync();
                await contentDialog.ShowAsync();
                this.Frame.Navigate(typeof(Pages.Demo.NavigationViewDemo));
            }            
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.RegisterPage));
        }

    }
}
