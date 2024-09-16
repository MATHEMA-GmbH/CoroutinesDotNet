using BenchmarkDotNet.Running;
using YieldReturnAsync;

var summary = BenchmarkRunner.Run<YieldBenchmarks>();

static async Task ProcessBotsAsync()
{
    await foreach (var bot in GetBotsWithNormalLoopAsync(1_000_000))
    {
        if (bot.Id < 1000)
            Console.WriteLine($"Id: {bot.Id}, Name: {bot.Name}");
        else
            break;
    }
}

#region normal loop async
static async IAsyncEnumerable<Bot> GetBotsWithNormalLoopAsync(int count)
{
    List<Bot> bots = new List<Bot>();
    for (int i = 0; i < count; i++)
    {
        bots.Add(new Bot() { Id = i, Name = $"Name {i}" });
        await Task.Yield(); // Yield control to the caller to mimic async work
    }
    foreach (var bot in bots)
    {
        yield return bot;
    }
}
#endregion

#region with yield async
static async IAsyncEnumerable<Bot> GetBotsWithYieldAsync(int count)
{
    for (int i = 0; i < count; i++)
    {
        yield return new Bot() { Id = i, Name = $"Name {i}" };
        await Task.Yield(); // Yield control to the caller to mimic async work
    }
}
#endregion