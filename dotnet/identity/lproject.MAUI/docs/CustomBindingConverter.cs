using System.Globalization;

namespace lproject.MAUI.docs;

public class CustomBindingConverter : IValueConverter
{
    // parameter: ConveterParameter
    // Type: Convert: Target type, ConvertBack: Source type
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }
}