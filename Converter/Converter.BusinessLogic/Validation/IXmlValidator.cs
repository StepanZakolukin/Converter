namespace Converter.BusinessLogic.Validation;

public interface IXmlValidator
{
    bool Validate(string xmlPath, string xsdResourceUri, out string message);
    bool Validate(string xmlPath, out string message);
}