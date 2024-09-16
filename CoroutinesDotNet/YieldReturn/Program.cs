using BenchmarkDotNet.Running;
using YieldReturn;

var summary = BenchmarkRunner.Run<YieldBenchmarks>();

static void ProcessBots()
{
    var bots = GetBotsWithNormalLoop(1_000_000);
    foreach (var bot in bots)
    {
        if (bot.Id < 1000)
            Console.WriteLine($"Id: {bot.Id}, Name: {bot.Name}");
        else
            break;
    }
}

#region normal loop
static IEnumerable<Bot> GetBotsWithNormalLoop(int count)
{
    List<Bot> bots = new List<Bot>();
    for (int i = 0; i < count; i++)
    {
        bots.Add(new Bot() { Id = i, Name = $"Name {i}" });
    }
    return bots;
}
#endregion

#region with yield
static IEnumerable<Bot> GetBotsWithYield(int count)
{
    //kind of on demand, just that we need
    //iot devices
    //source: C# Yield Return: What is it and how does it work?
    //https://www.youtube.com/watch?v=HRXkeaeImGs
    for (int i = 0; i < count; i++)
    {
        yield return new Bot() { Id = i, Name = $"Name {i}" };
    }
}
#endregion
