var _lock = new object();
var cts = new CancellationTokenSource();
bool coroutinesRunning = true;

// Start the coroutines in a background thread
Task.Run(() => StartCoroutines(cts.Token));

Console.WriteLine("Press any key to stop...");
Console.ReadKey();
cts.Cancel();  // Signal cancellation to the coroutines

void StartCoroutines(CancellationToken token)
{
    var coroutineA = CoroutineA(token);
    var coroutineB = CoroutineB(token);

    // Keep running while cancellation hasn't been requested
    while (!token.IsCancellationRequested)
    {
        // Move the coroutines forward
        bool aIsRunning = coroutineA.MoveNext();
        bool bIsRunning = coroutineB.MoveNext();

        // If either coroutine is still running, keep going
        if (!aIsRunning && !bIsRunning)
        {
            coroutinesRunning = false; // If both coroutines finish, stop
            break;
        }

        // Delay to simulate work
        Thread.Sleep(25);
    }
}

IEnumerator<bool> CoroutineA(CancellationToken token)
{
    while (!token.IsCancellationRequested)
    {
        for (int i = 0; i < 80; i++)
        {
            if (token.IsCancellationRequested) yield break;

            lock (_lock)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write($"{nameof(CoroutineA)}: {new string('A', i)}");
            }

            yield return true;  // Yield and continue in the next iteration
        }

        // Clear the line after finishing
        lock (_lock)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(new string(' ', Console.WindowWidth));
        }
    }
}

IEnumerator<bool> CoroutineB(CancellationToken token)
{
    while (!token.IsCancellationRequested)
    {
        for (int i = 0; i < 80; i++)
        {
            if (token.IsCancellationRequested) yield break;

            lock (_lock)
            {
                Console.SetCursorPosition(0, 1);
                Console.Write($"{nameof(CoroutineB)}: {new string('B', i)}");
            }

            yield return true;  // Yield and continue in the next iteration
        }

        // Clear the line after finishing
        lock (_lock)
        {
            Console.SetCursorPosition(0, 1);
            Console.Write(new string(' ', Console.WindowWidth));
        }
    }
}