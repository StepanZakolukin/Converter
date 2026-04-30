namespace Converter.Application.Logic.XmlTransformer;

public interface IXmlTransformer
{
    void Transform(string inputXmlPath, string xsltPath, string outputXmlPath);
}