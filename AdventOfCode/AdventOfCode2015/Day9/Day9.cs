using System.IO.Pipes;
using CsharpDotNetUtils;

namespace AdventOfCode2015.Day9;

public class Day9
{
    // TODO: re-implement solution! Dijkstra's algorithm doesn't apply here. This is actually a Traveling Salesman Problem (TSP).
    // Each city has to be visited exactly once. 
    private const string FilePath = @"Day9/input.txt";
    private readonly Dictionary<string, List<Tuple<string, int>>> _distances = new();
    private readonly HashSet<string> _cities = new();
    
    public void PartOne()
    {
        var sr = TextUtils.GetStreamReaderFromTextFile(FilePath);
        LoadCitiesAndDistances(sr);
        
        var shortestDistance = int.MaxValue;
        foreach (var cityPermutation in GetPermutations(_cities.ToList()))
        {
            var distance = CalculateDistance(cityPermutation);     
            shortestDistance = Math.Min(shortestDistance, distance);
        }
        
        Console.WriteLine($"[Day9/Part1]: shortest distance: {shortestDistance}");
    }

    public void PartTwo()
    {
        var sr = TextUtils.GetStreamReaderFromTextFile(FilePath);
        LoadCitiesAndDistances(sr);
        
        var longestDistance = int.MinValue;
        foreach (var cityPermutation in GetPermutations(_cities.ToList()))
        {
            var distance = CalculateDistance(cityPermutation);     
            longestDistance = Math.Max(longestDistance, distance);
        }
        
        Console.WriteLine($"[Day9/Part2]: longest distance: {longestDistance}");       
    }

    private void LoadCitiesAndDistances(StreamReader sr)
    {
        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine().Split(' ');
            var sourceCity = line[0];
            var destinationCity = line[2];
            var distance = int.Parse(line[4]);
            var sourceToDestinationDistance = new Tuple<string, int>(destinationCity, distance);
            var destinationToSourceDistance = new Tuple<string, int>(sourceCity, distance);
            
            _cities.Add(sourceCity);
            _cities.Add(destinationCity);
            
            if (!_distances.ContainsKey(sourceCity))
            {
                _distances.Add(sourceCity, new List<Tuple<string, int>> {sourceToDestinationDistance});
            }
            else
            {
                _distances[sourceCity].Add(sourceToDestinationDistance);
                
            }

            if (!_distances.ContainsKey(destinationCity))
            {
                _distances.Add(destinationCity, new List<Tuple<string, int>> {destinationToSourceDistance});
            }
            else
            {
                _distances[destinationCity].Add(destinationToSourceDistance);
            }
        }
    }

    private IEnumerable<List<string>> GetPermutations(List<string> cities)
    {
        if (cities.Count == 0)
        {
            yield return new List<string>();
            yield break;
        }

        for (var i = 0; i < cities.Count; i++)
        {
            var currentCity = cities[i];
            var remainingCities = cities.Take(i).Concat(cities.Skip(i + 1)).ToList();

            foreach (var permutation in GetPermutations(remainingCities))
            {
                permutation.Insert(0, currentCity);
                yield return permutation;
            }
        }
    }

    private int CalculateDistance(List<string> route)
    {
        var distance = 0;
        for (var i = 0; i < route.Count - 1; i++)
        {
            var currentCity = route[i];
            var nextCity = route[i + 1];
            distance += _distances[currentCity].Where(x => x.Item1.Equals(nextCity)).First().Item2;
        }
        return distance;
    }
}