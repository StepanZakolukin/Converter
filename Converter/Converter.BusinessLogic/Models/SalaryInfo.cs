namespace Converter.Application.Models;

public record SalaryInfo
{
    public double TotalSalary => MonthlySalaries.Sum(salary => salary.Amount);
    public required IReadOnlyList<SalaryDetail> MonthlySalaries { get; init; }
}