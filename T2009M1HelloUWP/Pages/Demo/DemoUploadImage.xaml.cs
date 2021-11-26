using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
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
    public sealed partial class DemoUploadImage : Page
    {
        public DemoUploadImage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker
            {
                // config picker.
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.Downloads
            };
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".mp3");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();

            if (file != null) {
                Debug.WriteLine(file.Path); // đường dẫn của file trên máy tính.

                // setup account, full package, tránh nhầm lẫn package.
                CloudinaryDotNet.Account account = new CloudinaryDotNet.Account
                {
                    Cloud = "xuanhung2401",
                    ApiKey = "882796476526336",
                    ApiSecret = "gOOT_72AMyn9TQz1Hd4MxyGRjxY"
                };
                // tạo đối tượng upload cloudinary.            
                CloudinaryDotNet.Cloudinary cloud = new CloudinaryDotNet.Cloudinary(account);
                cloud.Api.Secure = true;
                // tạo thông tin upload.
                Debug.WriteLine(file.Path);                               
                //CloudinaryDotNet.Actions.ImageUploadParams imageUploadParams
                //    = new CloudinaryDotNet.Actions.ImageUploadParams
                //    {
                //        File = new CloudinaryDotNet.FileDescription(file.Name, await file.OpenStreamForReadAsync())
                //    };
                CloudinaryDotNet.Actions.RawUploadParams imageUploadParams
                   = new CloudinaryDotNet.Actions.RawUploadParams
                   {
                       File = new CloudinaryDotNet.FileDescription(file.Name, await file.OpenStreamForReadAsync())
                   };
                // thực hiện upload, lấy thông tin về.

                CloudinaryDotNet.Actions.RawUploadResult result = await cloud.UploadAsync(imageUploadParams);
                Debug.WriteLine(result.SecureUrl);
            }
            
        }
    }
}
