namespace AdventOfCode.Day01;

public class Part2 : IChallenge
{
    public void Run()
    {
        Console.WriteLine("\n********** Part 2 **********");
        var input = File.ReadAllText("Day01/Inputs/input2.txt");
        Solve(input);
    }

    private void Solve(string input)
    {
        var lines = input.Split([Environment.NewLine], StringSplitOptions.None);

        var leftList = new List<int>();
        var rightList = new List<int>();

        foreach (var line in lines)
        {
            var numbers = line.Split("   ").Select(int.Parse).ToArray();
            leftList.Add(numbers[0]);
            rightList.Add(numbers[1]);
        }

        var similarityScores = new List<int>();
        foreach (var number in leftList)
        {
            var rightListOccurrences = rightList.Count(x => x == number);
            var similarityScore = number * rightListOccurrences;
            similarityScores.Add(similarityScore);
        }
        
        var sum = similarityScores.Sum();
        Console.WriteLine($"Sum of all similarity scores: {sum}");
    }
}
