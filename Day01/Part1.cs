namespace AdventOfCode.Day01;

public class Part1 : IChallenge
{
    public void Run()
    {
        Console.WriteLine("\n********** Part 1 **********");
        var input = File.ReadAllText("Day01/Inputs/input1.txt");
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

        leftList.Sort();
        rightList.Sort();

        var distanceList = new List<int>();
        for (var i = 0; i < leftList.Count; i++)
        {
            var leftListNumber = leftList[i];
            var rightListNumber = rightList[i];
            var distance = Math.Abs(leftListNumber - rightListNumber);
            distanceList.Add(distance);
        }
        
        var sum = distanceList.Sum();
        Console.WriteLine($"Sum of all distances: {sum}");
    }
}
