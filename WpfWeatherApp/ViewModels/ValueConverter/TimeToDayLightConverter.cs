using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfWeatherApp.ViewModels.ValueConverter
{
    class TimeToDayLightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime LocalObservationDateTime = (DateTime)value;
            if (LocalObservationDateTime.TimeOfDay >= (new TimeSpan(6,0,0)) && LocalObservationDateTime.TimeOfDay  <= (new TimeSpan(11,59, 59)))
                return "Morning";
            if (LocalObservationDateTime.TimeOfDay >= (new TimeSpan(12,0,0)) && LocalObservationDateTime.TimeOfDay  <= (new TimeSpan(17, 59,59)))
                return "Afternoon";
            return "Evening";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
