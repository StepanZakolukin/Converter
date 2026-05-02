using Converter.BusinessLogic.Models;

namespace Converter.BusinessLogic.Logic.EmployeeRepository;

public interface IEmployeeRepository
{
    IEnumerable<Employee> GetAll(string path);
    IEnumerable<Employee> GetAllRow(string path);
    void AddRecord(string path, FullName name, Salary salary);
}