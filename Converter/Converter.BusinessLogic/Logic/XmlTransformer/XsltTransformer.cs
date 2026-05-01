using System.Xml.Xsl;

namespace Converter.Application.Logic.XmlTransformer;

public class XsltTransformer : IXmlTransformer
{
    public void Transform(string inputPath, string xsltPath, string outputPath)
    {
        var xslt = new XslCompiledTransform();
        xslt.Load(xsltPath);
        xslt.Transform(inputPath, outputPath);
    }
}