using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using T2009M1HelloUWP.Entities;
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
    public sealed partial class ListViewDemo : Page
    {
        public ListViewDemo()
        {            
            this.InitializeComponent(); 
            this.Loaded += Page_Loaded; 
        }
        private void MyListView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Debug.WriteLine("Clicked item.");
            var currentIndex = MyListView.SelectedIndex;
            Debug.WriteLine($"Current index: {currentIndex}");
            Debug.WriteLine($"Selected item: {MyListView.SelectedItem}");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Load design success!");
            Debug.WriteLine("Loading data...");

            List<Account> accounts = new List<Account>();        
            accounts.Add(new Account
            {
                firstName = "Tuan",
                lastName = "Anh",
                address = "Hanoi",
                avatar = "https://cdn.icon-icons.com/icons2/2643/PNG/512/male_boy_person_people_avatar_icon_159358.png",
                birthday = "2001-08-08",
                email = "ta228@gmail.com",
                gender = 1,
                introduction = "Hanoi",
                phone = "091231234",
                password = "091231234"
            });
            accounts.Add(new Account
            {
                firstName = "anh",
                lastName = "tuan",
                address = "Hanoi",
                avatar = "https://cdn.iconscout.com/icon/free/png-256/avatar-373-456325.png",
                birthday = "2010-10-10",
                email = "tuananh@gmail.com",
                gender = 1,
                introduction = "Hanoi",
                phone = "091231233",
                password = "091231233"
            });

            MyListView.ItemsSource = accounts;
            Debug.WriteLine("Load data success");
        }
    }
}
