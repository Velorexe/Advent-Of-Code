using System;
using System.IO;
using System.Linq;

public struct Vector2 {
    public int X = 0;
    public int Y = 0;

    public Vector2 (int x, int y) {
        this.X = x;
        this.Y = y;
    }

    public static Vector2 operator * (Vector2 a, int b) =>
        new Vector2 (a.X * b, a.Y * b);

    public static Vector2 operator + (Vector2 a, Vector2 b) =>
        new Vector2 (a.X + b.X, a.Y + b.Y);
}

public enum Directions {
    FORWARD,
    BACKWARD,
    DOWN,
    UP
}

public Vector2[] DirectionTranslation = new Vector2[] {
    new Vector2 (1, 0),
    new Vector2 (-1, 0),
    new Vector2 (0, 1),
    new Vector2 (0, -1)
};

#region A
{
    Vector2 currentLocation = new Vector2 ();
    Vector2[] directions = Array.ConvertAll (File.ReadAllLines ("2021/2/directions.txt"), x => {
        string[] input = x.Split (' ');

        Directions direction = (Directions) Enum.Parse (typeof (Directions), input[0].ToUpper ());
        Vector2 translation = DirectionTranslation[(int) direction] * Convert.ToInt32 (input[1]);

        return translation;
    });

    for (int i = 0; i < directions.Length; i++) {
        currentLocation += directions[i];
    }

    Console.WriteLine (currentLocation.X + "" + currentLocation.Y);
    Console.WriteLine ("Multiplied Position is " + (currentLocation.X * currentLocation.Y));
}
#endregion

#region B
{
    Vector2 currentLocation = new Vector2 ();
    int aim = 0;

    foreach (string fileDirection in File.ReadLines ("2021/2/directions.txt")) {
        string[] input = fileDirection.Split (' ');

        Directions direction = (Directions) Enum.Parse (typeof (Directions), input[0].ToUpper ());
        int multiplier = Convert.ToInt32 (input[1]);
        Vector2 translation = DirectionTranslation[(int) direction] * multiplier;

        switch (direction) {
            case Directions.DOWN:
                aim += multiplier;
                break;
            case Directions.UP:
                aim -= multiplier;
                break;
            case Directions.FORWARD:
                currentLocation.Y += aim * multiplier;
                break;
        }

        currentLocation.X += translation.X;
    }

    Console.WriteLine ("Multiplied Position is " + (currentLocation.X * currentLocation.Y));
}
#endregion