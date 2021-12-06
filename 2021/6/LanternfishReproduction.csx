using System.IO;

{
    string line = File.ReadAllLines ("F:/Projects/AdventOfCode/2021/6/lanternfishData.txt") [0];
    int[] lanternfishData = Array.ConvertAll (line.Split (','), x => Convert.ToInt32 (x));

    int amountOfDays = 256;

    int startofTime = 8;

    ulong[] lanternfishes = new ulong[startofTime + 1];
    foreach (int i in lanternfishData)
        lanternfishes[i]++;

    for (int i = 0; i < amountOfDays; i++) {
        ulong newFishesBuffer = lanternfishes[0];

        for (int j = 0; j < lanternfishes.Length - 1; j++)
            lanternfishes[j] = lanternfishes[j + 1];

        lanternfishes[8] = newFishesBuffer;
        lanternfishes[6] += newFishesBuffer;
    }

    ulong count = 0;
    for (int i = 0; i < lanternfishes.Length; i++)
        count += (ulong) lanternfishes[i];

    Console.WriteLine ("Amount of Lanternfishes after " + amountOfDays + " days " + count);
}