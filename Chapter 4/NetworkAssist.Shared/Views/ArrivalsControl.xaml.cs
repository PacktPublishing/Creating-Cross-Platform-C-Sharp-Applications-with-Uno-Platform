using NetworkAssist.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnoBookRail.Common.Network;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace NetworkAssist.Views
{
public sealed partial class ArrivalsControl : UserControl
{
    public ArrivalsViewModel VM { get; set; }

    public ArrivalsControl()
    {
        InitializeComponent();
        VM = new ArrivalsViewModel();
    }

    // Doing this in Code-behind because of https://github.com/unoplatform/uno/issues/5792
    private async void OnStationListSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if ((sender as ComboBox).SelectedItem is Station selectedStn)
        {
            await VM.LoadArrivalsDataAsync(selectedStn.Id);
        }
    }
}
}
