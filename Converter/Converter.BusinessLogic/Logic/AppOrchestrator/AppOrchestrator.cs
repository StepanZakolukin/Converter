using Converter.Application.Logic.DataEnricher;
using Converter.Application.Logic.EmployeeRepository;
using Converter.Application.Logic.XmlTransformer;
using Converter.Application.Models;

namespace Converter.Application.Logic.AppOrchestrator;

public class AppOrchestrator(
    IXmlTransformer xmlTransformer,
    IDataEnricher dataEnricher,
    IEmployeeRepository employeeRepository) : IAppOrchestrator
{
    public IEnumerable<Employee> RunFullCycle(string source, string xslt, string result)
    {
        xmlTransformer.Transform(source, xslt, result);
        
        dataEnricher.EnrichEmployeesXml(result);
        dataEnricher.EnrichSourceXml(source);
        
        return employeeRepository.GetAll(result);
    }

    public IEnumerable<Employee> AddAndRefresh(string source, string xslt, string result, Employee newItem)
    {
        employeeRepository.AddRecord(source, newItem);
        
        return RunFullCycle(source, xslt, result);
    }
}