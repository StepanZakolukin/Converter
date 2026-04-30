namespace Converter.Application.Models;

public record Employee
{
    public required string Name { get; init; }
    public required string Surname { get; init; }
    public required SalaryInfo SalaryInfo { get; init; }
}