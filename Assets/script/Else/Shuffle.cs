using System;
using System.Collections.Generic;

public static class Shuffle
{
    private static Random rand = new Random();

    public static void Shuffle_Run<T>(IList<T> values)
    {
        for(int i = values.Count - 1; i>0; i--)
        {
            int k = rand.Next(i + 1);
            T value = values[k];
            values[k] = values[i];
            values[i] = value;
        }
    }
}
