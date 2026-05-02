using Converter.BusinessLogic.Models;

namespace Converter.BusinessLogic.Logic.AppOrchestrator;

public interface IAppOrchestrator
{
    IEnumerable<Employee> RunFullCycle(string source, string result);
    IEnumerable<Employee> AddAndRefresh(string source, string result, FullName name, Salary salary);
}