#region A
{
    int[] crabPositions = Array.ConvertAll(File.ReadAllLines("2021/7/crabPositions.txt")[0].Split(','), x => Convert.ToInt32(x));

    int fuelCost = int.MaxValue;
    int bestCase = 0;

    int largestTravelCase = crabPositions.Max() - crabPositions.Min();

    for(int i = crabPositions.Min(); i < largestTravelCase + 1; i++) {
        int tempCost = 0;
        foreach(int crab in crabPositions) {
            tempCost += Math.Abs(crab - i);
        }

        if(tempCost < fuelCost) {
            fuelCost = tempCost;
            bestCase = i;
        }
    }

    Console.WriteLine("The cheapest horizontal position is " + bestCase + " with " + fuelCost + " fuel cost.");
}
#endregion

#region B
{
    int[] crabPositions = Array.ConvertAll(File.ReadAllLines("2021/7/crabPositions.txt")[0].Split(','), x => Convert.ToInt32(x));

    int fuelCost = int.MaxValue;
    int bestCase = 0;

    int largestTravelCase = crabPositions.Max() - crabPositions.Min();

    for(int i = crabPositions.Min(); i < largestTravelCase + 1; i++) {
        int tempCost = 0;
        foreach(int crab in crabPositions) {
            int cost = Math.Abs(crab - i);
            tempCost += (int)(0.5 * (cost * cost) + 0.5 * cost);
        }

        if(tempCost < fuelCost) {
            fuelCost = tempCost;
            bestCase = i;
        }
    }

    Console.WriteLine("The cheapest horizontal position is " + bestCase + " with " + fuelCost + " fuel cost.");
}
#endregion