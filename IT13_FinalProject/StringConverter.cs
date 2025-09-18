using System.Globalization;

namespace IT13_FinalProject
{
    public class StringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            bool isVisible = value is bool b && b;
            return isVisible ? parameter?.ToString() ?? string.Empty : string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}