namespace esinFirstTask;

public class JsonGraph
{
    public string Name { get; set; }
    public bool IsOriented { get; set; }
    public List<GraphEdge> Edges { get; set; }
    public List<GraphVertex> Vertices { get; set; }

    public JsonGraph(List<GraphEdge> edges, List<GraphVertex> vertices, bool isOriented, string name)
    {
        Edges = edges;
        Vertices = vertices;
        IsOriented = isOriented;
        Name = name;
    }
}