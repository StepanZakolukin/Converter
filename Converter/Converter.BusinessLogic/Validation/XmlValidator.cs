using System.Windows;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Converter.BusinessLogic.Constants;

namespace Converter.BusinessLogic.Validation;

internal class XmlValidator : IXmlValidator
{
    public bool Validate(string xmlPath, string xsdResourceUri, out string message)
    {
        var uri = new Uri(xsdResourceUri, UriKind.Absolute);
        var resourceStreamInfo = Application.GetResourceStream(uri);

        if (resourceStreamInfo == null)
            throw new ArgumentException("Схема валидации не найдена", nameof(xsdResourceUri));
        
        try
        {
            var schemas = new XmlSchemaSet();
            using var schemaStream = resourceStreamInfo.Stream;
            using var reader = XmlReader.Create(schemaStream);
            schemas.Add(string.Empty, reader);

            var document = XDocument.Load(xmlPath);
            var errorMessage = string.Empty;

            document.Validate(schemas, (_, validationEvent) =>
            {
                errorMessage = validationEvent.Message;
            });

            message = errorMessage;
            return errorMessage == string.Empty;
        }
        catch (Exception ex)
        {
            message = ex.Message;
            return false;
        }
    }

    public bool Validate(string xmlPath, out string message) => 
        Validate(xmlPath, Resources.ValidationSchemeUri, out message);
}