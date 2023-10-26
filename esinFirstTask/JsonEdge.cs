namespace esinFirstTask;

/// <summary>
/// Logic for converting graph edge to json
/// </summary>
public class JsonEdge
{
    /// <summary>
    /// Edge name
    /// </summary>
    public required string EdgeName { get; init; }

    /// <summary>
    /// Edge first vertex name
    /// </summary>
    public required string FirstVertexName { get; init; }

    /// <summary>
    /// Edge second vertex name
    /// </summary>
    public required string SecondVertexName { get; init; }

    /// <summary>
    /// Edge weight
    /// </summary>
    public int Weight { get; init; }
}