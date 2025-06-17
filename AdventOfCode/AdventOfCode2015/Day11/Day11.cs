using System.Text;

namespace AdventOfCode2015.Day11;

public class Day11
{
    // TODO: fix logic (possibly the password validation method) to get right answer.
    
    private const string InitialPassword = "hepxcrrq";
    private int _passwordIndex = InitialPassword.Length - 1;
    private bool _wrapped = false;

    public void PartOne()
    {
        var newPasswordCharacters = InitialPassword.ToCharArray();

        while (true)
        {
            // Generate next password
            var newPassword = GenerateNextPassword(newPasswordCharacters, _passwordIndex);
        
            // Validate password. If valid, return new password     
            if (IsValidPassword(newPassword))
            {
                break;
            }
            
            // If letters wrapped around, reset index
            if (_wrapped)
            {
                _passwordIndex = InitialPassword.Length - 1;
                _wrapped = false;
            }
        }
        
        Console.WriteLine($"[Day 11/Part 1]: {new string(newPasswordCharacters)}");
    }

    private string GenerateNextPassword(char[] passwordCharacters, int startingIndex)
    {
        // Get char's ASCII code: values for lower case letters are 97 ('a') thru 122 ('z')
        int currentChar =  passwordCharacters[startingIndex];
        var nextChar = (currentChar + 1) % 123;

        if (nextChar == 0)
        {
            // Add 97 to make it go from z to a
            nextChar += 97;
            passwordCharacters[startingIndex] = (char)nextChar;
            startingIndex = startingIndex == 0 ? 7 : startingIndex - 1;
            _wrapped = true;
            
            // Recursively increase the next char to the left, stop when a char doesn't wrap around
            GenerateNextPassword(passwordCharacters, startingIndex);
        }
        else
        {
            passwordCharacters[startingIndex] = (char)nextChar;
        }
        _passwordIndex = startingIndex; 
        return new string(passwordCharacters);
    }

    private static bool IsValidPassword(string password)
    {
        var isValid = false;
        
        // First check
        if (password.Contains('i') || password.Contains('o') || password.Contains('l'))
        {
            return false;
        }
        
        // Second check: Validate increasing straight of at least three letters
        var characters = password.ToCharArray();
        var p1 = 0;
        var p2 = 1;
        int char1 = characters[p1];
        int char2 = characters[p2];
        
        while (p2 < characters.Length - 1) 
        {
            if (char2 - char1 == 1)
            {
                p1++;
                p2++;
                char1 = characters[p1];
                
                // Special case: when the character is 'h', 'n', or 'k' (i.e. the ones before forbidden chars i, o, and l)
                // the next to follow should be two spaces ahead, instead of one. I.e. we skip one.
                if (char1 == 'h' || char1 == 'n' || char1 == 'k')
                {
                    char1++;
                }
                char2 = characters[p2];
                isValid = char2 - char1 == 1;
            }

            if (isValid)
            {
                break;
            }
            
            p1++;
            p2++;
        }

        if (!isValid)
        {
            return false;
        } 
        
        // Third check: Validate two different non-overlapping pairs
        HashSet<char> pairsSet = new ();
        p1 = 0;
        p2 = 1;
        while (p2 < characters.Length)
        {
            if (characters[p1] == characters[p2])
            {
                if (p2 < characters.Length - 1 && characters[p1] == characters[p2 + 1])
                {
                    p1 = p2 + 2;
                    p2 = p2 + 3;
                    continue;
                }
                pairsSet.Add(characters[p1]);
                p1 = p2 + 1;
                p2 = p2 + 2;
            }
            else
            {
                p1++;
                p2++;      
            }
        }

        return pairsSet.Count >= 2;
    }
}