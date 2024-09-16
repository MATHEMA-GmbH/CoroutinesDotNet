object lockOut = new object();

var cts = new CancellationTokenSource();

Task.Run(() => DriveThreads(cts.Token, lockOut));

Console.WriteLine("Press any key to stop...");
Console.ReadKey();
cts.Cancel();

static void DriveThreads(CancellationToken token, object lockIt)
{
    var threadA = new Thread(() => ThreadA(token, lockIt));
    var threadB = new Thread(() => ThreadB(token, lockIt));

    threadA.Start();
    threadB.Start();

    threadA.Join();
    threadB.Join();
}

static void ThreadA(CancellationToken token, object lockIt)
{
    try
    {
        while (!token.IsCancellationRequested)
        {
            for (int i = 0; i < 80; i++)
            {
                if (token.IsCancellationRequested) break;

                lock (lockIt)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.Write($"{nameof(ThreadA)}: {new string('A', i)}");
                }
                Thread.Sleep(25);
            }

            if (token.IsCancellationRequested) break;

            // Clear the line
            lock (lockIt)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write(new string(' ', Console.WindowWidth));
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception in {nameof(ThreadA)}: {ex.Message}");
    }
}

static void ThreadB(CancellationToken token, object lockIt)
{
    try
    {
        while (!token.IsCancellationRequested)
        {
            for (int i = 0; i < 80; i++)
            {
                if (token.IsCancellationRequested) break;

                lock (lockIt)
                {
                    Console.SetCursorPosition(0, 1);
                    Console.Write($"{nameof(ThreadB)}: {new string('B', i)}");
                }
                Thread.Sleep(25);
            }

            if (token.IsCancellationRequested) break;

            // Clear the line
            lock (lockIt)
            {
                Console.SetCursorPosition(0, 1);
                Console.Write(new string(' ', Console.WindowWidth));
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception in {nameof(ThreadB)}: {ex.Message}");
    }
}