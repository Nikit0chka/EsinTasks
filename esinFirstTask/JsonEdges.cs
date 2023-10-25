namespace esinFirstTask;

public class JsonEdges
{
    public bool IsOriented { get; set; }
    public List<GraphEdge> Edges { get; set; }

    public JsonEdges(List<GraphEdge> edges, bool isOriented)
    {
        Edges = edges;
        IsOriented = isOriented;
    }
}