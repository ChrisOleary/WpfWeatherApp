using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfWeatherApp.ViewModels.ValueConverter
{
    class BoolToRainConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isRaining = (bool)value;
            if (isRaining)
                return "Currently Raining";
            return "Not Raining";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string isRaining = (string)value;
            if (isRaining == "Currently Raining")
                return true;
            return false;
        }
    }
}
