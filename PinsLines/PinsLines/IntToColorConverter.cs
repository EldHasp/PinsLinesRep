using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PinsLines
{
    /// <summary>Конвертор возвращающий цвет по индексу</summary>
    [ValueConversion(typeof(int), typeof(SolidColorBrush))]
    public class IntToColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values?.Length != 2)
                return null;

            if (
                    (values[0] is int index || (values[0] is string str && int.TryParse(str, out index)))
                    && (values[1] is SolidColorBrush[] colors)
                    && index >= 0 && index < colors.Length
                )
                return colors[index];
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
