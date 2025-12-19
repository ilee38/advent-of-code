using CsharpDotNetUtils;

namespace AdventOfCode2025.Day6;

public class Day6
{
    public static void Part1()
    {
        var problemList = new List<List<int>>();
        var stringRows = new List<string>();
        var operationsRow = new List<string>();
        
        using (var sr = TextUtils.GetStreamReaderFromTextFile(@"Day6/input.txt"))
        {
            while (!sr.EndOfStream)
            {
                stringRows.Add(sr.ReadLine());
            }

            for (var i = 0; i < stringRows.Count - 1; i++)
            {
                problemList.Add(TextUtils.GetIntegersFromString(stringRows[i]));
            }

            foreach (var op in stringRows[^1].Split(' '))
            {
                if (op.Trim().Equals("*") || op.Trim().Equals("+"))
                {
                    operationsRow.Add(op);
                }
            }
        }

        var width = problemList[0].Count;   // assumes all rows have the same number of digits
        long grandTotal = 0;
        for (var i = 0; i < width; i++)
        {
            long total;
            
            if (operationsRow[i].Equals("*"))
            {
                total = 1;
                for (var j = 0; j < problemList.Count; j++)
                {
                    total *= problemList[j][i];
                }
            }
            else
            {
                total = 0;
                for (var j = 0; j < problemList.Count; j++)
                {
                    total += problemList[j][i];
                }
            }
            grandTotal += total;
        }
        
        Console.WriteLine($"[Day6/Part1]: {grandTotal}");
    }
}