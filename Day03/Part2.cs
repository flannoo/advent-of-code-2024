using System.Text.RegularExpressions;

namespace AdventOfCode.Day03;

public class Part2 : IChallenge
{
    public void Run()
    {
        Console.WriteLine("\n********** Part 2 **********");
        var input = File.ReadAllText("Day03/Inputs/input2.txt");
        Solve(input);
    }

    private void Solve(string input)
    {
        var pattern = @"mul\((\w{1,3}|\d+),(\w{1,3}|\d+)\)";
        var controlPattern = @"do\(\)|don't\(\)";
        var matches = Regex.Matches(input, pattern);
        var controlMatches = Regex.Matches(input, controlPattern);

        var instructionResults = new List<int>();
        foreach (Match match in matches)
        {
            Match? lastControlMatch = null;
            foreach (Match controlMatch in controlMatches)
            {
                if (controlMatch.Index < match.Index)
                {
                    lastControlMatch = controlMatch;
                }
                else
                {
                    break;
                }
            }
            
            if (lastControlMatch is { Value: "don't()" })
            {
                continue;
            }
            
            var firstOperand = int.Parse(match.Groups[1].Value);
            var secondOperand = int.Parse(match.Groups[2].Value);
            instructionResults.Add(firstOperand * secondOperand);
        }

        Console.WriteLine($"Result: {instructionResults.Sum()}");
    }
}

