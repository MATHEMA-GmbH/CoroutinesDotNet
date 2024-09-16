using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using CoroutinesVsThreads;

var config = new ManualConfig()
    .AddJob(Job.Default.WithToolchain(InProcessEmitToolchain.Instance).WithWarmupCount(1).WithIterationCount(10))
    .AddLogger(ConsoleLogger.Default) // Agregar logger de consola
    .AddColumnProvider(BenchmarkDotNet.Columns.DefaultColumnProviders.Instance);

var summary = BenchmarkRunner.Run<BenchmarkTests>(config);

[MemoryDiagnoser]
public class BenchmarkTests
{
    CancellationTokenSource cts = new CancellationTokenSource();
    static  BlockingCollection<string> messageQueue = new BlockingCollection<string>();
    static object _lock = new object();

    [Benchmark]
    public async Task CoroutinesBenchmarkIEnumerable()
    {
        cts = new CancellationTokenSource();
        cts.CancelAfter(1000);
        var consoleTask = Task.Run(() => ConsoleWriter(cts.Token));
        
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        await DriveCoroutinesAsync(cts.Token);
        stopwatch.Stop();
        
        cts.Cancel();
        consoleTask.Wait();
        await consoleTask;
        Console.WriteLine($"CoroutinesBenchmark Time: {stopwatch.ElapsedMilliseconds} ms");
    }
    
    [Benchmark]
    public async Task CoroutinesBenchmarkIAsyncEnumerable()
    {
        using var cts = new CancellationTokenSource(1000);
        var consoleTask = Task.Run(() => ConsoleWriter(cts.Token));

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        try
        {
            await DriveCoroutinesAsync<int>(
                intervalMs: 50,
                cts.Token,
                CoroutineA, CoroutineB);
        }
        catch (OperationCanceledException)
        {
            // Manejar la cancelación aquí si es necesario
        }
        stopwatch.Stop();

        cts.Cancel();
        await consoleTask;
        Console.WriteLine($"CoroutinesBenchmark Time: {stopwatch.ElapsedMilliseconds} ms");
    }

    [Benchmark]
    public void ThreadsBenchmark()
    {
        cts = new CancellationTokenSource();
        cts.CancelAfter(1000);
        var consoleTask = Task.Run(() => ConsoleWriter(cts.Token));
        
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        DriveThreads(cts.Token, _lock);
        stopwatch.Stop();
        
        cts.Cancel();
        consoleTask.Wait();
        Console.WriteLine($"ThreadsBenchmark Time: {stopwatch.ElapsedMilliseconds} ms");
    }

    private static async Task DriveCoroutinesAsync(CancellationToken token)
    {
        try
        {
            using var combined = CoroutineCombinator<int>.Combine(
                    CoroutineAIEnumerable,
                    CoroutineBIEnumerable)
                .GetEnumerator();

            while (!token.IsCancellationRequested)
            {
                if (!combined.MoveNext())
                {
                    break;
                }
                await Task.Delay(25, token); // Use Task.Delay with cancellation token
            }
        }
        catch (OperationCanceledException)
        {
            // This exception is expected on cancellation
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in DriveCoroutinesAsync: {ex.Message}");
        }
    }

    private static IEnumerable<int> CoroutineAIEnumerable()
    {
        for (int i = 0; i < 80; i++)
        {
            messageQueue.Add($"{nameof(CoroutineAIEnumerable)}: {new string('A', i)}");
            yield return i;
        }
    }

    private static IEnumerable<int> CoroutineBIEnumerable()
    {
        for (int i = 0; i < 80; i++)
        {
            messageQueue.Add($"{nameof(CoroutineBIEnumerable)}: {new string('B', i)}");
            yield return i;
        }
    }
    
    private static async Task DriveCoroutinesAsync<T>(
        int intervalMs,
        CancellationToken token,
        params Func<CancellationToken, IAsyncEnumerable<T>>[] coroutines)
    {
        var tasks = coroutines.Select(async coroutine => 
        {
            var interval = new Interval();
            await foreach (var item in coroutine(token).WithCancellation(token))
            {
                await interval.Delay(intervalMs, token);
            }
        });

        await Task.WhenAll(tasks); 
    }
    
