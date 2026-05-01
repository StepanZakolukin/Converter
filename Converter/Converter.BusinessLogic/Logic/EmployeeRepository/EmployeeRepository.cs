using System.Xml.Linq;
using Converter.Application.Models;

namespace Converter.Application.Logic.EmployeeRepository;

public class EmployeeRepository : IEmployeeRepository
{
    public IEnumerable<Employee> GetAll(string path)
    {
        var doc = XDocument.Load(path);
        return doc.Descendants("Employee").Select(ParseEmployee);
    }
    
    public void AddRecord(string path, FullName name, SalaryRecord salary)
    {
        var doc = XDocument.Load(path);
        doc.Root?.Add(new XElement("item",
            new XAttribute("name", name.FirstName),
            new XAttribute("surname", name.LastName),
            new XAttribute("amount", salary.Amount),
            new XAttribute("mount", salary.Month)));
        doc.Save(path);
    }

    private Employee ParseEmployee(XElement element)
    {
        return new Employee
        {
            Name = new FullName
            {
                FirstName = element.Attribute("name")?.Value,
                LastName = element.Attribute("surname")?.Value
            },
            SalaryInfo = new SalaryInfo
            {
                MonthlySalaries = element.Elements("salary").Select(s => new SalaryRecord
                {
                    Month = s.Attribute("mount")?.Value,
                    Amount = Parse(s.Attribute("amount")?.Value)
                }).ToList()
            }
        };
    }
    
    private double Parse(string number)
    {
        if (string.IsNullOrWhiteSpace(number)) return 0;
        
        var normalized = number.Replace(',', '.');
        
        if (double.TryParse(normalized, System.Globalization.NumberStyles.Any, 
                System.Globalization.CultureInfo.InvariantCulture, out var result))
        {
            return result;
        }
    
        return 0;
    }
}