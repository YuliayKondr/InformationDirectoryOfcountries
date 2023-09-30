using System;
using System.Globalization;
using System.Windows;
namespace InformationDirectoryOfСountries.Converters;

public class StringToVisibilityConverter : ValueConverterBase
{
    public bool Collapse { get; set; }

    public bool Inverse { get; set; }

    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        _ = bool.TryParse(parameter as string, out bool collapse);

        bool result = value is string str && !string.IsNullOrEmpty(str);

        return (result && !Inverse) || (!result && Inverse)
            ? Visibility.Visible
            : (collapse || Collapse ? Visibility.Collapsed : Visibility.Hidden);
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}