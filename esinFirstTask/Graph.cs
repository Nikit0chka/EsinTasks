using Newtonsoft.Json;

namespace esinFirstTask;

public class Graph
{
    #region properties

    public string Name;
    public bool IsOriented;

    private List<GraphEdge> _edges;
    private List<GraphVertex> _vertices;

    #endregion

    #region constructors

    public Graph(string name, bool isOriented)
    {
        Name = name;
        IsOriented = isOriented;

        _edges = new List<GraphEdge>();
        _vertices = new List<GraphVertex>();
    }

    public Graph(Graph graphToCopy)
    {
        Name = graphToCopy.Name;
        IsOriented = graphToCopy.IsOriented;

        _vertices = new List<GraphVertex>();
        _edges = new List<GraphEdge>();

        var vertexMap = new Dictionary<GraphVertex, GraphVertex>();

        foreach (var vertex in graphToCopy._vertices)
        {
            var newVertex = new GraphVertex(vertex.Name);
            _vertices.Add(newVertex);
            vertexMap[vertex] = newVertex;
        }

        foreach (var edge in graphToCopy._edges)
        {
            var newEdge = new GraphEdge(
                edge.Name,
                vertexMap[edge.FirstVertex],
                vertexMap[edge.SecondVertex],
                edge.Weight
            );

            _edges.Add(newEdge);
            vertexMap[edge.FirstVertex].AddEdge(newEdge);
            vertexMap[edge.SecondVertex].AddEdge(newEdge);
        }
    }

    #endregion

    #region addItems

    public void AddVertex(GraphVertex addedVertex)
    {
        if (_vertices.Any(c => c.Name == addedVertex.Name))
            throw new Exception("a vertex with the same name has already been added!");

        _vertices.Add(addedVertex);
    }

    public void AddEdge(string edgeName, string firstVertexName, string secondVertexName, int edgeWeight)
    {
        if (_edges.Any(c => c.Name == edgeName))
            throw new Exception("an edge with the same name has already been added!");
        if (_vertices.All(c => c.Name != firstVertexName))
            throw new Exception("a vertex with this name has not been added!");
        if (_vertices.All(c => c.Name != secondVertexName))
            throw new Exception("a vertex with this name has not been added!");

        var firstVertex = _vertices.First(c => c.Name == firstVertexName);
        var secondVertex = _vertices.First(c => c.Name == secondVertexName);
        var addedEdge = new GraphEdge(edgeName, firstVertex, secondVertex, edgeWeight);

        firstVertex.AddEdge(addedEdge);
        secondVertex.AddEdge(addedEdge);

        _edges.Add(addedEdge);
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

        var removedEdges = _edges.Where(edge =>
            edge.FirstVertex.Name == removedVertexName || edge.SecondVertex.Name == removedVertexName).ToList();
        foreach (var edge in removedEdges)
            RemoveEdgeByName(edge.Name);

        var removedVertex = _vertices.First(c => c.Name == removedVertexName);
        _vertices.Remove(removedVertex);
    }

    public void RemoveEdgeByName(string removedEdgeName)
    {
        if (_edges.All(c => c.Name != removedEdgeName))
            throw new Exception("an edge with this name has not been added!");

        var removedVertices = _vertices.Where(vertex => vertex.GraphEdges.Any(edge => edge.Name == removedEdgeName))
            .ToList();
        var removedEdge = _edges.First(c => c.Name == removedEdgeName);

        foreach (var vertex in removedVertices)
            vertex.RemoveEdge(removedEdge);

        _edges.Remove(removedEdge);
    }

    #endregion

    #region fileWork

    public void AddGraphToFile(string pathToFile)
    {
        try
        {
            var verticesData = new JsonVertices(_vertices, Name);
            var edgesData = new JsonEdges(_edges, IsOriented);

            var jsonVertices = JsonConvert.SerializeObject(verticesData, Formatting.Indented,
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });

            var jsonEdges = JsonConvert.SerializeObject(edgesData, Formatting.Indented,
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });

            File.WriteAllText(pathToFile + "_vertices.json", jsonVertices);
            File.WriteAllText(pathToFile + "_edges.json", jsonEdges);
        }
        catch (Exception ex)
        {
            throw new Exception($"exception by trying write json to file by path {pathToFile}: {ex.Message}");
        }
    }

    public void CreateGraphByFile(string pathToFile)
    {
        try
        {
            var jsonVertices = File.ReadAllText(pathToFile + "_vertices.json");
            var jsonEdges = File.ReadAllText(pathToFile + "_edges.json");

            var verticesData = JsonConvert.DeserializeObject<JsonVertices>(jsonVertices,
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });

            var edgesData = JsonConvert.DeserializeObject<JsonEdges>(jsonEdges,
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });

            Name = verticesData!.Name;
            _vertices = verticesData.Vertices;

            IsOriented = edgesData!.IsOriented;
            _edges = edgesData.Edges;
        }
        catch (Exception ex)
        {
            throw new Exception($"exception reading json from file by path {pathToFile}: {ex.Message}");
        }
    }

    #endregion
}