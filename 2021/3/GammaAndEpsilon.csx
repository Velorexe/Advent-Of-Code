using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#region A
{
    string[] lines = File.ReadAllLines ("2021/3/binaryInput.txt");

    int gamma = 0;
    int epsilon = 0;

    int[] oneCount = new int[lines[0].Length];

    for (int i = 0; i < lines.Length; i++) {
        for (int j = 0; j < lines[i].Length; j++) {
            oneCount[j] += lines[i][j] == '0' ? 0 : 1;
        }
    }

    for (int i = 0; i < oneCount.Length; i++) {
        if (oneCount[i] > lines.Length / 2) {
            gamma += (int) Math.Pow (2, oneCount.Length - i - 1);
        } else {
            epsilon += (int) Math.Pow (2, oneCount.Length - i - 1);
        }
    }

    Console.WriteLine ("Gamma is " + gamma);
    Console.WriteLine ("Epsilon is " + epsilon);

    Console.WriteLine ("Gamma and Epsilon is: " + gamma * epsilon);
}
#endregion


#region B
{
    string[] lines = File.ReadAllLines ("2021/3/binaryInput.txt");

    int[] counter = new int[lines[0].Length];
    for (int i = 0; i < lines[0].Length; i++) {
        for (int j = 0; j < lines.Length; j++) {
            counter[i] += lines[j][i] == '1' ? 1 : 0;
        }
    }

    List<int> oxygen = Enumerable.Range (0, lines.Length).Where (x => lines[x][0] == (counter[0] > lines.Length / 2 ? '1' : '0')).ToList ();
    List<int> co2 = Enumerable.Range (0, lines.Length).Where (x => lines[x][0] == (counter[0] > lines.Length / 2 ? '0' : '1')).ToList ();

    for (int i = 1; i < counter.Length; i++) {

        bool isPositive;

        if (oxygen.Count > 1) {
            counter = new int[lines[0].Length];
            for (int j = 0; j < counter.Length; j++) {
                for (int k = 0; k < oxygen.Count; k++) {
                    counter[j] += lines[oxygen[k]][j] == '1' ? 1 : 0;
                }
            }
            isPositive = counter[i] + (oxygen.Count % 2 == 0 ? 1 : 0) > oxygen.Count / 2;
            if (oxygen.Count == 2 && counter[i] == 1) {
                oxygen = oxygen.Where(x => lines[x][i] == '1').ToList ();
            } else {
                oxygen = oxygen.Where(x => lines[x][i] == (isPositive ? '1' : '0')).ToList ();
            }
        }
        if (co2.Count > 1) {
            counter = new int[lines[0].Length];
            for (int j = 0; j < counter.Length; j++) {
                for (int k = 0; k < co2.Count; k++) {
                    counter[j] += lines[co2[k]][j] == '1' ? 1 : 0;
                }
            }
            isPositive = counter[i] + (co2.Count % 2 == 0 ? 1 : 0) > co2.Count / 2;
            if (co2.Count == 2 && counter[1] == 1) {
                co2 = co2.Where(x => lines[x][i] == '0').ToList ();
            } else {
                co2 = co2.Where(x => lines[x][i] == (!isPositive ? '1' : '0')).ToList ();
            }
        }
    }

    Console.WriteLine ("Oxygen is " + lines[oxygen[0]]);
    Console.WriteLine ("Co2 is " + lines[co2[0]]);

    Console.WriteLine ("Lifesupport " + Convert.ToInt32 (lines[oxygen[0]], 2) * Convert.ToInt32 (lines[co2[0]], 2));
}
#endregion