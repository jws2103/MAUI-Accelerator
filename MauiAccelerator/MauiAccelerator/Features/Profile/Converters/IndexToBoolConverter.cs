using System.Globalization;

namespace MauiAccelerator.Features.Profile.Converters;

public class IndexToBoolConverter : IValueConverter
{
    public int ExpectedIndex { get; set; }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int index)
        {
            return ExpectedIndex == index;
        }

        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}