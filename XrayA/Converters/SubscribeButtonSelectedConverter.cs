using System;
using System.Globalization;
using Avalonia.Data.Converters;
using XrayA.Models.DataBase;

namespace XrayA.Converters;

public class SubscribeButtonSelectedConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Guid id && parameter is Subscribe subscribe)
        {
            return id == subscribe.Id;
        }

        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if(value is bool isSelected && parameter is Subscribe subscribe)
        {
            if (isSelected)
            {
                return subscribe.Id;
            }
        }

        return null;
    }
}