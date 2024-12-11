using System;
using CsharpDotNetUtils;

namespace AdventOfCode2024.Day9;

public class Day9
{
    private int _checksum = 0;

    public void PartOneChecksum()
    {
        var streamReader = TextUtils.GetStreamReaderFromTextFile(@"Day9/testinput.txt");
        var filesystem = new List<int>();
        // read numbers in chunks using the streamReader
        while (!streamReader.EndOfStream)
        {
            filesystem.Add(int.Parse(((char)streamReader.Read()).ToString()));
        }

        var rearrangedFileSystem = MoveFiles(filesystem);
        _checksum = CalculateChecksum(rearrangedFileSystem);

        Console.WriteLine($"[Day 9 / part 1] Filesystem checksum: {_checksum}");
    }

    private List<int> MoveFiles(List<int> filesystem)
    {
        int rightFileIndex;
        bool spaceAtEnd;

        if (filesystem.Count % 2 == 0)
        {
            rightFileIndex = filesystem.Count - 2;
            spaceAtEnd = true;
        }
        else
        {
            rightFileIndex = filesystem.Count - 1;
            spaceAtEnd = false;
        }

        var leftFileIndex = 0;
        var remainingRightFileBlock = filesystem[rightFileIndex];
        var remainingFreespace = filesystem[leftFileIndex + 1];
        var currentLeftFileId = leftFileIndex;
        var currentRightFileId = rightFileIndex;
        var movedFiles = new List<int>();

        while (leftFileIndex < rightFileIndex)
        {
            // expand left file blocks
            for (var i = 0; i < filesystem[leftFileIndex]; i++)
            {
                movedFiles.Add(leftFileIndex);
            }
            leftFileIndex += 2;

            // fill up free space w/ right file blocks
            for (var j = 0; j < remainingFreespace; j++)
            {
                if (remainingRightFileBlock > 0)
                {
                    movedFiles.Add(filesystem[rightFileIndex]);
                    remainingRightFileBlock--;
                    remainingFreespace--;
                }
            }
            if (!(remainingRightFileBlock > 0))
            {
                rightFileIndex -= 2;
            }
        }

        // check for any remaining file blocks
        if (remainingRightFileBlock > 0 && spaceAtEnd)
        {
            for (var i = 0; i < remainingRightFileBlock; i++)
            {
                movedFiles.Add(filesystem[rightFileIndex]);
            }
        }
        return movedFiles;
    }

    private int CalculateChecksum(List<int> rearrangedFileSystem)
    {
        var runnigTotal = 0;
        for (var i = 0; i < rearrangedFileSystem.Count; i++)
        {
            runnigTotal += i * rearrangedFileSystem[i];
        }

        return runnigTotal;
    }
}
