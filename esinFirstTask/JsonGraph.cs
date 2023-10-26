namespace esinFirstTask;

/// <summary>
/// Logic for converting graph to json
/// </summary>
public class JsonGraph
{
    /// <summary>
    /// Graph name
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Graph oriented
    /// </summary>
    public required bool IsOriented { get; set; }

    /// <summary>
    /// Graph json edges
    /// </summary>
    public required List<JsonEdge> JsonEdges { get; set; }

    /// <summary>
    /// Graph json vertices
    /// </summary>
    public required List<JsonVertex> JsonVertices { get; set; }
}