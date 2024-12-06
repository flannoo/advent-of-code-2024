namespace AdventOfCode.Day06;

public class Part2 : IChallenge
{
    public void Run()
    {
        Console.WriteLine("\n********** Part 2 **********");
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
        var allCoordinates = new HashSet<(int x, int y)>();
        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < cols; c++)
            {
                allCoordinates.Add((r, c));
                switch (lines[r][c])
                {
                    case '^':
                        guardX = r;
                        guardY = c;
                        direction = 0;
                        break;
                    case '>':
                        guardX = r;
                        guardY = c;
                        direction = 1;
                        break;
                    case 'v':
                        guardX = r;
                        guardY = c;
                        direction = 2;
                        break;
                    case '<':
                        guardX = r;
                        guardY = c;
                        direction = 3;
                        break;
                }
            }
        }
        
        var workingObstacleCoordinates = new HashSet<(int x, int y)>();

        allCoordinates.ExceptWith(new HashSet<(int x, int y)>
        {
            (guardX, guardY)
        });

        foreach (var coordinate in allCoordinates)
        {
            var newGuardX = guardX;
            var newGuardY = guardY;
            var newDirection = direction;
            var visited = new Dictionary<(int x, int y), int>();
            HashSet<(int x, int y, int direction)> states = [];
            
            while (true)
            {
                var (dx, dy) = directions[newDirection];
                int nextX = newGuardX + dx, nextY = newGuardY + dy;

                // Check if next position is an obstacle or out of bounds
                var outOfBounds = nextX < 0 || nextX >= rows || nextY < 0 || nextY >= cols;
                if (outOfBounds)
                {
                    break;
                }

                var isObstacle = (lines[nextX][nextY] == '#' || (nextX == coordinate.x && nextY == coordinate.y));
                if (isObstacle)
                {
                    // Turn right (90 degrees clockwise)
                    newDirection = (newDirection + 1) % 4;
                }
                else
                {
                    // Move forward
                    newGuardX = nextX;
                    newGuardY = nextY;

                    var currentState = (newGuardX, newGuardY, newDirection);
            
                    // Detect cycle based on state
                    if (!states.Add(currentState))
                    {
                        workingObstacleCoordinates.Add(coordinate);
                        break;
                    }

                    // Track visit count for position
                    visited.TryGetValue((newGuardX, newGuardY), out var value);
                    visited[(newGuardX, newGuardY)] = value + 1;
                }
            }
        }

        Console.WriteLine($"Result: {workingObstacleCoordinates.Count}");
    }

    private HashSet<(int, int)> GetLineOfSightPositions(string[] lines,
        int startX,
        int startY,
        int direction,
        (int dx, int dy)[] directions)
    {
        var lineOfSight = new HashSet<(int, int)>();
        var (dx, dy) = directions[direction];

        int x = startX + dx, y = startY + dy;
        while (x >= 0 && x < lines.Length && y >= 0 && y < lines[0].Length && lines[x][y] != '#')
        {
            lineOfSight.Add((x, y));
            x += dx;
            y += dy;
        }

        return lineOfSight;
    }
}
