using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode2015.Day4;

public class Day4
{
    private const string InputKey = "bgvyzdsv";
    
    public static void PartOneLowestPositiveNumber()
    {
        var suffix = 0000000;
        while (true)
        {
            var inputBytes = Encoding.ASCII.GetBytes($"{InputKey}{suffix}");
            var hashBytes = MD5.HashData(inputBytes);
            var hashInHex = Convert.ToHexString(hashBytes).ToUpper();
            
            if (hashInHex.StartsWith("000000"))
            {
                Console.WriteLine(hashInHex);
                Console.WriteLine($"suffix: {suffix}");
                break;
            }     
            suffix++;
        }
    }
}