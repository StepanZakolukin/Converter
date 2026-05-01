using System.Xml.Linq;
using Converter.Application.Constants;
using Converter.Application.Extentions;
using Converter.Application.Models;

namespace Converter.Application.Logic.EmployeeRepository;

public class EmployeeRepository : IEmployeeRepository
{
    public IEnumerable<Employee> GetAll(string path)
    {
        var doc = XDocument.Load(path);
        return doc.Descendants(OutputXmlElement.Employee).Select(ParseEmployee);
    }
    
    public void AddRecord(string path, FullName name, SalaryRecord salary)
    {
        var doc = XDocument.Load(path);
        doc.Root?.Add(new XElement(InputXmlElement.Item,
            new XAttribute(OutputXmlElement.Name, name.FirstName),
            new XAttribute(OutputXmlElement.Surname, name.LastName),
            new XAttribute(OutputXmlElement.Amount, salary.Amount),
            new XAttribute(OutputXmlElement.Month, salary.Month)));
        doc.Save(path);
    }

    private Employee ParseEmployee(XElement element)
    {
        return new Employee
        {
            Name = new FullName
            {
                FirstName = element.Attribute(OutputXmlElement.Name)?.Value,
                LastName = element.Attribute(OutputXmlElement.Surname)?.Value
            },
            SalaryInfo = new SalaryInfo
            {
                MonthlySalaries = element.Elements(OutputXmlElement.Salary)
                    .Select(s => new SalaryRecord 
                    {
                        Month = s.Attribute(OutputXmlElement.Month)?.Value,
                        Amount = DoubleExtensions.Parse(s.Attribute(OutputXmlElement.Amount)?.Value) 
                    })
                    .ToList()
            }
        };
    }
}