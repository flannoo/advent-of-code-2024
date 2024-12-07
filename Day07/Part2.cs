namespace AdventOfCode.Day07;

public class Part2 : IChallenge
{
    public void Run()
    {
        Console.WriteLine("\n********** Part 2 **********");
        var input = File.ReadAllText("Day07/Inputs/input1.txt");
        Solve(input);
    }

    private void Solve(string input)
    {
        var lines = input.Split([Environment.NewLine], StringSplitOptions.None);
        
        var validEquations = new List<long>();

        foreach (var line in lines)
        {
            var parts = line.Split(":");
            var equationResult = long.Parse(parts[0].Trim());
            var equationNumbers = parts[1].Trim().Split(" ").Select(long.Parse).ToArray();

            if (EvaluateCombinations(equationNumbers, equationResult))
            {
                validEquations.Add(equationResult);
            }
        }

        var totalCalibration = validEquations.Sum();
        Console.WriteLine($"Result: {totalCalibration}");
    }
    
    private bool EvaluateCombinations(long[] numbers, long target)
    {
        // There are 3 choices per operator (+, *, ||) hence, Calculate 3^(n-1) combinations
        var combinationCount = (long)Math.Pow(3, numbers.Length - 1);

        for (var i = 0; i < combinationCount; i++)
        {
            if (EvaluateExpression(numbers, target, i))
            {
                return true;
            }
        }
        return false;
    }

    private bool EvaluateExpression(long[] numbers, long target, long combination)
    {
        var result = numbers[0];

        for (var i = 0; i < numbers.Length - 1; i++)
        {
            var currentOp = (int)((combination / (long)Math.Pow(3, i)) % 3);

            switch (currentOp)
            {
                case 0:
                    result += numbers[i + 1];
                    break;
                case 1:
                    result *= numbers[i + 1];
                    break;
                case 2:
                    result = Concatenate(result, numbers[i + 1]);
                    break;
            }
        }

        return result == target;
    }

    private long Concatenate(long a, long b)
    {
        var bString = b.ToString();
        return long.Parse(a.ToString() + bString);
    }
}
