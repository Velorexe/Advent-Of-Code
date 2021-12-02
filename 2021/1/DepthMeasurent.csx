using System.IO;

#region A

{
    int[] input = { };
    int increaseAmount = 0;

    input = Array.ConvertAll (File.ReadAllLines ("2021/1/depthMeasurents.txt"), x => int.Parse (x));

    for (int i = 0; i < input.Length - 1; i++) {
        if (input[i] < input[i + 1])
            increaseAmount++;
    }

    Console.WriteLine ("Depth has increased " + increaseAmount + " times.");
}

#endregion

#region B

{
    int[] input = { };
    int increaseAmount = 0;

    input = Array.ConvertAll (File.ReadAllLines ("2021/1/depthMeasurents.txt"), x => int.Parse (x));

    for (int i = 0; i < input.Length - 3; i++) {
        if(input[i] + input[i + 1] + input[i + 2] < input[i + 1] + input[i + 2] + input[i + 3])
            increaseAmount++;
    }

    Console.WriteLine ("Depth has increased " + increaseAmount + " times.");
}

#endregion