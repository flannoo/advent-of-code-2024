namespace AdventOfCode.Day05;

public class Part1 : IChallenge
{
    public void Run()
    {
        Console.WriteLine("\n********** Part 1 **********");
        var input = File.ReadAllText("Day05/Inputs/input1.txt");
        Solve(input);
    }

    private void Solve(string input)
    {
        var lines = input.Split([Environment.NewLine], StringSplitOptions.None);

        var pageOrderRules = new List<PageOrderRule>();
        var pageUpdates = new List<PageUpdate>();

        foreach (var line in lines)
        {
            if (line.Contains('|'))
            {
                var ruleParts = line.Split('|');

                var pageOrderRule = new PageOrderRule()
                {
                    PageNumberBefore = int.Parse(ruleParts[0]),
                    PageNumber = int.Parse(ruleParts[1])
                };
                pageOrderRules.Add(pageOrderRule);
            }

            if (line.Contains(','))
            {
                var pageNumbers = line.Split(',').Select(int.Parse).ToList();
                pageUpdates.Add(new PageUpdate { PageNumbers = pageNumbers });
            }
        }

        var middlePageNumbers = new List<int>();
        foreach(var pageUpdate in pageUpdates)
        {
            if (IsPageUpdateValid(pageUpdate.PageNumbers, pageOrderRules))
            {
                var middleIndex = pageUpdate.PageNumbers.Count / 2;
                middlePageNumbers.Add(pageUpdate.PageNumbers[middleIndex]);
            }
        }

        Console.WriteLine($"Result: {middlePageNumbers.Sum()}");
    }
    
    private bool IsPageUpdateValid(List<int> pageNumbers, List<PageOrderRule> pageOrderRules)
    {
        foreach (var rule in pageOrderRules)
        {
            var indexBefore = pageNumbers.IndexOf(rule.PageNumberBefore);
            var index = pageNumbers.IndexOf(rule.PageNumber);
        
            if(indexBefore == -1 || index == -1)
            {
                continue;
            }
            
            if (indexBefore > index)
            {
                return false;
            }
        }
        return true;
    }
}
