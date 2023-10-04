namespace esinFirstTask;

public class GraphVertex
{
    public readonly string Name;
    private readonly List<GraphEdge> _graphEdges;

    public GraphVertex(string name)
    {
        Name = name;
        _graphEdges = new List<GraphEdge>();
    }

    public void AddEdge(GraphEdge edge)
    {
        if (_graphEdges.Contains(edge))
            throw new Exception("can not add edge because it is already added");
        
        _graphEdges.Add(edge);
    }
}