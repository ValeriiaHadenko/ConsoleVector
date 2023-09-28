using System.Diagnostics;
using System.Threading;
using ConsoleAppVector;

Dictionary<string, int> GetResultTask(Vector[] vectors1, Vector[] vectors2)
{
    Stopwatch stopwatch = new();

    stopwatch.Start();

    (int, int) result = Vector.AreEqual(vectors1, vectors2);

    stopwatch.Stop();

    return new()
    {
        { "time", (int) stopwatch.ElapsedMilliseconds },
        { "vectorCount", result.Item1 + result.Item2},
        { "countEquals",  result.Item1},
        { "countNotEquals", result.Item2}
    };
}

Dictionary<string, int> MultiThreadedGetResultTask(int threadCount, Vector[] vectors1, Vector[] vectors2)
{
    int totalCountEquals = 0;
    int totalCountNotEquals = 0;
    int vectorCount = vectors1.Length == vectors2.Length ? vectors1.Length : 0;

    Stopwatch stopwatch = new();

    //for (int i = 0; i < vectorCount; i++)
    //{
    //    Console.WriteLine($"vector 1 [{i}] {vectors1[i]} -  vector 2 [{i}] {vectors2[i]}");
    //}


    stopwatch.Start();

    for (int i = 0; i < threadCount; i++)
    {
        int start = i * vectorCount / threadCount;
        int end = ((1 + i) * vectorCount / threadCount) - 1;

        Console.WriteLine(
            $"vector 1 [{start}] {vectors1[start]}, [{end}] {vectors1[end]} - " +
            $"vector 2 [{start}] {vectors2[start]}, [{end}] {vectors2[end]}");

    }

    stopwatch.Stop();



    return new()
    {
        { "time", (int) stopwatch.ElapsedMilliseconds },
        { "vectorCount", totalCountEquals + totalCountNotEquals},
        { "countEquals",  totalCountEquals},
        { "countNotEquals", totalCountNotEquals}
    };
}



void PrintResultTask(Dictionary<string, int> dict, string name = "Task result:")
{
    Console.WriteLine(
        $"{name}" +
        $"\nTime: {dict["time"]} ms " +
        $"\nNumber of vector elements: {dict["vectorCount"]}" +
        $"\nNumber of equal vectors: {dict["countEquals"]}" +
        $"\nNumber of not equal vectors: {dict["countNotEquals"]}"
        );
}




Vector[] vectors1 = Vector.GenerateRandomVectors(500, min: 0, max: 5);
Vector[] vectors2 = Vector.GenerateRandomVectors(500, min: 0, max: 5);

PrintResultTask(GetResultTask(vectors1, vectors2));

PrintResultTask(MultiThreadedGetResultTask(8, vectors1, vectors2));
