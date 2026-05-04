using System.Windows;
using System.Xml;
using System.Xml.Xsl;

namespace Converter.BusinessLogic.XmlTransformer;

public class XsltTransformer : IXmlTransformer
{
    public void Transform(string inputPath, string xsltResourceUri, string outputPath)
    {
        var uri = new Uri(xsltResourceUri);
        var resourceStream = Application.GetResourceStream(uri);

        if (resourceStream == null)
            throw new ArgumentException("Файл преобразования не найден", nameof(xsltResourceUri));

        using var reader = XmlReader.Create(resourceStream.Stream);
        var xslt = new XslCompiledTransform();
        xslt.Load(reader);

        using var writer = XmlWriter.Create(outputPath, xslt.OutputSettings);
        using var xmlReader = XmlReader.Create(inputPath);
        xslt.Transform(xmlReader, writer);
    }
}