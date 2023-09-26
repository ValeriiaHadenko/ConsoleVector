using System.Diagnostics;
using ConsoleAppVector;

Dictionary<string, int> GetResultTask(int vectorCount)
{
    int countEquals = 0;
    int countNotEquals = 0;

    Stopwatch stopwatch = new Stopwatch();

    Dictionary<string, int> task = new Dictionary<string, int>();

    Vector[] vectors1 = Vector.GenerateRandomVectors(vectorCount, min: 0, max: 5);
    Vector[] vectors2 = Vector.GenerateRandomVectors(vectorCount, min: 0, max: 5);

    stopwatch.Start();

    for (int i = 0; i < vectorCount; i++)
    {
        if (Vector.AreEqual(vectors1[i], vectors2[i])) countEquals++;
        else countNotEquals++;
    }

    stopwatch.Stop();

    task.Add("time", (int) stopwatch.ElapsedMilliseconds);
    task.Add("vectorCount", vectorCount);
    task.Add("countEquals", countEquals);
    task.Add("countNotEquals", countNotEquals);

    return task;
}


void PrintResultTask(Dictionary<string, int> dict)
{
    Console.WriteLine(
        $"Time: {dict["time"]} ms " +
        $"\nNumber of vector elements: {dict["vectorCount"]}" +
        $"\nNumber of equal vectors: {dict["countEquals"]}" +
        $"\nNumber of not equal vectors: {dict["countNotEquals"]}\n"
        );
}



PrintResultTask(GetResultTask(10000));
PrintResultTask(GetResultTask(20000));
PrintResultTask(GetResultTask(20000));
PrintResultTask(GetResultTask(00000));