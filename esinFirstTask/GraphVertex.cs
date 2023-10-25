namespace esinFirstTask;

/// <summary>
/// Logic work with graph vertex
/// </summary>
public class GraphVertex
{
    /// <summary>
    /// Vertex name
    /// </summary>
    public string Name  { get; }

    /// <summary>
    /// Vertex edges
    /// </summary>
    public List<GraphEdge> GraphEdges { get; }

    /// <summary>
    /// </summary>
    /// <param name="name">Vertex name</param>
    public GraphVertex(string name)
    {
        Name = name;
        GraphEdges = new List<GraphEdge>();
    }

    /// <summary>
    /// Adding an edge to vertex
    /// </summary>
    /// <param name="edge">Edge to add</param>
    public void AddEdge(GraphEdge edge)
    {
        if (GraphEdges.Contains(edge))
           return;

        GraphEdges.Add(edge);
    }

    /// <summary>
    /// Removing an edge to vertex
    /// </summary>
    /// <param name="edge">Edge to remove</param>
    public void RemoveEdge(GraphEdge edge)
    {
        if (!GraphEdges.Contains(edge))
            return;

        GraphEdges.Remove(edge);
    }

    /// <summary>
    /// Override for better console work experience
    /// </summary>
    public override string ToString() => $"Vertex: {Name}, Edges: {string.Join(", ", GraphEdges.Select(graphEdge => graphEdge.Name))}";
}