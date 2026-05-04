namespace Converter.BusinessLogic.Validation;

public interface IXmlIntegrityChecker
{
    bool Check(string filePath, out string message);
}