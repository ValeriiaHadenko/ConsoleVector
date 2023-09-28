﻿using System.Diagnostics;
using System.Threading;
using ConsoleAppVector;



int size = 40000000;

Vector[] vectors1 = Vector.GenerateRandomVectors(size, min: 0, max: 5);
Vector[] vectors2 = Vector.GenerateRandomVectors(size, min: 0, max: 5);

PrintResultTask(GetResultTask(vectors1, vectors2));
PrintResultTask(MultiThreadedGetResultTask(2, vectors1, vectors2));




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
    int countEquals = 0;
    int countNotEquals = 0;
    int vectorCount = vectors1.Length == vectors2.Length ? vectors1.Length : 0;


    Thread[] threads = new Thread[threadCount];
    Stopwatch stopwatch = new();


    stopwatch.Start();

    for (int i = 0; i < threadCount; i++)
    {
        int start = i * vectorCount / threadCount;
        int end = ((1 + i) * vectorCount / threadCount) - 1;


        threads[i] = new Thread(() =>
        {
            int localCountEquals = 0;
            int localCountNotEquals = 0;

            for (int j = start; j <= end; j++)
            {
                if (Vector.AreEqual(vectors1[j], vectors2[j]))
                    localCountEquals++;
                else
                    localCountNotEquals++;
                
            }

            countEquals += localCountEquals;
            countNotEquals += localCountNotEquals;

        });

        threads[i].Start();

    }

    foreach (Thread thread in threads) thread.Join();

    
    stopwatch.Stop();

    return new()
    {
        { "time", (int) stopwatch.ElapsedMilliseconds },
        { "vectorCount", countEquals + countNotEquals},
        { "countEquals",  countEquals},
        { "countNotEquals", countNotEquals}
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
