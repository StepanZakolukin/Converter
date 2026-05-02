namespace Converter.BusinessLogic.Models;

public record Employee
{
    public required FullName Name { get; init; }
    public required SalaryInfo SalaryInfo { get; init; }
}