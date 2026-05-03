using System.Xml.Linq;
using Converter.BusinessLogic.Constants;

namespace Converter.BusinessLogic.DataEnricher;

public class DataEnricher : IDataEnricher
{
    public void EnrichReport(string path)
    {
        var document = XDocument.Load(path);
        
        foreach (var employee in document.Descendants(OutputXmlElement.Employee))
        {
            var total = employee
                .Elements(OutputXmlElement.Salary)
                .Sum(element => XmlNumberParser.Parse(element.Attribute(OutputXmlElement.Amount)?.Value));
            employee.SetAttributeValue(OutputXmlElement.TotalSalary, XmlNumberParser.ToXmlString(total));
        }
        
        document.Save(path);
    }

    public void EnrichSource(string path)
    {
        var document = XDocument.Load(path);
        
        var total = document.Root?
            .Elements(SourceXmlElements.Item)
            .Sum(element => XmlNumberParser.Parse(element.Attribute(OutputXmlElement.Amount)?.Value)) ?? 0;
        document.Root?.SetAttributeValue(SourceXmlElements.TotalAll, XmlNumberParser.ToXmlString(total));
        document.Save(path);
    }
}