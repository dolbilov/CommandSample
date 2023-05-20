using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace CommandSample.Converters;

public class ButtonTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var robotName = value as string;

        return string.IsNullOrEmpty(robotName)
            ? "No one robot here."
            : $"Open the Pod Bay for {robotName}.";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}