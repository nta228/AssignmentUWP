using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using T2009M1HelloUWP.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class NoteList : Page
    {
        private NoteModel noteModel = new NoteModel();
        public NoteList()
        {
            this.InitializeComponent();
            this.Loaded += NoteList_Loaded;
        }

        private void NoteList_Loaded(object sender, RoutedEventArgs e)
        {
            MyListView.ItemsSource = noteModel.FindAll();
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.Demo.DemoSQLite));
        }

        private void SearchHandle(object sender, RoutedEventArgs e)
        {
            var result = noteModel.SearchByKeyword(txtKeyword.Text);
            Debug.WriteLine(result.Count);
            MyListView.ItemsSource = result;
        }
    }
}
