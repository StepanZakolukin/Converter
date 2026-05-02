namespace Converter.BusinessLogic.Models;

public record Salary
{
    public required string Month { get; init; }
    public required double Amount { get; init; }
}