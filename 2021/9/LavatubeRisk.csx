public struct Vector2 {
    public int X;
    public int Y;

    public Vector2(int x, int y) {
        X = x;
        Y = y;
    }
}

public bool[,] MarkBasin(Vector2 startingPoint, bool[,] basinField, int[][] heightMaps, ref int count) {
    if(!basinField[startingPoint.X, startingPoint.Y] && heightMaps[startingPoint.X][startingPoint.Y] != 9) {
        basinField[startingPoint.X, startingPoint.Y] = true;
        count++;

        if(startingPoint.X != basinField.GetLength(0) - 1) {
            basinField = MarkBasin(new Vector2(startingPoint.X + 1, startingPoint.Y), basinField, heightMaps, ref count);
        }
        if(startingPoint.X != 0) {
            basinField = MarkBasin(new Vector2(startingPoint.X - 1, startingPoint.Y), basinField, heightMaps, ref count);
        }
        if(startingPoint.Y != 0) {
            basinField = MarkBasin(new Vector2(startingPoint.X, startingPoint.Y - 1), basinField, heightMaps, ref count);
        }
        if(startingPoint.Y != basinField.GetLength(1) - 1) {
            basinField = MarkBasin(new Vector2(startingPoint.X, startingPoint.Y + 1), basinField, heightMaps, ref count);
        }
    }

    return basinField;
}

{
    string[] lines = File.ReadAllLines ("2021/9/heightmapInput.txt");
    int[][] heightMaps = Array.ConvertAll (lines, x => {
        char[] characters = x.ToArray ();
        int[] result = new int[characters.Length];
        for (int i = 0; i < result.Length; i++) {
            result[i] = Convert.ToInt32 (characters[i].ToString ());
        }
        return result;
    });

    //Make one long string instead of small sections
    List<int> lowestPoints = new List<int> ();

    List<Vector2> lowPointCoordinates = new List<Vector2>();
    bool[,] field = new bool[heightMaps.Length, heightMaps[0].Length];

    for (int i = 0; i < heightMaps.Length; i++) {
        for (int j = 0; j < heightMaps[i].Length; j++) {
            bool lowPoint = false;
            if(i == 0) 
                lowPoint = (heightMaps[i][j] < heightMaps[i + 1][j]);
            else if(i == heightMaps.Length - 1)
                lowPoint = (heightMaps[i][j] < heightMaps[i - 1][j]);
            else 
                lowPoint = (heightMaps[i][j] < heightMaps[i - 1][j] && heightMaps[i][j] < heightMaps[i + 1][j]);

            if(!lowPoint)
                continue;

            if(j == 0)
                lowPoint = (heightMaps[i][j] < heightMaps[i][j + 1]);
            else if(j == heightMaps[i].Length - 1)
                lowPoint = (heightMaps[i][j] < heightMaps[i][j - 1]);
            else 
                lowPoint = (heightMaps[i][j] < heightMaps[i][j + 1] && heightMaps[i][j] < heightMaps[i][j - 1]);

            if(lowPoint) {
                lowestPoints.Add(heightMaps[i][j] + 1);
                lowPointCoordinates.Add(new Vector2(i, j));
            }
        }
    }

    int[] basins = new int[lowPointCoordinates.Count]; 
    for(int i = 0; i < lowPointCoordinates.Count; i++) {
        int amount = 0;
        field = MarkBasin(lowPointCoordinates[i], field, heightMaps, ref amount);
        basins[i] = amount;
    }

    basins = basins.OrderByDescending(x => x).Take(3).ToArray();

    int basinCount = basins[0];
    for(int i = 1; i < basins.Length; i++) {
        basinCount *= basins[i];
    }

    Console.WriteLine ("Risk is: " + lowestPoints.Sum (x => x));
    Console.WriteLine("Basinsizes is: " + basinCount);
}