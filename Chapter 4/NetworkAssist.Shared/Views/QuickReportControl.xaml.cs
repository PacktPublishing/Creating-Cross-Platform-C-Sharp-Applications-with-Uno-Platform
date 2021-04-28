using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Windows.Media.Capture;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Devices.Geolocation;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using Windows.UI.Popups;

namespace NetworkAssist.Views
{
    public sealed partial class QuickReportControl : UserControl
    {
        public QuickReportControl()
        {
            this.InitializeComponent();
        }

Windows.Storage.StorageFile capturedPhoto;

private async void CaptureImageClicked(object sender, RoutedEventArgs e)
{
    try
    {
        var captureUI = new CameraCaptureUI();
        captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;

        capturedPhoto = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

        if (capturedPhoto == null)
        {
            return;
        }
        else
        {
            var source = new BitmapImage(new Uri(capturedPhoto.Path));
            ImageToInclude.Source = source;
            TakePictureButton.Visibility = Visibility.Collapsed;
        }
    }
    catch (Exception ex)
    {
        System.Diagnostics.Debug.WriteLine(ex);
    }
}

private async Task<string> GetLocationAsync()
{
#if __IOS__
    // Bug prevents running this on iOS https://github.com/unoplatform/uno/issues/4853
    return "Unavailable";
#endif

    try
    {
        var accessStatus = await Geolocator.RequestAccessAsync();

        switch (accessStatus)
        {
            case GeolocationAccessStatus.Allowed:
                var geolocator = new Geolocator();
                var pos = await geolocator.GetGeopositionAsync();
                return $"{pos.Coordinate.Latitude},{pos.Coordinate.Longitude},{pos.Coordinate.Altitude}";

            case GeolocationAccessStatus.Denied:
                return "Location access denied";

            case GeolocationAccessStatus.Unspecified:
                return "Location Error";
        }
    }
    catch (Exception ex)
    {
        System.Diagnostics.Debug.WriteLine(ex);
    }

    return string.Empty;
}


private async void SendClicked(object sender, RoutedEventArgs e)
{
    var url = $"{ViewModels.DataService.WebApiDomain}/QuickReports/Create";

    BusyOverlay.Visibility = Visibility.Visible;
    try
    {
        var http = new HttpClient();
        var formContent = new MultipartFormDataContent();

        if (capturedPhoto != null)
        {
            var fileContent = new StreamContent(await capturedPhoto?.OpenStreamForReadAsync());
            formContent.Add(fileContent, "imageFile", "capturedFile");
        }
        formContent.Add(new StringContent(await GetLocationAsync()), "location");
        formContent.Add(new StringContent(EnteredText.Text), "information");
        var response = await http.PostAsync(new Uri(url), formContent);
        var serverResponse = await response.Content.ReadAsStringAsync();

        if (serverResponse == "success")
        {
            EnteredText.Text = string.Empty;
            capturedPhoto = null;
            ImageToInclude.Source = null;
            TakePictureButton.Visibility = Visibility.Visible;

            var msgDlg = new MessageDialog("Quick report submitted", "Thank you");
            await msgDlg.ShowAsync();
        }
        else
        {
            throw new HttpRequestException("Unsuccessful upload");
        }
    }
    catch (Exception ex)
    {
        System.Diagnostics.Debug.WriteLine(ex);

        // Depending on the exception or the reason for the failure,
        // it may be appropriate to log this error or automatically retry the request.
        var msgDlg = new MessageDialog("Failed to upload quick report");
        await msgDlg.ShowAsync();
    }
    finally
    {
        BusyOverlay.Visibility = Visibility.Collapsed;
    }
}


    }
}
