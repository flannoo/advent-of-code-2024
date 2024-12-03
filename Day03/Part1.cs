using System.Text.RegularExpressions;

namespace AdventOfCode.Day03;

public class Part1 : IChallenge
{
    public void Run()
    {
        Console.WriteLine("\n********** Part 1 **********");
        var input = File.ReadAllText("Day03/Inputs/input1.txt");
        Solve(input);
    }

    private void Solve(string input)
    {
        var pattern = @"mul\((\w{1,3}|\d+),(\w{1,3}|\d+)\)";
        var matches = Regex.Matches(input, pattern);

        var instructionResults = new List<int>();
        foreach (Match match in matches)
        {
            var firstOperand = int.Parse(match.Groups[1].Value);
            var secondOperand = int.Parse(match.Groups[2].Value);
            instructionResults.Add(firstOperand * secondOperand);
        }

        Console.WriteLine($"Result: {instructionResults.Sum()}");
    }
}
