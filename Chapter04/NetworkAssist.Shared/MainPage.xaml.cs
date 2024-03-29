﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace NetworkAssist
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        public void ShowArrivals(object sender, RoutedEventArgs args)
        {
            Arrivals.Visibility = Visibility.Visible;
            QuickReport.Visibility = Visibility.Collapsed;
        }
                public void ShowQuickReport(object sender, RoutedEventArgs args)
        {
            Arrivals.Visibility = Visibility.Collapsed;
            QuickReport.Visibility = Visibility.Visible;
        }
    }
}
