namespace AdventOfCode.Day07;

public class Part1 : IChallenge
{
    public void Run()
    {
        Console.WriteLine("\n********** Part 1 **********");
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
        var combinationCount = (long)Math.Pow(2, numbers.Length - 1);

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
            if ((combination & (1 << i)) != 0)
            {
                result += numbers[i + 1];
            }
            else
            {
                result *= numbers[i + 1];
            }
        }

        return result == target;
    }
}
