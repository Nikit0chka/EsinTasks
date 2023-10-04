namespace esinFirstTask;

public class GraphEdge
{
    public readonly string Name;
    private GraphVertex _firstVertex;
    private GraphVertex _secondVertex;
    public int Weight;

    public GraphEdge(string name, GraphVertex firstVertex, GraphVertex secondVertex)
    {
        Name = name;
        _firstVertex = firstVertex;
        _secondVertex = secondVertex;
    }
}