using System.IO.Pipes;
using CsharpDotNetUtils;

namespace AdventOfCode2015.Day9;

public class Day9
{
    private const string FilePath = @"Day9/input.txt";
    private readonly Dictionary<Vertex, List<Edge>> _graph = new Dictionary<Vertex, List<Edge>>();
    
    public void PartOne()
    {
        var sr = TextUtils.GetStreamReaderFromTextFile(FilePath);
        CreateGraph(sr);
        
        // For each vertex in the graph, create a priority queue (List) and run Dijkstra's starting form it
        foreach (var startingVertex in _graph.Keys)
        {
            var priorityQueue = CreatePriorityQueue(startingVertex);
            var shortestPath = new List<Vertex>();
            
            while (priorityQueue.Count > 0)
            {
                var currentVertex = GetLowestPriorityVertex(priorityQueue);
                shortestPath.Add(currentVertex);
                foreach (var edge in _graph[currentVertex])
                {
                    var adjacentVertex = edge.Destination;
                    if (RelaxEdge(currentVertex, adjacentVertex, edge.EdgeWeight))
                    {
                        // update priority queue with new vertex weight
                        UpdatePriority(adjacentVertex, priorityQueue);
                    }
                }
            }

            var totalDistanceForShortestPath = 0;
            shortestPath.ForEach(x => totalDistanceForShortestPath += x.Weight);
            Console.WriteLine($"Shortest path starting from {startingVertex.Name}: {totalDistanceForShortestPath}");
        }
    }

    private void CreateGraph(StreamReader sr)
    {
        var currentVertexName = string.Empty;
        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine();
            var parts = line.Split(' ');
            var nextVertexName = parts[0];
            
            Vertex vertex;
            if (nextVertexName != currentVertexName && !_graph.Keys.Any(x => x.Name.Equals(nextVertexName)))
            {
                vertex = new Vertex(nextVertexName);
                _graph.Add(vertex, new List<Edge>());
            }
            else
            {
                vertex = _graph.Keys.First(x => x.Name == nextVertexName);
            }
            
            // Add destination vertex as well if it doesn't exist in the graph
            Vertex destinationVertex;
            if (!_graph.Keys.Any(x => x.Name.Equals(parts[2])))
            {
                destinationVertex = new Vertex(parts[2]);
                _graph.Add(destinationVertex, new List<Edge>());
            }
            else
            {
                destinationVertex = _graph.Keys.First(x => x.Name == parts[2]);
            } 
            
            _graph[vertex].Add(new Edge(vertex, destinationVertex, int.Parse(parts[4])));
            
            // Also add connection the other way around (i.e. this should be an un-directed graph)
            _graph[destinationVertex].Add(new Edge(destinationVertex, vertex, int.Parse(parts[4])));
            currentVertexName = nextVertexName;
        }
    }
    
    private List<Vertex> CreatePriorityQueue(Vertex startingVertex)
    {
        var queue = new List<Vertex>();
        startingVertex.Weight = 0;
        queue.Add(startingVertex);
        
        foreach (var vertex in _graph[startingVertex])
        {
            if (vertex.Destination.Name.Equals(startingVertex.Name, StringComparison.InvariantCultureIgnoreCase))
            {
                vertex.Destination.Weight = 0;
            }
            else
            {
                vertex.Destination.Weight = int.MaxValue;
            }
            queue.Add(vertex.Destination);
        }
        return queue;
    }

    private static Vertex GetLowestPriorityVertex(List<Vertex> priorityQueue)
    {
        var lowestPriorityVertex = priorityQueue[0];
        foreach (var vertex in priorityQueue)
        {
            if (vertex.Weight < lowestPriorityVertex.Weight)
            {
                lowestPriorityVertex = vertex;
            }
        }
        priorityQueue.Remove(lowestPriorityVertex);
        return lowestPriorityVertex;
    }

    private static bool RelaxEdge(Vertex source, Vertex destination, int edgeWeight)
    {
        if (destination.Weight > source.Weight + edgeWeight)
        {
            destination.Weight = source.Weight + edgeWeight;
            destination.Parent = source;
            return true;
        }
        return false;
    }

    private static void UpdatePriority(Vertex vertex, List<Vertex> priorityQueue)
    {
        foreach (var v in priorityQueue)
        {
            if (v.Name.Equals(vertex.Name, StringComparison.InvariantCultureIgnoreCase))
            {
                v.Weight = vertex.Weight;
            }
        }
    }
}

internal class Vertex : IEquatable<Vertex>
{
    public string Name { get; set; }
    public Vertex? Parent { get; set; }
    public int Weight { get; set; }

    public Vertex(string name, int weight = int.MaxValue, Vertex parent = null)
    {
        Name = name;
        Weight = weight;
        Parent = parent;
    }

    public bool Equals(Vertex? other)
    {
        if (other == null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }
        
        return Name == other.Name;
    }
}

internal class Edge
{
    public Vertex Source { get; set; }
    public Vertex Destination { get; set; }
    public int EdgeWeight { get; set; }
    
    public Edge(Vertex source, Vertex destination, int edgeWeight)
    {
        Source = source;
        Destination = destination;
        EdgeWeight = edgeWeight;
    }
}