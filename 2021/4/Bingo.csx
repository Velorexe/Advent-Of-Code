using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

private bool HasBingo (int[][] bingoSheet, int[] bingoHits, int bingoIndex) {
    int[] cutBingoHits = bingoHits.Take (bingoIndex + 1).ToArray ();

    for (int i = 0; i < bingoSheet.Length; i++) {
        for (int j = 0; j < bingoSheet[i].Length; j++) {
            if (!cutBingoHits.Contains (bingoSheet[i][j]))
                break;
            if (j == bingoSheet[j].Length - 1)
                return true;
        }
    }

    for (int i = 0; i < bingoSheet.Length; i++) {
        for (int j = 0; j < bingoSheet[i].Length; j++) {
            if (!cutBingoHits.Contains (bingoSheet[j][i]))
                break;
            if (j == bingoSheet[j].Length - 1)
                return true;
        }
    }

    return false;
}

private int GetBingoSum (int[][] bingoSheet, int[] bingoHits, int bingoIndex) {
    int[] cutBingoHits = bingoHits.Take (bingoIndex + 1).ToArray ();
    int count = 0;

    for (int i = 0; i < bingoSheet.Length; i++) {
        for (int j = 0; j < bingoSheet[i].Length; j++) {
            if (!cutBingoHits.Contains (bingoSheet[i][j]))
                count += bingoSheet[i][j];
        }
    }

    return count;
}

#region A
{
    string[] lines = File.ReadAllLines ("2021/4/bingoInput.txt");

    int[] bingoHits = Array.ConvertAll (lines[0].Split (','), x => Convert.ToInt32 (x));

    List<int[][]> bingoSheets = new List<int[][]> ();

    for (int i = 1; i < lines.Length; i++) {
        if (string.IsNullOrEmpty (lines[i])) {
            int[][] bingoSheet = new int[5][];
            for (int j = 0; j < 5; j++) {
                bingoSheet[j] = Array.ConvertAll (lines[i + 1 + j].Split (' ').Where (x => !string.IsNullOrEmpty (x)).ToArray (), x => Convert.ToInt32 (x));
            }
            bingoSheets.Add (bingoSheet);
        }
    }

    int winningIndex = 0;
    int bingoIndex = 0;

    void Loop () {
        for (int i = 0; i < bingoHits.Length; i++) {
            for (int j = 0; j < bingoSheets.Count; j++) {
                if (HasBingo (bingoSheets[j], bingoHits, i)) {
                    winningIndex = j;
                    bingoIndex = i;
                    return;
                }
            }
        }
    }

    Loop ();

    int winningSum = GetBingoSum (bingoSheets[winningIndex], bingoHits, bingoIndex);

    Console.WriteLine (winningIndex);
    Console.WriteLine (winningSum);

    Console.WriteLine ("Winning Sum is " + winningSum * bingoHits[bingoIndex]);
}
#endregion

#region B
{
    string[] lines = File.ReadAllLines ("2021/4/bingoInput.txt");

    int[] bingoHits = Array.ConvertAll (lines[0].Split (','), x => Convert.ToInt32 (x));

    List<int[][]> bingoSheets = new List<int[][]> ();

    for (int i = 1; i < lines.Length; i++) {
        if (string.IsNullOrEmpty (lines[i])) {
            int[][] bingoSheet = new int[5][];
            for (int j = 0; j < 5; j++) {
                bingoSheet[j] = Array.ConvertAll (lines[i + 1 + j].Split (' ').Where (x => !string.IsNullOrEmpty (x)).ToArray (), x => Convert.ToInt32 (x));
            }
            bingoSheets.Add (bingoSheet);
        }
    }

    int winningIndex = 0;
    int bingoIndex = 0;

    int wonAmount = 0;
    List<int> wonIndexes = new List<int>();

    void Loop () {
        for (int i = 0; i < bingoHits.Length; i++) {
            for (int j = 0; j < bingoSheets.Count; j++) {
                if (!wonIndexes.Contains(j) && HasBingo (bingoSheets[j], bingoHits, i)) {
                    wonAmount++;
                    wonIndexes.Add(j);
                    if (wonAmount == bingoSheets.Count) {
                        winningIndex = j;
                        bingoIndex = i;
                        return;
                    }
                }
            }
        }
    }

    Loop ();

    int winningSum = GetBingoSum (bingoSheets[winningIndex], bingoHits, bingoIndex);

    Console.WriteLine (winningIndex);
    Console.WriteLine (winningSum);
    Console.WriteLine(bingoHits[bingoIndex]);

    Console.WriteLine ("Winning Sum is " + winningSum * bingoHits[bingoIndex]);
}
#endregion