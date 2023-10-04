using System.Text.Json;

namespace esinFirstTask;

public class Graph
{
    #region properties

    public string Name;

    private List<GraphEdge> _edges;
    private List<GraphVertex> _vertices;
    private bool _isOriented;

    #endregion

    #region constructors

    public Graph()
    {
        _edges = new List<GraphEdge>();
        _vertices = new List<GraphVertex>();
    }

    public Graph(string pathToFile)
    {
        _edges = new List<GraphEdge>();
        _vertices = new List<GraphVertex>();

        CreateGraphByFile(pathToFile);
    }

    public Graph(Graph graphToCopy)
    {
        _edges = new List<GraphEdge>();
        _vertices = new List<GraphVertex>();

        foreach (GraphEdge edge in graphToCopy._edges)
            _edges.Add(edge);

        foreach (GraphVertex vertex in graphToCopy._vertices)
            _vertices.Add(vertex);
    }

    #endregion

    #region addItems

    public void AddVertex(GraphVertex addedVertex)
    {
        if (_vertices.Any(c => c.Name == addedVertex.Name))
            throw new Exception("a vertex with the same name has already been added!");

        _vertices.Add(addedVertex);
    }

    public void AddEdge(string edgeName, string firstVertexName, string secondVertexName)
    {
        if (_edges.All(c => c.Name != edgeName))
            throw new Exception("an edge with the same name has already been added!");
        if (_vertices.All(c => c.Name != firstVertexName))
            throw new Exception("a vertex with this name has not been added!");
        if (_vertices.All(c => c.Name != secondVertexName))
            throw new Exception("a vertex with this name has not been added!");

        GraphVertex firstVertex = _vertices.First(c => c.Name == firstVertexName);
        GraphVertex secondVertex = _vertices.First(c => c.Name == secondVertexName);
        _edges.Add(new GraphEdge(edgeName, firstVertex, secondVertex));
    }

    #endregion

    #region getItems

    public List<GraphVertex> GetVerticesList() =>
        _vertices;

    public List<GraphEdge> GetEdgesList() =>
        _edges;

    #endregion

    #region removeItems

    public void RemoveVertexByName(string removedVertexName)
    {
        if (_vertices.All(c => c.Name != removedVertexName))
            throw new Exception("a vertex with this name has not been added!");

        _vertices.Remove(_vertices.First(c => c.Name == removedVertexName));
    }

    public void RemoveEdgeByName(string removedEdgeName)
    {
        if (_edges.All(c => c.Name != removedEdgeName))
            throw new Exception("an edge with this name has not been added!");

        _edges.Remove(_edges.First(c => c.Name == removedEdgeName));
    }

    #endregion

    #region fileWork

    public void AddGraphToFile(string pathToFile)
    {
        try
        {
            Json data = new(_edges, _vertices);
            var json = JsonSerializer.Serialize(data);

            File.WriteAllText(pathToFile, json);
        }
        catch (Exception ex)
        {
            throw new Exception($"exception by trying write json to file by path {pathToFile}: {ex}");
        }
    }

    private void CreateGraphByFile(string pathToFile)
    {
        try
        {
            var json = File.ReadAllText(pathToFile);
            Json? data = JsonSerializer.Deserialize<Json>(json);

            if (data?.Vertices is not null)
                _vertices = data.Vertices;
            if (data?.Edges is not null)
                _edges = data.Edges;
        }
        catch (Exception ex)
        {
            throw new Exception($"exception reading json from file by path {pathToFile}: {ex.Message}");
        }
    }

    #endregion
}