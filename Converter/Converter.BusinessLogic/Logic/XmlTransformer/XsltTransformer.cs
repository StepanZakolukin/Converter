using System.IO;
using System.Windows;
using System.Xml;
using System.Xml.Xsl;

namespace Converter.BusinessLogic.Logic.XmlTransformer;

public class XsltTransformer : IXmlTransformer
{
    public void Transform(string xmlInput, string xsltResourceUri, string xmlOutput)
    {
        var uri = new Uri(xsltResourceUri);
        var resourceStream = Application.GetResourceStream(uri);

        if (resourceStream == null)
            throw new ArgumentException("Ресурс не найден", nameof(xsltResourceUri));

        using var reader = XmlReader.Create(resourceStream.Stream);
        var xslt = new XslCompiledTransform();
        xslt.Load(reader);

        using var writer = XmlWriter.Create(xmlOutput, xslt.OutputSettings);
        using var xmlReader = XmlReader.Create(xmlInput);
        xslt.Transform(xmlReader, writer);
    }
}