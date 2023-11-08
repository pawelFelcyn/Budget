using System.Globalization;

namespace Budget.Converters;

internal sealed class HexStringToColorConverter : IValueConverter, IMarkupExtension
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string str)
        {
            return Color.FromArgb(str);
        }
        return Binding.DoNothing;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Color color)
        {
            return color.ToHex();
        }

        return Binding.DoNothing;
    }

    public object ProvideValue(IServiceProvider serviceProvider) => this;
}
