namespace esinFirstTask;

public class GraphVertex
{
    public string Name  { get; set; }
    public List<GraphEdge> GraphEdges { get; set; }

    public GraphVertex(string name)
    {
        Name = name;
        GraphEdges = new List<GraphEdge>();
    }

    public void AddEdge(GraphEdge edge)
    {
        if (GraphEdges.Contains(edge))
           return;
        
        GraphEdges.Add(edge);
    }

    public void RemoveEdge(GraphEdge edge)
    {
        if (!GraphEdges.Contains(edge))
            return;

        GraphEdges.Remove(edge);
    }
    
    public override string ToString() => $"Vertex: {Name}, Edges: {string.Join(", ", GraphEdges.Select(graphEdge => graphEdge.Name))}";
    
}