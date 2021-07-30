using System;
using Windows.UI.Xaml.Data;

namespace NetworkAssist.Converters
{
    public class CupertinoButtonColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value?.ToString() == parameter?.ToString())
            {
                return App.Current.Resources["CupertinoBlueBrush"];
            }
            else
            {
                return App.Current.Resources["CupertinoSecondaryGrayBrush"];
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => throw new NotImplementedException();
    }
}
