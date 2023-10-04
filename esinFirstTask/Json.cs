namespace esinFirstTask;

public class Json
{
    public List<GraphEdge> Edges { get; set; }
    public List<GraphVertex> Vertices { get; set; }

    public Json(List<GraphEdge> edges, List<GraphVertex> vertices)
    {
        Edges = edges;
        Vertices = vertices;
    }
}