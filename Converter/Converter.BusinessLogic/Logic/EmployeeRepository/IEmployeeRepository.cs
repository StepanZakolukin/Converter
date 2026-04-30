using Converter.Application.Models;

namespace Converter.Application.Logic.EmployeeRepository;

public interface IEmployeeRepository
{
    IEnumerable<Employee> GetAll(string path);
    void AddRecord(string path, Employee newItem);
}