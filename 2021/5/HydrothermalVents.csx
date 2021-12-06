using System;
using System.IO;

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

public class Path {
    public readonly Vector2 PointA;
    public readonly Vector2 PointB;

    public readonly Vector2[] Lines;

    public Path (Vector2 pointA, Vector2 pointB) {
        PointA = pointA;
        PointB = pointB;

        List<Vector2> points = new List<Vector2> ();
        //Horizontal
        if (PointA.Y == PointB.Y) {
            if (pointB.X > pointA.X) {
                for (int i = 0; i <= PointB.X - PointA.X; i++) {
                    points.Add (new Vector2 (PointA.X + i, PointB.Y));
                }
            } else {
                for (int i = 0; i <= PointA.X - PointB.X; i++) {
                    points.Add (new Vector2 (PointB.X + i, PointB.Y));
                }
            }
        }
        //Vertical
        if (PointA.X == pointB.X) {
            if (pointB.Y > pointA.Y) {
                for (int i = 0; i <= PointB.Y - PointA.Y; i++) {
                    points.Add (new Vector2 (PointB.X, PointA.Y + i));
                }
            } else {
                for (int i = 0; i <= PointA.Y - PointB.Y; i++) {
                    points.Add (new Vector2 (PointB.X, PointB.Y + i));
                }
            }
        }
        //Diagonal / Exactly 45 Degrees
        if (Math.Abs (PointA.X - PointB.X) == Math.Abs (PointA.Y - PointB.Y)) {
            if (PointB.X > PointA.X) {
                if (PointB.Y > PointA.Y) {
                    for (int i = 0; i <= PointB.Y - PointA.Y; i++) {
                        points.Add (new Vector2 (pointA.X + i, PointA.Y + i));
                    }
                } else {
                    for (int i = 0; i <= PointA.Y - PointB.Y; i++) {
                        points.Add (new Vector2 (pointA.X + i, PointA.Y - i));
                    }
                }
            } else {
                if (PointB.Y > PointA.Y) {
                    for (int i = 0; i <= PointB.Y - PointA.Y; i++) {
                        points.Add (new Vector2 (pointA.X - i, PointA.Y + i));
                    }
                } else {
                    for (int i = 0; i <= PointA.Y - PointB.Y; i++) {
                        points.Add (new Vector2 (pointA.X - i, PointA.Y - i));
                    }
                }
            }
        }

        this.Lines = points.ToArray ();
    }

    public bool Contains (Vector2 coordinate) {
        if (coordinate.X >= PointA.X && coordinate.X <= PointB.X && coordinate.Y == PointA.Y && coordinate.Y == PointB.Y)
            return true;
        else if (coordinate.Y >= PointA.Y && coordinate.Y <= PointB.Y && coordinate.X == PointA.X && coordinate.X == PointB.X)
            return true;
        else if (coordinate.X <= PointA.X && coordinate.X >= PointB.X && coordinate.Y == PointA.Y && coordinate.Y == PointB.Y)
            return true;
        else if (coordinate.Y <= PointA.Y && coordinate.Y >= PointB.Y && coordinate.X == PointA.X && coordinate.X == PointB.X)
            return true;
        return false;
    }
}

#region A + B
{
    string[] lines = File.ReadAllLines ("2021/5/hydrothermalInput.txt");
    Path[] vents = new Path[lines.Length];

    for (int i = 0; i < lines.Length; i++) {
        string[] line = lines[i].Split (' ');

        string[] vector1 = line[0].Split (',');
        string[] vector2 = line[2].Split (',');

        vents[i] = new Path (
            new Vector2 (Convert.ToInt32 (vector1[0]), Convert.ToInt32 (vector1[1])),
            new Vector2 (Convert.ToInt32 (vector2[0]), Convert.ToInt32 (vector2[1])));
    }

    int[, ] thermalField = new int[vents.Max (x => x.PointA.X > x.PointB.X ? x.PointA.X : x.PointB.X) + 1,
        vents.Max (x => x.PointA.Y > x.PointB.Y ? x.PointA.Y : x.PointB.Y) + 1];

    int overlapCounter = 0;

    foreach (Path path in vents) {
        foreach (Vector2 point in path.Lines) {
            thermalField[point.X, point.Y]++;
            if (thermalField[point.X, point.Y] == 2) {
                overlapCounter++;
            }
        }
    }

    Console.WriteLine ("Overlapping " + overlapCounter + " times.");
}
#endregion