namespace esinFirstTask;


/// <summary>
/// Logic work with graph edge
/// </summary>
public class GraphEdge
{
    /// <summary>
    /// Edge name
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Edge weight
    /// </summary>
    public int Weight { get; }

    /// <summary>
    /// Edge first vertex
    /// </summary>
    public GraphVertex FirstVertex { get; }

    /// <summary>
    /// Edge second vertex
    /// </summary>
    public GraphVertex SecondVertex { get; }

    /// <summary>
    /// </summary>
    /// <param name="name">Edge name</param>
    /// <param name="firstVertex">Edge first vertex</param>
    /// <param name="secondVertex">Edge second vertex</param>
    /// <param name="weight">Edge weight</param>
    public GraphEdge(string name, GraphVertex firstVertex, GraphVertex secondVertex, int weight)
    {
        Name = name;
        FirstVertex = firstVertex;
        SecondVertex = secondVertex;
        Weight = weight;
    }

    /// <summary>
    /// Override for better console work experience
    /// </summary>
    public override string ToString()
    {
        return Weight == 0
            ? $"Edge {Name}, FirstVertex - {FirstVertex.Name}, SecondVertex - {SecondVertex.Name}"
            : $"Edge {Name}, FirstVertex - {FirstVertex.Name}, SecondVertex - {SecondVertex.Name}, Weight - {Weight}";
    }
}