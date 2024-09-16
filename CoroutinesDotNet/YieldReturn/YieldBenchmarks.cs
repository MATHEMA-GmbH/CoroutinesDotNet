using BenchmarkDotNet.Attributes;

namespace YieldReturn
{
    [MemoryDiagnoser]
    public class YieldBenchmarks
    {
        [Benchmark]
        public void ProcessBots()
        {
            //with million there is a hug difference in terms of memory consumption
            //but with 1000 not much
            var bots = GetBots(1_000_000);
            foreach (var bot in bots)
            {
                if (bot.Id < 1000)
                    Console.WriteLine($"Id: {bot.Id}, Name: {bot.Name}");
                else
                    break;
            }
        }

        IEnumerable<Bot> GetBots(int count)
        {
            List<Bot> bots = new List<Bot>();
            for (int i = 0; i < count; i++)
            {
                bots.Add(new Bot() { Id = i, Name = $"Name {i}" });
            }
            return bots;
        }

        [Benchmark]
        public void ProcessBotsYield()
        {
            var bots = GetBotsYield(1_000_000);
            foreach (var bot in bots)
            {
                if (bot.Id < 1000)
                    Console.WriteLine($"Id: {bot.Id}, Name: {bot.Name}");
                else
                    break;
            }
        }

        IEnumerable<Bot> GetBotsYield(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new Bot() { Id = i, Name = $"Name {i}" };
            }
        }
    }
}