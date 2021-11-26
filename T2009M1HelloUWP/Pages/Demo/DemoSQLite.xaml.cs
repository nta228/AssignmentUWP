using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using T2009M1HelloUWP.Entities;
using T2009M1HelloUWP.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T2009M1HelloUWP.Pages.Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DemoSQLite : Page
    {
        private NoteModel noteModel = new NoteModel();
        public DemoSQLite()
        {
            this.InitializeComponent();
            this.Loaded += DemoSQLite_Loaded;
        }

        private async void DemoSQLite_Loaded(object sender, RoutedEventArgs e)
        {
            DatabaseMigration.UpdateDabase();        
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var note = new Note()
            {
                Title = txtTitle.Text,
                Detail = txtDetail.Text,
                Id = Guid.NewGuid().ToString("N"),
                CreatedAt = DateTime.Now
            };
            var result = noteModel.Save(note);
            ContentDialog contentDialog = new ContentDialog();
            contentDialog.PrimaryButtonText = "Okie";
            if (result)
            {
                contentDialog.Title = "Action success!";
                contentDialog.Content = "Note saved!";
                await contentDialog.ShowAsync();
                this.Frame.Navigate(typeof(Pages.Demo.NoteList));
            }
            else {
                contentDialog.Title = "Action fails!";
                contentDialog.Content = "Please try again!";
                await contentDialog.ShowAsync();
            }                       
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.Demo.NoteList));
        }
    }
}
