```

BenchmarkDotNet v0.13.12, Windows 10 (10.0.19045.4651/22H2/2022Update)
11th Gen Intel Core i7-11800H 2.30GHz, 1 CPU, 16 logical and 8 physical cores
  [Host]     : .NET Framework 4.8.1 (4.8.9256.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8.1 (4.8.9256.0), X86 LegacyJIT


```
| Method              | Mean     | Error   | StdDev  |
|-------------------- |---------:|--------:|--------:|
| CoroutinesBenchmark |       NA |      NA |      NA |
| ThreadsBenchmark    | 230.7 μs | 4.33 μs | 7.24 μs |

Benchmarks with issues:
  BenchmarkTests.CoroutinesBenchmark: DefaultJob
