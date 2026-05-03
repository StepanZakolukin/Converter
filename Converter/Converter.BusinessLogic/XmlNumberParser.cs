using System.Globalization;

namespace Converter.BusinessLogic;

public static class XmlNumberParser
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;
    
    public static bool TryParse(string number, out double result)
    {
        result = double.NaN;
        return !string.IsNullOrWhiteSpace(number) &&
               double.TryParse(number.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out result);
    }
    
    public static double Parse(string number) => TryParse(number, out var result) 
        ? result 
        : throw new ArgumentException(number);
    
    public static string ToXmlString(double value) => value.ToString("F2", Culture);
}