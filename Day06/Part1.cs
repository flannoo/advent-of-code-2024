namespace AdventOfCode.Day06;

public class Part1 : IChallenge
{
    public void Run()
    {
        Console.WriteLine("\n********** Part 1 **********");
        var input = File.ReadAllText("Day06/Inputs/input1.txt");
        Solve(input);
    }

    private void Solve(string input)
    {
        var lines = input.Split([Environment.NewLine], StringSplitOptions.None);
        var rows = lines.Length;
        var cols = lines[0].Length;
        
        // Directions: up, right, down, left
        var directions = new (int dx, int dy)[] { (-1, 0), (0, 1), (1, 0), (0, -1) };
        
        // Locate the guards initial position and direction
        int guardX = 0, guardY = 0, direction = 0;
        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < cols; c++)
            {
                switch (lines[r][c])
                {
                    case '^':
                        guardX = r; guardY = c; direction = 0;
                        break;
                    case '>':
                        guardX = r; guardY = c; direction = 1;
                        break;
                    case 'v':
                        guardX = r; guardY = c; direction = 2;
                        break;
                    case '<':
                        guardX = r; guardY = c; direction = 3;
                        break;
                }
            }
        }
        
        var visited = new HashSet<(int x, int y)> { (guardX, guardY) };

        while (true)
        {
            var (dx, dy) = directions[direction];
            int nextX = guardX + dx, nextY = guardY + dy;

            // Check if next position is an obstacle or out of bounds
            var outOfBounds = nextX < 0 || nextX >= rows || nextY < 0 || nextY >= cols;
            var isObstacle = !outOfBounds && lines[nextX][nextY] == '#';

            if (isObstacle || outOfBounds)
            {
                // Turn right (90 degrees clockwise)
                direction = (direction + 1) % 4;
            }
            else
            {
                // Move forward
                guardX = nextX;
                guardY = nextY;
                visited.Add((guardX, guardY));
            }

            // If the guard moves out of bounds, we stop
            if (outOfBounds) break;
        }

        Console.WriteLine($"Result: {visited.Count}");
    }
}
