using System.Diagnostics;
using System.Threading;
using ConsoleAppVector;

Dictionary<string, int> GetResultTask(int vectorCount)
{
    int countEquals = 0;
    int countNotEquals = 0;

    Stopwatch stopwatch = new();

    Dictionary<string, int> task = new();

    Vector[] vectors1 = Vector.GenerateRandomVectors(vectorCount, min: 0, max: 5);
    Vector[] vectors2 = Vector.GenerateRandomVectors(vectorCount, min: 0, max: 5);

    stopwatch.Start();

    for (int i = 0; i < vectorCount; i++)
    {
        if (Vector.AreEqual(vectors1[i], vectors2[i]))
            countEquals++;
        else
            countNotEquals++;
    }

    stopwatch.Stop();

    task.Add("time", (int) stopwatch.ElapsedMilliseconds);
    task.Add("vectorCount", vectorCount);
    task.Add("countEquals", countEquals);
    task.Add("countNotEquals", countNotEquals);

    return task;
}

Dictionary<string, int> MultiThreadedGetResultTask(int vectorCount)
{
    int threadCount = Environment.ProcessorCount;

    int countEquals = 0;
    int countNotEquals = 0;

    Stopwatch stopwatch = new();

    Dictionary<string, int> task = new();

    Vector[] vectors1 = Vector.GenerateRandomVectors(vectorCount, min: 0, max: 5);
    Vector[] vectors2 = Vector.GenerateRandomVectors(vectorCount, min: 0, max: 5);


    object locker = new();
    stopwatch.Start();

    for (int i = 0; i < threadCount; i++)
    {
        Thread myThread = new(() =>
        {
            lock (locker)
            {
                countEquals = 0;
                countNotEquals = 0;

                for (int i = 0; i < vectorCount; i++)
                {
                    if (Vector.AreEqual(vectors1[i], vectors2[i]))
                        countEquals++;
                    else
                        countNotEquals++;

                }
            }
        });
        myThread.Start();
    }



    stopwatch.Stop();



    task.Add("time", (int)stopwatch.ElapsedMilliseconds);
    task.Add("vectorCount", vectorCount);
    task.Add("countEquals", countEquals);
    task.Add("countNotEquals", countNotEquals);

    return task;
}



void PrintResultTask(Dictionary<string, int> dict, string name = "Task result:")
{
    Console.WriteLine(
        $"{name}" +
        $"\nTime: {dict["time"]} ms " +
        $"\nNumber of vector elements: {dict["vectorCount"]}" +
        $"\nNumber of equal vectors: {dict["countEquals"]}" +
        $"\nNumber of not equal vectors: {dict["countNotEquals"]}\n"
        );
}




//PrintResultTask(GetResultTask(1000000));
//PrintResultTask(GetResultTask(2000000));

//PrintResultTask(MultiThreadedGetResultTask(1000000));
PrintResultTask(MultiThreadedGetResultTask(20000));
