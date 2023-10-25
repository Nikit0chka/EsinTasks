namespace esinFirstTask;

public class JsonEdge
{
    public required string EdgeName { get; init; }
    public required string FirstVertexName { get; init; }
    public required string SecondVertexName { get; init; }
    public int Weight { get; init; }
}