using Converter.BusinessLogic.Constants;
using Converter.BusinessLogic.Logic.DataEnricher;
using Converter.BusinessLogic.Logic.EmployeeRepository;
using Converter.BusinessLogic.Logic.XmlTransformer;
using Converter.BusinessLogic.Models;

namespace Converter.BusinessLogic.Logic.AppOrchestrator;

public class AppOrchestrator(
    IXmlTransformer xmlTransformer,
    IDataEnricher dataEnricher,
    IEmployeeRepository employeeRepository) : IAppOrchestrator
{
    public IEnumerable<Employee> RunFullCycle(string source, string result)
    {
        xmlTransformer.Transform(source, Resources.TransformFileUri, result);
        
        dataEnricher.EnrichEmployeesXml(result);
        dataEnricher.EnrichSourceXml(source);
        
        return employeeRepository.GetAll(result);
    }

    public IEnumerable<Employee> AddAndRefresh(string source, string result, FullName name, Salary salary)
    {
        employeeRepository.AddRecord(source, name, salary);
        
        return RunFullCycle(source, result);
    }
}