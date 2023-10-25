namespace esinFirstTask;

public class JsonVertices
{
    public string Name { get; set; }
    public List<GraphVertex> Vertices { get; set; }

    public JsonVertices(List<GraphVertex> vertices, string name)
    {
        Vertices = vertices;
        Name = name;
    }
}
