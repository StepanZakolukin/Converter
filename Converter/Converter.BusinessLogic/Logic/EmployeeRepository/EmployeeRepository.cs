using System.Xml.Linq;
using Converter.BusinessLogic.Constants;
using Converter.BusinessLogic.Extensions;
using Converter.BusinessLogic.Models;

namespace Converter.BusinessLogic.Logic.EmployeeRepository;

public class EmployeeRepository : IEmployeeRepository
{
    public IEnumerable<Employee> GetAll(string path)
    {
        var doc = XDocument.Load(path);
        return doc.Descendants(OutputXmlElement.Employee).Select(ParseEmployee);
    }
    
    public IEnumerable<Employee> GetAllRow(string path)
    {
        var doc = XDocument.Load(path);
        return doc.Descendants(InputXmlElement.Item)
            .GroupBy(element => new FullName { 
                FirstName = element.Attribute(InputXmlElement.Name)?.Value, 
                LastName = element.Attribute(InputXmlElement.Surname)?.Value 
            })
            .Select(groupElement => new Employee 
            {
                Name = groupElement.Key,
                SalaryInfo = new SalaryInfo
                {
                    MonthlySalaries = groupElement
                        .Select(element => new Salary 
                        {
                            Month = element.Attribute(InputXmlElement.Month)?.Value,
                            Amount = DoubleExtensions.Parse(element.Attribute(InputXmlElement.Amount)?.Value) 
                        })
                        .ToList()
                }
            });
    }
    
    public void AddRecord(string path, FullName name, Salary salary)
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
                    .Select(s => new Salary 
                    {
                        Month = s.Attribute(OutputXmlElement.Month)?.Value,
                        Amount = DoubleExtensions.Parse(s.Attribute(OutputXmlElement.Amount)?.Value) 
                    })
                    .ToList()
            }
        };
    }
}