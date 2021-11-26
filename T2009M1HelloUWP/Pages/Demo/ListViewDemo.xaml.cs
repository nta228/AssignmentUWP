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
            this.InitializeComponent(); // load giao diện.
            this.Loaded += Page_Loaded; // load data sau khi giao diện đã load thành công.
        }
     
        // Xử lý khi click vào một phần tử
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
                firstName = "Hung",
                lastName = "Dao",
                address = "Hanoi",
                avatar = "https://cdn.icon-icons.com/icons2/2643/PNG/512/male_boy_person_people_avatar_icon_159358.png",
                birthday = "2000-10-10",
                email = "hung@gmail.com",
                gender = 1,
                introduction = "Hanoi",
                phone = "091231233",
                password = "091231233"
            });
            accounts.Add(new Account
            {
                firstName = "Luyen",
                lastName = "Dao",
                address = "Hanoi",
                avatar = "https://cdn.iconscout.com/icon/free/png-256/avatar-373-456325.png",
                birthday = "2010-10-10",
                email = "luyendh@gmail.com",
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
