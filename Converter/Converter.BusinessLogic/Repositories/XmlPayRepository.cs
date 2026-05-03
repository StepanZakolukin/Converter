using System.Xml.Linq;
using Converter.BusinessLogic.Constants;
using Converter.BusinessLogic.Models;

namespace Converter.BusinessLogic.Repositories;

public class XmlPayRepository(string filePath)
{
    public IEnumerable<Employee> GetRawData()
    {
        return XDocument
            .Load(filePath)
            .Descendants(SourceXmlElements.Item)
            .GroupBy(ParseFullName)
            .Select(groupElement => new Employee 
            {
                Name = groupElement.Key,
                SalaryInfo = new SalaryInfo
                {
                    MonthlySalaries = groupElement
                        .Select(ParseSalary)
                        .ToList()
                }
            });
    }

    private FullName ParseFullName(XElement element) => new()
    {
        FirstName = element.Attribute(SourceXmlElements.Name)?.Value,
        LastName = element.Attribute(SourceXmlElements.Surname)?.Value
    };

    private Salary ParseSalary(XElement element) => new()
    {
        Month = element.Attribute(SourceXmlElements.Month)?.Value,
        Amount = XmlNumberParser.Parse(element.Attribute(SourceXmlElements.Amount)?.Value) 
    };

    public void AddPayment(FullName name, Salary salary)
    {
        var document = XDocument.Load(filePath);
        
        document.Root?.Add(
            new XElement(
                SourceXmlElements.Item, 
                new XAttribute(SourceXmlElements.Name, name.FirstName), 
                new XAttribute(SourceXmlElements.Surname, name.LastName), 
                new XAttribute(SourceXmlElements.Amount, salary.Amount), 
                new XAttribute(SourceXmlElements.Month, salary.Month)));
        
        document.Save(filePath);
    }
}