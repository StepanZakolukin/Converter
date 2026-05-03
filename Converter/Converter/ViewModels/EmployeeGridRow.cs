namespace Converter.ViewModels;

public record EmployeeGridRow
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required double January { get; init; }
    public required double February { get; init; }
    public required double March { get; init; }
    public required double April { get; init; }
    public required double May { get; init; }
    public required double June { get; init; }
    public required double July { get; init; }
    public double August { get; init; }
    public required double September { get; init; }
    public required double October { get; init; }
    public required double November { get; init; }
    public required double December { get; init; }
    public required double Total { get; init; }
}