using System.Text.RegularExpressions;

namespace CsharpDotNetUtils;

public static class TextUtils
{
    public static StreamReader GetStreamReaderFromTextFile(string filePath)
    {
        var streamReader = new StreamReader(filePath);
        return streamReader;
    }

    public static string ReadAllTextToString(string filePath)
    {
        var allText = File.ReadAllText(filePath);
        return allText;
    }

    public static List<int> GetIntegersFromString(string line)
    {
        var digitsPattern = new Regex(@"\d+", RegexOptions.Compiled);
        var digits = digitsPattern.Matches(line);

        return digits.Select(x => int.Parse(x.Value)).ToList();
    }

    public static List<double> GetDoublesFromString(string line)
    {
        var digitsPattern = new Regex(@"\d+", RegexOptions.Compiled);
        var digits = digitsPattern.Matches(line);

        return digits.Select(x => double.Parse(x.Value)).ToList();
    }

    public static List<int> GetIntegersWithSignFromString(string line)
    {
        var digitsWithSignPattern = new Regex(@"-?\d+", RegexOptions.Compiled);
        var digitsWithSign = digitsWithSignPattern.Matches(line);
        
        return digitsWithSign.Select(x => int.Parse(x.Value)).ToList();
    }
}
