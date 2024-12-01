using System.Reflection;
using AdventOfCode;

var days = Enumerable.Range(1, 25).Select(day => $"Day{day:D2}").ToArray();
foreach (var day in days)
{
    LoadAndRunDayChallenges(day);
}

Console.WriteLine("======== All challenges completed ========");
static void LoadAndRunDayChallenges(string dayFolder)
{
    var currentAssembly = Assembly.GetExecutingAssembly();
    var challengeTypes = currentAssembly.GetTypes()
        .Where(t => typeof(IChallenge).IsAssignableFrom(t) && !t.IsInterface && t.Namespace == $"AdventOfCode.{dayFolder}");
    
    Console.WriteLine($"\n======== Advent Of Code: {dayFolder} ========");
    foreach (var challengeType in challengeTypes)
    {
        if (Activator.CreateInstance(challengeType) is IChallenge challengeInstance)
        {
            challengeInstance.Run();
        }
        else
        {
            Console.WriteLine($"Failed to create an instance of {challengeType.Name}.");
        }
    }
}