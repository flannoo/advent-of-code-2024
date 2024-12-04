namespace AdventOfCode.Day04;

public class Part1 : IChallenge
{
    public void Run()
    {
        Console.WriteLine("\n********** Part 1 **********");
        var input = File.ReadAllText("Day04/Inputs/input1.txt");
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

        const string wordToFind = "XMAS";
        var occurrences = 0;

        for (var row = 0; row < puzzle.GetLength(0); row++)
        {
            for (var col = 0; col < puzzle.GetLength(1); col++)
            {
                if (SearchHorizontalLeftToRight(puzzle, row, col, wordToFind))
                {
                    occurrences++;
                }
                
                if (SearchHorizontalRightToLeft(puzzle, row, col, wordToFind))
                {
                    occurrences++;
                }

                if (SearchVerticalTopToBottom(puzzle, row, col, wordToFind))
                {
                    occurrences++;
                }
                
                if (SearchVerticalBottomToTop(puzzle, row, col, wordToFind))
                {
                    occurrences++;
                }
                
                if (SearchDiagonalTopLeftToBottomRight(puzzle, row, col, wordToFind))
                {
                    occurrences++;
                }
                
                if (SearchDiagonalTopRightToBottomLeft(puzzle, row, col, wordToFind))
                {
                    occurrences++;
                }
                
                if (SearchDiagonalBottomLeftToTopRight(puzzle, row, col, wordToFind))
                {
                    occurrences++;
                }
                
                if (SearchDiagonalBottomRightToTopLeft(puzzle, row, col, wordToFind))
                {
                    occurrences++;
                }
            }
        }

        Console.WriteLine($"Result: {occurrences.ToString()}");
    }

    private bool SearchHorizontalLeftToRight(char[,] puzzle, int row, int col, string word)
    {
        if (col + word.Length > puzzle.GetLength(1))
        {
            return false;
        }

        for (var i = 0; i < word.Length; i++)
        {
            if (puzzle[row, col + i] != word[i])
            {
                return false;
            }
        }
        return true;
    }

    private bool SearchHorizontalRightToLeft(char[,] puzzle, int row, int col, string word)
    {
        if (col - word.Length < -1)
        {
            return false;
        }

        for (var i = 0; i < word.Length; i++)
        {
            if (puzzle[row, col - i] != word[i])
            {
                return false;
            }
        }
        return true;
    }

    private bool SearchVerticalTopToBottom(char[,] puzzle, int row, int col, string word)
    {
        if (row + word.Length > puzzle.GetLength(0))
        {
            return false;
        }

        for (var i = 0; i < word.Length; i++)
        {
            if (puzzle[row + i, col] != word[i])
            {
                return false;
            }
        }
        return true;
    }

    private bool SearchVerticalBottomToTop(char[,] puzzle, int row, int col, string word)
    {
        if (row - word.Length < -1)
        {
            return false;
        }

        for (var i = 0; i < word.Length; i++)
        {
            if (puzzle[row - i, col] != word[i])
            {
                return false;
            }
        }
        return true;
    }

    private bool SearchDiagonalTopLeftToBottomRight(char[,] puzzle, int row, int col, string word)
    {
        if (row + word.Length > puzzle.GetLength(0) || col + word.Length > puzzle.GetLength(1))
        {
            return false;
        }

        for (var i = 0; i < word.Length; i++)
        {
            if (puzzle[row + i, col + i] != word[i])
            {
                return false;
            }
        }

        return true;
    }

    private bool SearchDiagonalBottomLeftToTopRight(char[,] puzzle, int row, int col, string word)
    {
        if (row - word.Length < -1 || col + word.Length > puzzle.GetLength(1))
        {
            return false;
        }

        for (var i = 0; i < word.Length; i++)
        {
            if (puzzle[row - i, col + i] != word[i])
            {
                return false;
            }
        }

        return true;
    }
    
    private bool SearchDiagonalTopRightToBottomLeft(char[,] puzzle, int row, int col, string word)
    {
        if (row + word.Length > puzzle.GetLength(0) || col - word.Length < -1)
        {
            return false;
        }

        for (var i = 0; i < word.Length; i++)
        {
            if (puzzle[row + i, col - i] != word[i])
            {
                return false;
            }
        }
        return true;
    }

    private bool SearchDiagonalBottomRightToTopLeft(char[,] puzzle, int row, int col, string word)
    {
        if (row - word.Length < -1 || col - word.Length < -1)
        {
            return false;
        }

        for (var i = 0; i < word.Length; i++)
        {
            if (puzzle[row - i, col - i] != word[i])
            {
                return false;
            }
        }
        return true;
    }
}
