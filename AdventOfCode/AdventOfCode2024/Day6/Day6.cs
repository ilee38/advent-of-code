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
                        currentDirection = '|';
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
                case '|':
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
}