    private static async IAsyncEnumerable<int> CoroutineA(
        [EnumeratorCancellation] CancellationToken token)
    {
        var inputIdler = new InputIdler();
        for (int i = 0; i < 80; i++)
        {
            // yield to the event loop to process any keyboard/mouse input first
            await inputIdler.Yield(token);
            
            messageQueue.Add($"{nameof(CoroutineA)}: {new String('A', i)}");
            yield return i;
        }
    }
    
    private static async IAsyncEnumerable<int> CoroutineB(
        [EnumeratorCancellation] CancellationToken token)
    {
        var inputIdler = new InputIdler();
        for (int i = 0; i < 80; i++)
        {
            // yield to the event loop to process any keyboard/mouse input first
            await inputIdler.Yield(token);

            messageQueue.Add($"{nameof(CoroutineB)}: {new String('B', i)}");

            // slow down CoroutineB
            await Task.Delay(25, token);
            yield return i;
        }
    }

    static void DriveThreads(CancellationToken token, object _lock)
    {
        var threadA = new Thread(() => ThreadA(token, _lock));
        var threadB = new Thread(() => ThreadB(token, _lock));

        threadA.Start();
        threadB.Start();

        threadA.Join();
        threadB.Join();
    }

    static void ThreadA(CancellationToken token, object _lock)
    {
        try
        {
            while (!token.IsCancellationRequested)
            {
                for (int i = 0; i < 80; i++)
                {
                    if (token.IsCancellationRequested) break;
                    messageQueue.Add($"{nameof(ThreadA)}: {new string('A', i)}");
                    Thread.Sleep(25);
                }

                if (token.IsCancellationRequested) break;
            }
        }
        catch (Exception ex)
        {
            messageQueue.Add($"Exception in {nameof(ThreadA)}: {ex.Message}");
        }
    }

    static void ThreadB(CancellationToken token, object _lock)
    {
        try
        {
            while (!token.IsCancellationRequested)
            {
                for (int i = 0; i < 80; i++)
                {
                    if (token.IsCancellationRequested) break;
                    messageQueue.Add($"{nameof(ThreadB)}: {new string('B', i)}");
                    Thread.Sleep(25);
                }

                if (token.IsCancellationRequested) break;
            }
        }
        catch (Exception ex)
        {
            messageQueue.Add($"Exception in {nameof(ThreadB)}: {ex.Message}");
        }
    }
    
