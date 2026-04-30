namespace Converter.Application.Models;

public record SalaryDetail
{
    public required string Month { get; init; }
    public required double Amount { get; init; }
}