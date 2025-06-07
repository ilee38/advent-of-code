using System.Text;

namespace AdventOfCode2015.Day10;

public class Day10
{
    private const string Input = "1113222113";
    private const int PartOneRepeats = 40;
    private const int PartTwoRepeats = 50;

    public void PartOneAndTwo()
    {
        var line = Input.ToArray();

        for (var i = 0; i < PartTwoRepeats; i++)
        {
            var nextInput = new StringBuilder();
            var count = 1;
            var symbol = line[0];
            for (var j = 1; j < line.Length; j++)
            {
                var nextSymbol = line[j];
                if (nextSymbol == symbol)
                {
                    count++;
                }
                else
                {
                    nextInput.Append($"{count}{symbol}");
                
                    // Reset counter
                    count = 1;
                }
                symbol = nextSymbol;
            }
        
            // Append the final count and symbol
            nextInput.Append($"{count}{symbol}");
            line = nextInput.ToString().ToArray();
            
            Console.WriteLine($"Sequence length: {nextInput.Length}");
        }
    }
}