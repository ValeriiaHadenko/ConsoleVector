﻿namespace ConsoleAppVector;

public class Vector
{
    private double x;
    private double y;

    public double X { get { return x; } }
    public double Y { get { return y; } }

    public string VEC { get => ToString(); }
    public double Length { get => GetLength(); }

    public static Vector XAxis { get => new(1, 0); }
    public static Vector YAxis { get => new(0, 1); }

    public Vector()
    {
        x = 0;
        y = 0;
    }
    public Vector(double x, double y)
    {
        this.x = x;
        this.y = y;
    }
    public Vector(Vector other)
    {
        x = other.x;
        y = other.y;
    }

    public double GetLength() => Math.Sqrt(x * x + y * y);

    public static bool AreEqual(Vector v1, Vector v2) => v1.Equals(v2);
    public static (int, int) AreEqual(Vector[] v1, Vector[] v2)
    {
        int vectorCount = v1.Length == v2.Length ? v1.Length : 0;

        int countEquals = 0;
        int countNotEquals = 0;

        for (int i = 0; i < vectorCount; i++)
        {
            if (v1[i].Equals(v2[i]))
                countEquals++;
            else
                countNotEquals++;
        }

        return (countEquals, countNotEquals);
    }

    public static Vector[] GenerateRandomVectors(int count)
    {
        Random random = new();
        Vector[] vectors = new Vector[count];

        for (int i = 0; i < count; i++)
        {
            vectors[i] = new Vector(random.Next(), random.Next());
        }

        return vectors;
    }
    public static Vector[] GenerateRandomVectors(int count, int max)
    {
        Random random = new();
        Vector[] vectors = new Vector[count];

        for (int i = 0; i < count; i++)
        {
            vectors[i] = new Vector(random.Next(0, max), random.Next(0, max));
        }

        return vectors;
    }
    public static Vector[] GenerateRandomVectors(int count, int min, int max)
    {
        Random random = new();
        Vector[] vectors = new Vector[count];

        for (int i = 0; i < count; i++)
        {
            vectors[i] = new Vector(random.Next(min, max), random.Next(min, max));
        }

        return vectors;
    }

    public override string ToString() => $"({x}, {y})";
    public override bool Equals(object? obj)
    {
        if (obj is Vector vector)
        {
            return x == vector.x && y == vector.y;
        }

        return false;
    }
    public override int GetHashCode() => base.GetHashCode();

}

