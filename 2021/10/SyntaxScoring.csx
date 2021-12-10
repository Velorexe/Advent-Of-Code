public int GetSyntaxErrors (string line) {
    Stack<char> characters = new Stack<char> ();
    for (int i = 0; i < line.Length; i++) {
        char dequeuedChar;
        switch (line[i]) {
            case '(':
            case '[':
            case '{':
            case '<':
                characters.Push (line[i]);
                break;
            case ')':
                dequeuedChar = characters.Pop ();
                if (dequeuedChar != '(') return 3;
                break;
            case ']':
                dequeuedChar = characters.Pop ();
                if (dequeuedChar != '[') return 57;
                break;
            case '}':
                dequeuedChar = characters.Pop ();
                if (dequeuedChar != '{') return 1197;
                break;
            case '>':
                dequeuedChar = characters.Pop ();
                if (dequeuedChar != '<') return 25137;
                break;
        }
    }

    return 0;
}

public ulong GetAutocomplete (string line) {
    Stack<char> characters = new Stack<char> ();
    for (int i = 0; i < line.Length; i++) {
        switch (line[i]) {
            case '(':
            case '[':
            case '{':
            case '<':
                characters.Push (line[i]);
                break;
            case ')':
            case ']':
            case '}':
            case '>':
                characters.Pop ();
                break;
        }
    }

    ulong points = 0;
    char character;
    while (characters.Count > 0) {
        character = characters.Pop ();
        points *= 5;
        switch (character) {
            case '(':
                points += 1;
                break;
            case '[':
                points += 2;
                break;
            case '{':
                points += 3;
                break;
            case '<':
                points += 4;
                break;
        }
    }

    return points;
}

{
    string[] lines = File.ReadAllLines ("2021/10/syntaxInput.txt");

    int points = 0;
    List<ulong> autocompletePoints = new List<ulong> ();

    for (int i = 0; i < lines.Length; i++) {
        int syntaxScore = GetSyntaxErrors (lines[i]);
        points += syntaxScore;
        if (syntaxScore == 0) autocompletePoints.Add (GetAutocomplete (lines[i]));
    }

    autocompletePoints.Sort ();

    Console.WriteLine ("Syntax score: " + points);
    Console.WriteLine ("Autocomplete score " + autocompletePoints[autocompletePoints.Count / 2]);
}