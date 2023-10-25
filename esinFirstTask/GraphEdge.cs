namespace esinFirstTask;

public class GraphEdge
{
    public string Name { get; set; }
    public int Weight { get; set; }
    public GraphVertex FirstVertex { get; set; }
    public GraphVertex SecondVertex { get; set; }

    public GraphEdge(string name, GraphVertex firstVertex, GraphVertex secondVertex, int weight)
    {
        Name = name;
        FirstVertex = firstVertex;
        SecondVertex = secondVertex;
        Weight = weight;
    }


    public override string ToString()
    {
        return Weight == 0
            ? $"Edge {Name}, FirstVertex - {FirstVertex.Name}, SecondVertex - {SecondVertex.Name}"
            : $"Edge {Name}, FirstVertex - {FirstVertex.Name}, SecondVertex - {SecondVertex.Name}, Weight - {Weight}";
    }
}