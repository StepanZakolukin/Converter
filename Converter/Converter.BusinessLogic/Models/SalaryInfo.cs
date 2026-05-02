namespace Converter.BusinessLogic.Models;

public record SalaryInfo
{
    public double TotalSalary => MonthlySalaries.Sum(salary => salary.Amount);
    public double GetAmountForMonth(string month) => 
        MonthlySalaries
            .Where(salary => salary.Month == month)
            .Sum(salary => salary.Amount);
    public required IReadOnlyList<Salary> MonthlySalaries { get; init; }
}