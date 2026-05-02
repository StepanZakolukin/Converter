namespace Converter.BusinessLogic.Models;

public record FullName
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}