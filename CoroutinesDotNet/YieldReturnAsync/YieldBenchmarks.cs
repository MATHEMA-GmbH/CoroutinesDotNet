using BenchmarkDotNet.Attributes;

namespace YieldReturnAsync
{
    [MemoryDiagnoser]
    public class YieldBenchmarks
    {
        [Benchmark]
        public async Task ProcessBotsAsync()
        {
            //with million there is a hug difference in terms of memory consumption
            //but with 1000 not much
            var bots = GetBotsAsync(1_000_000);
            await foreach (var bot in bots)
            {
                if (bot.Id < 1000)
                    Console.WriteLine($"Id: {bot.Id}, Name: {bot.Name}");
                else
                    break;
            }
        }

        async IAsyncEnumerable<Bot> GetBotsAsync(int count)
        {
            List<Bot> bots = new List<Bot>();
            for (int i = 0; i < count; i++)
            {
                bots.Add(new Bot() { Id = i, Name = $"Name {i}" });
                await Task.Yield(); // Yield control to avoid blocking
            }
            foreach (var bot in bots)
            {
                yield return bot;
            }
        }

        [Benchmark]
        public async Task ProcessBotsYieldAsync()
        {
            var bots = GetBotsYieldAsync(1_000_000);
            await foreach (var bot in bots)
            {
                if (bot.Id < 1000)
                    Console.WriteLine($"Id: {bot.Id}, Name: {bot.Name}");
                else
                    break;
            }
        }

        async IAsyncEnumerable<Bot> GetBotsYieldAsync(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new Bot() { Id = i, Name = $"Name {i}" };
                await Task.Yield(); // Yield control to avoid blocking
            }
        }
    }
}
