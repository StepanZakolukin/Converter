namespace Converter.BusinessLogic.Logic.XmlTransformer;

public interface IXmlTransformer
{
    void Transform(string xmlInput, string xsltResourceUri, string xmlOutput);
}