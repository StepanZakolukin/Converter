using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using Converter.Application.Constants;
using Converter.Application.Extentions;

namespace Converter.Application.Logic.DataEnricher;

public class DataEnricher : IDataEnricher
{
    private const string Format = "F2";
    private CultureInfo culture = CultureInfo.InvariantCulture;
    
    public void EnrichEmployeesXml(string path)
    {
        var doc = XDocument.Load(path);
        foreach (var empElement in doc.Descendants(OutputXmlElement.Employee))
        {
            var total = empElement
                .Elements(OutputXmlElement.Salary)
                .Sum(element => DoubleExtensions.Parse(element.Attribute(OutputXmlElement.Amount)?.Value));
            empElement.SetAttributeValue(OutputXmlElement.TotalSalary, total.ToString(Format, culture));
        }
        doc.Save(path);
    }

    public void EnrichSourceXml(string path)
    {
        var doc = XDocument.Load(path);
        var total = doc.Root?
            .Elements(InputXmlElement.Item)
            .Sum(element => DoubleExtensions.Parse(element.Attribute(OutputXmlElement.Amount)?.Value)) ?? 0;
        doc.Root?.SetAttributeValue(InputXmlElement.TotalAll, total.ToString(Format, culture));
        doc.Save(path);
    }
}