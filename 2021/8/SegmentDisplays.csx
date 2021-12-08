string Sort (string input) {
    char[] characters = input.ToArray ();
    Array.Sort (characters);
    return new string (characters);
}

bool ContainsAllCharacters (string source, string matcher) {
    bool contains = false;
    foreach (char c in matcher) {
        contains = source.Contains (c);
        if (!contains)
            return false;
    }
    return contains;
}

{
    string[] lines = File.ReadAllLines ("2021/8/segmentInput.txt");

    string[][] delimiters = new string[lines.Length][];
    string[][] segmentInputs = new string[lines.Length][];

    for (int i = 0; i < lines.Length; i++) {
        string[] split = lines[i].Split ('|');

        delimiters[i] = split[0].Split (' ').OrderBy (x => x.Length).Where (x => !String.IsNullOrEmpty (x)).ToArray ();
        segmentInputs[i] = split[1].Split (' ').Where (x => !String.IsNullOrEmpty (x)).ToArray ();
    }

    //int[] positionMatching = new int[] { 0, 3, 6, 7 };

    Int64 count = 0;

    for (int i = 0; i < delimiters.Length; i++) {
        string[] segmentNumbers = new string[] { string.Empty, Sort (delimiters[i][0]), string.Empty, string.Empty, Sort (delimiters[i][2]), string.Empty, string.Empty, Sort (delimiters[i][1]), Sort (delimiters[i][9]), string.Empty };

        string[] lengthSix = delimiters[i].Where (x => x.Length == 6).Select (x => Sort (x)).ToArray ();
        string[] lengthFive = delimiters[i].Where (x => x.Length == 5).Select (x => Sort (x)).ToArray ();

        List<int> zeroAndSix = new List<int> ();
        int[] differences = new int[3];

        for (int j = 0; j < lengthSix.Length; j++) {
            if (ContainsAllCharacters (lengthSix[j], segmentNumbers[4]))
                segmentNumbers[9] = Sort (lengthSix[j]);
            else
                zeroAndSix.Add (j);
        }

        foreach (char c in segmentNumbers[4]) {
            for (int j = 0; j < lengthFive.Length; j++) {
                differences[j] += lengthFive[j].Contains (c) ? 1 : 0;
            }
        }

        //0 and 6
        if (lengthSix[zeroAndSix[0]].Contains (segmentNumbers[1][0]) && lengthSix[zeroAndSix[0]].Contains (segmentNumbers[1][1])) {
            segmentNumbers[0] = Sort (lengthSix[zeroAndSix[0]]);
            segmentNumbers[6] = Sort (lengthSix[zeroAndSix[1]]);
        } else {
            segmentNumbers[0] = Sort (lengthSix[zeroAndSix[1]]);
            segmentNumbers[6] = Sort (lengthSix[zeroAndSix[0]]);
        }

        //2 only 2 positions incommon with 4
        segmentNumbers[2] = Sort (lengthFive[Array.IndexOf (differences, differences.Min())]);
        lengthFive = lengthFive.Where (x => x != segmentNumbers[2]).ToArray ();
        
        char compare = segmentNumbers[8].Except(segmentNumbers[6]).ToArray()[0];
        char sixNineDifferentiator = segmentNumbers[8].Except(segmentNumbers[6]).ToArray()[0];//segmentNumbers[8][string.Compare (segmentNumbers[8], segmentNumbers[6])];

        if (lengthFive[0].Contains (sixNineDifferentiator)) {
            segmentNumbers[3] = Sort (lengthFive[0]);
            segmentNumbers[5] = Sort (lengthFive[1]);
        } else {
            segmentNumbers[3] = Sort (lengthFive[1]);
            segmentNumbers[5] = Sort (lengthFive[0]);
        }

        int lineCount = 0;

        for (int j = 0; j < segmentInputs[i].Length; j++) {
            int result = (int) (Array.IndexOf (segmentNumbers, Sort (segmentInputs[i][j])) * (1000 / (int) Math.Pow (10, j)));
            count += result;
            lineCount += result;
        }

        Console.WriteLine(string.Join(" ", segmentInputs[i]) + ": " + lineCount);
    }

    int simpleCount = 0;
    foreach (string[] input in segmentInputs) {
        simpleCount += input.Where (x => x.Length == 2 || x.Length == 3 || x.Length == 4 || x.Length == 7).Count ();
    }

    Console.WriteLine ("1, 4, 7 and 8 appear " + simpleCount + " times.");
    Console.WriteLine ("Total sum of " + count);
}