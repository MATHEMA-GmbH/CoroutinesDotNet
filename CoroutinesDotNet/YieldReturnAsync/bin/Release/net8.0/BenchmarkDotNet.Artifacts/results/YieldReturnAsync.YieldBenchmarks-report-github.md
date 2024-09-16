```

BenchmarkDotNet v0.13.12, Windows 10 (10.0.19045.4780/22H2/2022Update)
11th Gen Intel Core i7-11800H 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.200
  [Host]     : .NET 8.0.2 (8.0.224.6711), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 8.0.2 (8.0.224.6711), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method                | Mean       | Error    | StdDev   | Median   | Gen0      | Gen1      | Gen2      | Allocated   |
|---------------------- |-----------:|---------:|---------:|---------:|----------:|----------:|----------:|------------:|
| ProcessBotsAsync      | 1,000.2 ms | 19.82 ms | 41.38 ms | 983.0 ms | 8000.0000 | 8000.0000 | 2000.0000 | 94552.78 KB |
| ProcessBotsYieldAsync |   164.5 ms |  3.19 ms |  5.06 ms | 165.5 ms |         - |         - |         - |   141.39 KB |
