```

BenchmarkDotNet v0.13.12, Windows 10 (10.0.19045.4780/22H2/2022Update)
11th Gen Intel Core i7-11800H 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.200
  [Host]     : .NET 8.0.2 (8.0.224.6711), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 8.0.2 (8.0.224.6711), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method           | Mean     | Error   | StdDev   | Gen0      | Gen1      | Gen2      | Allocated   |
|----------------- |---------:|--------:|---------:|----------:|----------:|----------:|------------:|
| ProcessBots      | 370.9 ms | 6.90 ms | 17.80 ms | 8000.0000 | 8000.0000 | 2000.0000 | 94503.45 KB |
| ProcessBotsYield | 180.8 ms | 3.55 ms |  6.66 ms |         - |         - |         - |   140.27 KB |
