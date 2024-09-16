﻿using Coroutine;

var testEvent = new Event();

var seconds = CoroutineHandler.Start(WaitSeconds(), "Awesome Waiting Coroutine");
CoroutineHandler.Start(PrintEvery10Seconds(seconds));

CoroutineHandler.Start(EmptyCoroutine());

CoroutineHandler.InvokeLater(new Wait(5), () => {
    Console.WriteLine("Raising test event");
    CoroutineHandler.RaiseEvent(testEvent);
});
CoroutineHandler.InvokeLater(new Wait(testEvent), () => Console.WriteLine("Example event received"));

CoroutineHandler.InvokeLater(new Wait(testEvent), () => Console.WriteLine("I am invoked after 'Example event received'"), priority: -5);
CoroutineHandler.InvokeLater(new Wait(testEvent), () => Console.WriteLine("I am invoked before 'Example event received'"), priority: 2);

var lastTime = DateTime.Now;
while (true) {
    var currTime = DateTime.Now;
    CoroutineHandler.Tick(currTime - lastTime);
    lastTime = currTime;
    Thread.Sleep(1);
}

static IEnumerator<Wait> WaitSeconds() {
    Console.WriteLine("First thing " + DateTime.Now);
    yield return new Wait(1);
    Console.WriteLine("After 1 second " + DateTime.Now);
    yield return new Wait(9);
    Console.WriteLine("After 10 seconds " + DateTime.Now);
    CoroutineHandler.Start(NestedCoroutine());
    yield return new Wait(5);
    Console.WriteLine("After 5 more seconds " + DateTime.Now);
    yield return new Wait(10);
    Console.WriteLine("After 10 more seconds " + DateTime.Now);

    yield return new Wait(20);
    Console.WriteLine("First coroutine done");
}

static IEnumerator<Wait> PrintEvery10Seconds(ActiveCoroutine first) {
    while (true) {
        yield return new Wait(10);
        Console.WriteLine("The time is " + DateTime.Now);
        if (first.IsFinished) {
            Console.WriteLine("By the way, the first coroutine has finished!");
            Console.WriteLine($"{first.Name} data: {first.MoveNextCount} moves, " +
                              $"{first.TotalMoveNextTime.TotalMilliseconds} total time, " +
                              $"{first.LastMoveNextTime.TotalMilliseconds} last time");
            Environment.Exit(0);
        }
    }
}

static IEnumerator<Wait> EmptyCoroutine() {
    yield break;
}

static IEnumerable<Wait> NestedCoroutine() {
    Console.WriteLine("I'm a coroutine that was started from another coroutine!");
    yield return new Wait(5);
    Console.WriteLine("It's been 5 seconds since a nested coroutine was started, yay!");
}