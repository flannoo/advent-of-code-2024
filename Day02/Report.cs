namespace AdventOfCode.Day02;

public sealed class Report
{
    public List<int> Levels { get; init; } = [];
    public bool IsSafe { get; init; }

    public static Report Create(List<int> levels)
    {
        if (levels.Count < 2)
        {
            throw new ArgumentException("Report must have at least 2 levels");
        }

        var isSafe = IsSafeReport(levels);

        return new Report() { Levels = levels, IsSafe = isSafe };
    }

    public static Report CreateDampened(List<int> levels)
    {
        if (levels.Count < 2)
        {
            throw new ArgumentException("Report must have at least 2 levels");
        }

        var isSafe = IsSafeReport(levels);

        if (!isSafe)
        {
            for (int i = 0; i < levels.Count; i++)
            {
                var modifiedLevels = new List<int>(levels);
                modifiedLevels.RemoveAt(i);

                var isSafeWithRemoval = IsSafeReport(modifiedLevels);
                if (isSafeWithRemoval)
                {
                    isSafe = true;
                    break;
                }
            }
        }

        return new Report() { Levels = levels, IsSafe = isSafe };
    }

    private static bool IsSafeReport(List<int> levels)
    {
        var reportRules = new ReportRules();
        var isIncreasing = reportRules.AllAdjacentLevelsAreIncreasing(levels);
        var isDecreasing = reportRules.AllAdjacentLevelsAreDecreasing(levels);
        var isDistanceMaxThree = reportRules.AllAdjacentLevelsDistanceIsMaxThree(levels);

        return (isIncreasing || isDecreasing) && isDistanceMaxThree;
    }
}

public sealed class ReportRules
{
    public bool AllAdjacentLevelsAreIncreasing(List<int> levels)
    {
        var previousLevel = levels[0];
        for (int i = 1; i < levels.Count; i++)
        {
            var currentLevel = levels[i];
            if (currentLevel <= previousLevel)
            {
                return false;
            }

            previousLevel = currentLevel;
        }

        return true;
    }

    public bool AllAdjacentLevelsAreDecreasing(List<int> levels)
    {
        var previousLevel = levels[0];
        for (int i = 1; i < levels.Count; i++)
        {
            var currentLevel = levels[i];
            if (currentLevel >= previousLevel)
            {
                return false;
            }

            previousLevel = currentLevel;
        }

        return true;
    }

    public bool AllAdjacentLevelsDistanceIsMaxThree(List<int> levels)
    {
        var previousLevel = levels[0];
        for (int i = 1; i < levels.Count; i++)
        {
            var currentLevel = levels[i];

            var distance = Math.Abs(currentLevel - previousLevel);

            if (distance is < 1 or > 3)
            {
                return false;
            }

            previousLevel = currentLevel;
        }

        return true;
    }
}
