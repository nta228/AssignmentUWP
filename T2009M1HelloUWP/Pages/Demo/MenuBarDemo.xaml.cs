using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T2009M1HelloUWP.Pages.Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MenuBarDemo : Page
    {
        public MenuBarDemo()
        {
            this.InitializeComponent();
        }

        private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            var menuFlyoutItem = sender as MenuFlyoutItem;
            switch (menuFlyoutItem.Tag) {
                case "new":
                   
                    break;
                case "open":
                    await HandleOpenFileAsync();
                    break;
                case "save":
                    await HandleSaveFileAsync();
                    break;
                case "exit":
                    
                    break;
            }
        }

        private async Task HandleOpenFileAsync()
        {
            var openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            openPicker.FileTypeFilter.Add(".txt");
            openPicker.FileTypeFilter.Add(".cs");
            // xử lý chọn file.
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                var fileContent = await FileIO.ReadTextAsync(file);
                MyEditor.Text = fileContent;
            }
            else {
                // tạo thông báo cho người dùng.
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Action fails!";
                contentDialog.Content = "Please choose a file to open!";
                contentDialog.PrimaryButtonText = "Got it!";
                await contentDialog.ShowAsync();
            }
        }

        private async Task HandleSaveFileAsync()
        {
            var savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("Text Document (*.txt)", new List<string>() { ".txt" });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "*.txt";
            
            // gọi ra thư mục cần làm việc.
            //StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            //// lấy ra file cần làm việc từ trong thư mục đó.
            //StorageFile storageFile = await storageFolder.CreateFileAsync("tenfile.txt", CreationCollisionOption.ReplaceExisting);
            
            StorageFile storageFile = await savePicker.PickSaveFileAsync();
            if (storageFile != null) {
                // ghi dữ liệu ra file
                await FileIO.WriteTextAsync(storageFile, MyEditor.Text);
                Debug.WriteLine("Okie");
                // tạo thông báo cho người dùng.
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Action success!";
                contentDialog.Content = "Save file to local storage.";
                contentDialog.PrimaryButtonText = "Got it!";
                await contentDialog.ShowAsync();
            }            
        }
    }
}
