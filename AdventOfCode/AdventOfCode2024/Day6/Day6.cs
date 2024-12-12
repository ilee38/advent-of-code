using System;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using CsharpDotNetUtils;

namespace AdventOfCode2024.Day6;

public class Day6
{
    private readonly char OBSTACLE = '#';
    private readonly char VISITED = 'X';
    private readonly char STARTING_DIRECTION = '^';
    private int _distinctPositionCount = 0;
    private int _possibleObstaclesCount = 0;
    private List<char[]> _grid = new();

    public void PartOneDistinctPositions()
    {
        using var streamReader = TextUtils.GetStreamReaderFromTextFile(@"Day6/day6-input.txt");
        while (!streamReader.EndOfStream)
        {
            _grid.Add(streamReader.ReadLine().ToCharArray());
        }

        // go to starting position
        var x = 0;
        var y = 0;
        for (var i = 0; i < _grid.Count; i++)
        {
            for (var j = 0; j < _grid[i].Length; j++)
            {
                if (_grid[i][j] == STARTING_DIRECTION)
                {
                    x = j;
                    y = i;
                }
            }
        }
        _distinctPositionCount = CountPositionsInPath(x, y, STARTING_DIRECTION);
        Console.WriteLine($"[Day6/Part1] Visited positions: {_distinctPositionCount}");
    }

    public void PartTwoPossibleObstaclesForLoop()
    {
        using var streamReader = TextUtils.GetStreamReaderFromTextFile(@"Day6/day6-input.txt");
        while (!streamReader.EndOfStream)
        {
            _grid.Add(streamReader.ReadLine().ToCharArray());
        }

        // go to starting position
        var x = 0;
        var y = 0;
        for (var i = 0; i < _grid.Count; i++)
        {
            for (var j = 0; j < _grid[i].Length; j++)
            {
                if (_grid[i][j] == STARTING_DIRECTION)
                {
                    x = j;
                    y = i;
                }
            }
        }

        _possibleObstaclesCount = CountPossibleObstaclesToMakeLoop(x, y, STARTING_DIRECTION);
        Console.WriteLine($"[Day6/Part2] Total obstacle positions: {_possibleObstaclesCount}");
    }

    private int CountPositionsInPath(int x, int y, char currentDirection)
    {
        var count = 0;
        var done = false;
        while (!done)
        {
            switch (currentDirection)
            {
                // Move up if not end of grid or obstacle in front
                case '^':
                    if (y - 1 < 0)
                    {
                        if (_grid[y][x] != VISITED)
                        {
                            _grid[y][x] = VISITED;
                            count++;
                        }
                        done = true;
                    }
                    else if (_grid[y - 1][x] == OBSTACLE)
                    {
                        currentDirection = '>';
                    }
                    else
                    {
                        if (_grid[y][x] != VISITED)
                        {
                            _grid[y][x] = VISITED;
                            count++;
                        }
                        y--;
                    }
                    break;
                // Move right if not end of grid or obstacle in front
                case '>':
                    if (x + 1 >= _grid[x].Length)
                    {
                        if (_grid[y][x] != VISITED)
                        {
                            _grid[y][x] = VISITED;
                            count++;
                        }
                        done = true;
                    }
                    else if (_grid[y][x + 1] == OBSTACLE)
                    {
                        currentDirection = 'v';
                    }
                    else
                    {
                        if (_grid[y][x] != VISITED)
                        {
                            _grid[y][x] = VISITED;
                            count++;
                        }
                        x++;
                    }
                    break;
                // Move down if not end of grid or obstacle in front
                case 'v':
                    if (y + 1 >= _grid.Count)
                    {
                        if (_grid[y][x] != VISITED)
                        {
                            _grid[y][x] = VISITED;
                            count++;
                        }
                        done = true;
                    }
                    else if (_grid[y + 1][x] == OBSTACLE)
                    {
                        currentDirection = '<';
                    }
                    else
                    {
                        if (_grid[y][x] != VISITED)
                        {
                            _grid[y][x] = VISITED;
                            count++;
                        }
                        y++;
                    }
                    break;
                // Move left if not end of grid or obstacle in front
                case '<':
                    if (x - 1 < 0)
                    {
                        if (_grid[y][x] != VISITED)
                        {
                            _grid[y][x] = VISITED;
                            count++;
                        }
                        done = true;
                    }
                    else if (_grid[y][x - 1] == OBSTACLE)
                    {
                        currentDirection = '^';
                    }
                    else
                    {
                        if (_grid[y][x] != VISITED)
                        {
                            _grid[y][x] = VISITED;
                            count++;
                        }
                        x--;
                    }
                    break;
            }
        }
        return count;
    }

    /// <summary>
    /// Brute-force solution to count all possible single-obstacle placements to create
    /// a loop for the guard.
    /// </summary>
    /// <param name="x">Guard's starting position x-coordinate</param>
    /// <param name="y">Guard's starting postiion y-coordinate</param>
    /// <param name="currentDirection"></param>
    /// <returns>Number of possible single obstacles to create a loop.</returns>
    private int CountPossibleObstaclesToMakeLoop(int x, int y, char currentDirection)
    {
        var obstacleCount = 0;

        for (var i = 0; i < _grid.Count; i++)
        {
            for (var j = 0; j < _grid[i].Length; j++)
            {
                // Place obstacle if position is free and test for loop
                if (_grid[i][j] == '.')
                {
                    _grid[i][j] = '#';
                    var loop = TestForLoop(x, y, currentDirection);
                    obstacleCount = loop ? obstacleCount + 1 : obstacleCount;

                    // Remove obstacle before continuing with new one
                    _grid[i][j] = '.';
                }
            }
        }
        return obstacleCount;
    }

    private bool TestForLoop(int x, int y, char currentDirection)
    {
        var loop = true;
        for (var i = 0; i < (_grid.Count * _grid[0].Length); i++)
        {
            switch (currentDirection)
            {
                // Move up if not end of grid or obstacle in front
                case '^':
                    if (y - 1 < 0)
                    {
                        // Went off grid
                        return false;
                    }
                    else if (_grid[y - 1][x] == OBSTACLE)
                    {
                        currentDirection = '>';
                    }
                    else
                    {
                        y--;
                    }
                    break;
                // Move right if not end of grid or obstacle in front
                case '>':
                    if (x + 1 >= _grid[x].Length)
                    {
                        // Went off grid
                        return false;
                    }
                    else if (_grid[y][x + 1] == OBSTACLE)
                    {
                        currentDirection = 'v';
                    }
                    else
                    {
                        x++;
                    }
                    break;
                // Move down if not end of grid or obstacle in front
                case 'v':
                    if (y + 1 >= _grid.Count)
                    {
                        // Went off grid
                        return false;
                    }
                    else if (_grid[y + 1][x] == OBSTACLE)
                    {
                        currentDirection = '<';
                    }
                    else
                    {
                        y++;
                    }
                    break;
                // Move left if not end of grid or obstacle in front
                case '<':
                    if (x - 1 < 0)
                    {
                        // Went off grid
                        return false;
                    }
                    else if (_grid[y][x - 1] == OBSTACLE)
                    {
                        currentDirection = '^';
                    }
                    else
                    {
                        x--;
                    }
                    break;
            }
        }
        return loop;
    }
}
