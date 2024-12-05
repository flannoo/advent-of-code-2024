namespace AdventOfCode.Day05;

public sealed record PageOrderRule
{
    public int PageNumberBefore { get; init; }
    public int PageNumber { get; init; }
}

public sealed record PageUpdate
{
    public List<int> PageNumbers { get; init; } = [];
}
