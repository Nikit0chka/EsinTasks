namespace esinFirstTask;

public class JsonGraph
{
    public required string Name { get; set; }
    public required bool IsOriented { get; set; }
    public required List<JsonEdge> JsonEdges { get; set; }
    public required List<JsonVertex> JsonVertices { get; set; }
}