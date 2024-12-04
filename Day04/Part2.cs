namespace AdventOfCode.Day04;

public class Part2 : IChallenge
{
    public void Run()
    {
        Console.WriteLine("\n********** Part 2 **********");
        var input = File.ReadAllText("Day04/Inputs/input2.txt");
        Solve(input);
    }

    private void Solve(string input)
    {
        var lines = input.Split([Environment.NewLine], StringSplitOptions.None);

        var rowCount = lines.Length;
        var colCount = lines[0].Length;

        var puzzle = new char[rowCount, colCount];

        for (var row = 0; row < rowCount; row++)
        {
            for (var col = 0; col < colCount; col++)
            {
                puzzle[row, col] = lines[row][col];
            }
        }

        var occurrences = 0;

        for (var row = 1; row < puzzle.GetLength(0) - 1; row++)
        {
            for (var col = 1; col < puzzle.GetLength(1) - 1; col++)
            {
                if (SearchXPattern(puzzle, row, col))
                {
                    occurrences++;
                }
            }
        }

        Console.WriteLine($"Result: {occurrences.ToString()}");
    }

    private bool SearchXPattern(char[,] puzzle, int row, int col)
    {
        // Check only for 'A' as the center of the pattern
        if (puzzle[row, col] == 'A')
        {
            return HasPattern(puzzle, row, col, 'M', 'S');
        }

        return false;
    }

    private bool HasPattern(char[,] puzzle, int row, int col, char center1, char center2)
    {
        // Check both diagonal orientations
        return (puzzle[row - 1, col - 1] == center1 && puzzle[row + 1, col + 1] == center2 &&
                puzzle[row - 1, col + 1] == center1 && puzzle[row + 1, col - 1] == center2) ||
               (puzzle[row - 1, col - 1] == center1 && puzzle[row + 1, col + 1] == center2 &&
                puzzle[row - 1, col + 1] == center2 && puzzle[row + 1, col - 1] == center1) ||
               (puzzle[row - 1, col - 1] == center2 && puzzle[row + 1, col + 1] == center1 &&
                puzzle[row - 1, col + 1] == center2 && puzzle[row + 1, col - 1] == center1) ||
               (puzzle[row - 1, col - 1] == center2 && puzzle[row + 1, col + 1] == center1 &&
                puzzle[row - 1, col + 1] == center1 && puzzle[row + 1, col - 1] == center2);
    }
}
