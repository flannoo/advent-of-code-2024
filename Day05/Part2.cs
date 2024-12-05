namespace AdventOfCode.Day05;

public class Part2 : IChallenge
{
    public void Run()
    {
        Console.WriteLine("\n********** Part 2 **********");
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
            if (!IsPageUpdateValid(pageUpdate.PageNumbers, pageOrderRules))
            {
                var orderedPageNumbers = OrderPageNumbers(pageUpdate.PageNumbers, pageOrderRules);
                var middleIndex = orderedPageNumbers.Count / 2;
                middlePageNumbers.Add(orderedPageNumbers[middleIndex]);
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
    
    private List<int> OrderPageNumbers(List<int> pageNumbers, List<PageOrderRule> pageOrderRules)
    {
        var priority = new Dictionary<(int, int), int>();

        foreach (var rule in pageOrderRules)
        {
            if (pageNumbers.Contains(rule.PageNumberBefore) && pageNumbers.Contains(rule.PageNumber))
            {
                priority[(rule.PageNumberBefore, rule.PageNumber)] = -1;  // Prefer PageNumberBefore comes first
                priority[(rule.PageNumber, rule.PageNumberBefore)] = 1;   // Prefer PageNumber comes after
            }
        }

        pageNumbers.Sort((a, b) =>
        {
            if (a == b) return 0;

            if (priority.ContainsKey((a, b)))
                return priority[(a, b)];
            if (priority.ContainsKey((b, a)))
                return priority[(b, a)];

            return a.CompareTo(b);
        });

        return pageNumbers;
    }
}
