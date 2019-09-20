using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Utf8SvgInClipboard
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void CopySvgToClipboard()
        {
            var svgFileUri = new Uri("ms-appx:///Assets/Test.svg");
            var svgFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(svgFileUri);
            var svgText = await Windows.Storage.FileIO.ReadTextAsync(svgFile);

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(svgText);

            var stream = new Windows.Storage.Streams.InMemoryRandomAccessStream();
            await stream.WriteAsync(bytes.AsBuffer());
            stream.Seek(0);

            var dataPackage = new Windows.ApplicationModel.DataTransfer.DataPackage();
            dataPackage.SetData("image/svg+xml", stream);

            Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(dataPackage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CopySvgToClipboard();
        }
    }
}
