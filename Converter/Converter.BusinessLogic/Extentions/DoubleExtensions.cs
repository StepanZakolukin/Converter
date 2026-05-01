using System.Globalization;

namespace Converter.Application.Extentions;

public static class DoubleExtensions
{
    public static double Parse(string number) => 
        double.TryParse(number.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out var result)
            ? result 
            : 0;
}