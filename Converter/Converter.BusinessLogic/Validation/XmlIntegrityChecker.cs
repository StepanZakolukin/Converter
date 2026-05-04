using System.Xml;

namespace Converter.BusinessLogic.Validation;

public class XmlIntegrityChecker : IXmlIntegrityChecker
{
    public bool Check(string filePath, out string message)
    {
        try
        {
            var settings = new XmlReaderSettings 
            { 
                CheckCharacters = true, 
                ConformanceLevel = ConformanceLevel.Document 
            };

            using var reader = XmlReader.Create(filePath, settings);
            while (reader.Read()) { }

            message = string.Empty;
            return true;
        }
        catch (XmlException ex)
        {
            message = $"Файл поврежден (строка {ex.LineNumber}): {ex.Message}";
            return false;
        }
        catch (Exception ex)
        {
            message = $"Не удалось прочитать файл: {ex.Message}";
            return false;
        }
    }
}