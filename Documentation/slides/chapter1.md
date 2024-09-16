---
layout: image-right
---

# Session content
- Benefits and Practical Applications of Coroutines
- Yield, IEnumerable, IAsyncEnumerable
- Suspended-Resumed and Context Switching
- Coroutines Nuget Packages and Unity
- Conclusion
---
layout: my-two-cols
---

::top::

# Benefits

#### **Efficiency in resource management**

- Coroutines allow concurrent tasks to be performed without the overhead of operating system threads. By not requiring multiple threads, they avoid excessive resource consumption such as memory and CPU cycles.
<br/>
#### **Ease of handling asynchronous I/O**

- When a task involves I/O operations such as file reading or network access, coroutines can yield control to other tasks while waiting, optimizing time usage and allowing other operations to progress concurrently.
<br/>
#### **Latency reduction**

- In applications where multiple tasks need to be performed simultaneously (for example, real-time event processing), coroutines allow tasks to be processed as soon as they are ready, reducing latency.

---
layout: my-two-cols
---

::top::

# Benefits

#### **Improvement in code readability**

- Unlike other concurrency approaches, such as callbacks or promises (futures), coroutines can make asynchronous code look and behave like synchronous code, making it easier to read and maintain.
<br/>

#### **Precise control of the flow of execution**

- Allow pausing and resuming execution at specific points in the code, which is useful for implementing generators, simulations, and game programming.

---
layout: my-two-cols
---

::top::

# Practical Applications

#### **Real-time event processing**

- In applications like video games, simulations, or graphical interfaces, where processing must be fast and responsive, coroutines allow switching between different tasks, such as updating game logic and rendering graphics, without noticeable delays.

#### **Complex flow control systems**

- In systems requiring sophisticated control of execution flow, such as physical system simulations, finance, or artificial intelligence programming, coroutines allow the process to be divided into stages that can run in parallel or in turns.

---
layout: my-two-cols
---

::top::

# Yield, IEnumerable and IAsyncEnumerable

**yield**
Introduced in C# with version 2.0 of the language and .NET Framework 2.0 in 2005

used in the implementation of iterators, which simplifies the creation of collections that can be iterated sequentially

- simplified iterators
- sequence generation
- memory and performance

---
layout: my-two-cols
---

::top::

# Yield, IEnumerable and IAsyncEnumerable

**IEnumerable**
facilitates iteration over collections of objects. 
<br/><br/>It provides a contract for sequential access to the elements of a collection.

**IAsyncEnumerable**
facilitates asynchronous iteration over collections of objects, allowing for efficient and non-blocking data processing. 
<br/><br/>Introduced in C# 8.0 and .NET Core 3.0, it enhances the handling of asynchronous I/O operations.
---
layout: my-two-cols
---

::top::

# Yield, IEnumerable and IAsyncEnumerable

::left::
**yield** with **IEnumerable** (sync)
```csharp
public IEnumerable<int> GenerateNumbers()
{
    for (int i = 0; i < 5; i++)
    {
        yield return i;
    }
}

// Usage
foreach (var number in GenerateNumbers())
{
    Console.WriteLine(number);
}
```
::right::
**yield** with **IAsyncEnumerable** (async)
```csharp
public async IAsyncEnumerable<int> GenerateNumbersAsync()
{
    for (int i = 0; i < 5; i++)
    {
        yield return i;
        await Task.Delay(100);
    }
}

// Usage
await foreach (var number in GenerateNumbersAsync())
{
    Console.WriteLine(number);
}
```

---
layout: my-two-cols
---

::top::

# Yield and IEnumerable

<div style="text-align: center;margin:160px;font-size: 30px;">
    DEMO UND BENCHMARK
</div>

---
layout: my-two-cols
---

::top::

# Yield and IEnumerable

Results
<img src="/img/ProcessBots.png" />

---
layout: my-two-cols
---

::top::

# Yield and IAsyncEnumerable

<div style="text-align: center;margin:160px;font-size: 30px;">
    DEMO UND BENCHMARK
</div>

---
layout: my-two-cols
---

::top::

# Yield and IAsyncEnumerable

Results
<img src="/img/ProcessBotsAsync.png" />

---
layout: my-two-cols
---

::top::

# Suspended-Resumed Approach

- They operate on the same thread, and suspension and resumption are managed within the code itself. They do not involve an operating system context switch (thread), making them lighter in terms of performance.
<br/>
<br/>
---
layout: my-two-cols
---

::top::

# Suspended-Resumed Approach

<div style="text-align: center;margin:140px;font-size: 30px;">
    DEMO
</div>

---
layout: my-two-cols
---

::top::

# Coroutine Nuget Package and Unity


<table><tr><td><img src="/img/Coroutine.png" style="height:45px;width:45px;" /></td>
<td>Coroutine is a simple implementation of Unity's Coroutines to be used for any C# project</td>
</tr>
</table>

Characteristics

There are two predefined ways to pause a coroutine:

    Waiting for a certain amount of seconds to have passed
    Waiting for a certain custom event to occur

Additionally, Coroutine provides the following features:

    Creation of custom events to wait for
    No multi-threading, which allows for any kind of process to be executed in a coroutine, including rendering
    Thread-safety, which allows for coroutines to be started from different threads