    private static void ConsoleWriter(CancellationToken token)
    {
        try
        {
            while (!token.IsCancellationRequested || messageQueue.Count > 0)
            {
                if (messageQueue.TryTake(out var message, 100))
                {
                    lock (_lock)
                    {
                        Console.WriteLine(message);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in {nameof(ConsoleWriter)}: {ex.Message}");
        }
    }
}

public class InProcessConfig : ManualConfig
{
    public InProcessConfig()
    {
        AddJob(Job.Default.WithToolchain(InProcessEmitToolchain.Instance).WithWarmupCount(1).WithIterationCount(10));
    }
}

public class Interval
{
    private readonly Stopwatch _stopwatch = new Stopwatch();

    public Interval()
    {
        _stopwatch.Start();
    }

    public void Reset()
    {
        _stopwatch.Reset();
    }

    public async ValueTask Delay(int intervalMs, CancellationToken token)
    {
        var delay = intervalMs - (int)_stopwatch.ElapsedMilliseconds;
        if (delay > 0)
        {
            try
            {
                await Task.Delay(delay, token);
            }
            catch (TaskCanceledException)
            {
                // La tarea fue cancelada, manejarlo según sea necesario
                // Por ejemplo, podrías simplemente retornar o re-lanzar la excepción si es crítico
                return;
            }
        }
        Reset();
        token.ThrowIfCancellationRequested();
    }
}

public class InputIdler : SimpleValueTaskSource
    {
        private CancellationToken _token = default;

        private void OnIdle(object? s, EventArgs e)
        {
            if (!AnyInputMessage() || _token.IsCancellationRequested)
            {
                Complete();
            }
        }

        public override void Close()
        {
            System.Windows.Forms.Application.Idle -= OnIdle;
        }

        public async ValueTask Yield(CancellationToken token)
        {
            if (AnyInputMessage())
            {
                System.Windows.Forms.Application.Idle += OnIdle;
                try
                {
                    await GetValueTask();
                }
                catch (OperationCanceledException)
                {
                    // La tarea fue cancelada, manejarlo según sea necesario
                    return;
                }
                finally
                {
                    System.Windows.Forms.Application.Idle -= OnIdle;
                }
            }
            token.ThrowIfCancellationRequested();
        }

        public static bool AnyInputMessage()
        {
            uint status = GetQueueStatus(QS_INPUT);
            return (status >> 16) != 0;
        }

        [DllImport("user32.dll")]
        private static extern uint GetQueueStatus(uint flags);

        private const uint QS_KEY = 0x0001;
        private const uint QS_MOUSEMOVE = 0x0002;
        private const uint QS_MOUSEBUTTON = 0x0004;
        private const uint QS_POSTMESSAGE = 0x0008;
        private const uint QS_TIMER = 0x0010;
        private const uint QS_PAINT = 0x0020;
        private const uint QS_SENDMESSAGE = 0x0040;
        private const uint QS_HOTKEY = 0x0080;
        private const uint QS_ALLPOSTMESSAGE = 0x0100;
        private const uint QS_RAWINPUT = 0x0400;

        private const uint QS_MOUSE = (QS_MOUSEMOVE | QS_MOUSEBUTTON);

        private const uint QS_INPUT = (QS_MOUSE | QS_KEY | QS_RAWINPUT);
        private const uint QS_ALLEVENTS = (QS_INPUT | QS_POSTMESSAGE | QS_TIMER | QS_PAINT | QS_HOTKEY);

        private const uint QS_ALLINPUT = 0x4FF;
    }
    
    //TODO: cancellation
    public abstract class SimpleValueTaskSource: IDisposable, IValueTaskSource
    {
        private short _currentTaskToken = 1;
        private bool _isTaskCompleted = false;
        private (Action<object>?, object?) _continuation;

        public abstract void Close();

        protected void Complete()
        {
            _isTaskCompleted = true;
            var (callback, state) = _continuation;
            _continuation = default;
            callback?.Invoke(state!);
        }

        protected ValueTask GetValueTask() =>
            new ValueTask(this, _currentTaskToken);

        public void Dispose() => Close();

        void IValueTaskSource.GetResult(short token)
        {
            ThrowIfInvalidToken(token);
            ThrowIfIncomplete();
            _isTaskCompleted = false;
            _currentTaskToken += 2; // we don't want this to ever be zero
            _currentTaskToken &= short.MaxValue;
        }

        ValueTaskSourceStatus IValueTaskSource.GetStatus(short token)
        {
            ThrowIfInvalidToken(token);
            return _isTaskCompleted ?
                ValueTaskSourceStatus.Succeeded :
                ValueTaskSourceStatus.Pending;
        }

        void IValueTaskSource.OnCompleted(Action<object>? continuation, object? state, short token, ValueTaskSourceOnCompletedFlags flags)
        {
            ThrowIfInvalidToken(token);
            ThrowIfMultipleContinuations();
            _continuation = (continuation, state);
        }

        #region Throw helpers
        private void ThrowIfInvalidToken(short token)
        {
            if (_currentTaskToken != token)
            {
                throw new InvalidOperationException(nameof(ThrowIfInvalidToken));
            }
        }

        private void ThrowIfIncomplete()
        {
            if (!_isTaskCompleted)
            {
                throw new InvalidOperationException(nameof(ThrowIfIncomplete));
            }
        }

        private void ThrowIfMultipleContinuations()
        {
            if (_continuation != default)
            {
                throw new InvalidOperationException(nameof(ThrowIfMultipleContinuations));
            }
        }
        #endregion
    }