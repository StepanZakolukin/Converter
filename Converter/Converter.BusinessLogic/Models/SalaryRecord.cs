namespace Converter.Application.Models;

public record SalaryRecord
{
    public required string Month { get; init; }
    public required double Amount { get; init; }
}