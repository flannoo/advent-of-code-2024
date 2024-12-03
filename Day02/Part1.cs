namespace AdventOfCode.Day02;

public class Part1 : IChallenge
{
    public void Run()
    {
        Console.WriteLine("\n********** Part 1 **********");
        var input = File.ReadAllText("Day02/Inputs/input1.txt");
        Solve(input);
    }

    private void Solve(string input)
    {
        var lines = input.Split([Environment.NewLine], StringSplitOptions.None);

        var reports = new List<Report>();

        foreach (var line in lines)
        {
            try
            {
                var numbers = line.Split(" ").Select(int.Parse).ToList();
                reports.Add(Report.Create(numbers));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Skipping line {line}: {ex.Message})");
            }
        }
        
        var numberOfSafeReports = reports.Count(x => x.IsSafe);
        Console.WriteLine($"Number of safe reports: {numberOfSafeReports}");
    }
}
