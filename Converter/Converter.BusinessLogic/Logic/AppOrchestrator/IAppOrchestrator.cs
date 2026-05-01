using Converter.Application.Models;

namespace Converter.Application.Logic.AppOrchestrator;

public interface IAppOrchestrator
{
    IEnumerable<Employee> RunFullCycle(string source, string xslt, string result);
    IEnumerable<Employee> AddAndRefresh(string source, string xslt, string result, FullName name, SalaryRecord salary);
}