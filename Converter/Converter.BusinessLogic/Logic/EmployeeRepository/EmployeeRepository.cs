using Converter.Application.Models;

namespace Converter.Application.Logic.EmployeeRepository;

public class EmployeeRepository : IEmployeeRepository
{
    public IEnumerable<Employee> GetAll(string xmlPath)
    {
        throw new NotImplementedException();
    }

    public void AddRecord(string path, Employee newItem)
    {
        throw new NotImplementedException();
    }
}