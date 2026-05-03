namespace Converter.BusinessLogic.XmlTransformer;

public interface IXmlTransformer
{
    void Transform(string inputPath, string xsltResourceUri, string outputPath);
}