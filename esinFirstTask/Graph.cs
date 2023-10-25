using System.Text.Json;

namespace esinFirstTask;

/// <summary>
/// Logic work with graph
/// </summary>
public class Graph
{
    #region properties

    /// <summary>
    /// Graph name
    /// </summary>
    public string Name;

    /// <summary>
    /// Is graph oriented
    /// </summary>
    public bool IsOriented;

    /// <summary>
    /// Graph edges
    /// </summary>
    private List<GraphEdge> _edges;

    /// <summary>
    /// Graph vertices
    /// </summary>
    private List<GraphVertex> _vertices;

    #endregion

    #region constructors

    /// <param name="name">Graph name</param>
    /// <param name="isOriented">Graph oriented</param>
    public Graph(string name, bool isOriented)
    {
        Name = name;
        IsOriented = isOriented;

        _edges = new List<GraphEdge>();
        _vertices = new List<GraphVertex>();
    }

    /// <param name="graphToCopy">Graph to copy</param>
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

    /// <summary>
    /// Adding a vertex to graph
    /// </summary>
    /// <param name="addedVertex">Added vertex</param>
    public void AddVertex(GraphVertex addedVertex)
    {
        if (_vertices.Any(c => c.Name == addedVertex.Name))
            throw new Exception($"a vertex with the same {addedVertex.Name} has already been added!");

        _vertices.Add(addedVertex);
    }

    /// <summary>
    /// Adding an edge to graph
    /// </summary>
    /// <param name="edgeName">Added edge name</param>
    /// <param name="firstVertexName">Added first vertex name</param>
    /// <param name="secondVertexName">Added second vertex name</param>
    /// <param name="edgeWeight">Added edge weight</param>
    public void AddEdge(string edgeName, string firstVertexName, string secondVertexName, int edgeWeight)
    {
        if (_edges.Any(c => c.Name == edgeName))
            throw new Exception($"an edge with the same {edgeName} has already been added!");
        if (_vertices.All(c => c.Name != firstVertexName))
            throw new Exception($"a vertex with {firstVertexName} name has not been added!");
        if (_vertices.All(c => c.Name != secondVertexName))
            throw new Exception($"a vertex with {secondVertexName} name has not been added!");

        var firstVertex = _vertices.First(c => c.Name == firstVertexName);
        var secondVertex = _vertices.First(c => c.Name == secondVertexName);
        var addedEdge = new GraphEdge(edgeName, firstVertex, secondVertex, edgeWeight);

        firstVertex.AddEdge(addedEdge);
        secondVertex.AddEdge(addedEdge);

        _edges.Add(addedEdge);
    }

    #endregion

    #region getItems

    /// <returns>returns Vertex list</returns>
    public List<GraphVertex> GetVerticesList() =>
        _vertices;

    /// <returns>returns Edges list</returns>
    public List<GraphEdge> GetEdgesList() =>
        _edges;

    #endregion

    #region removeItems

    /// <summary>
    /// Removing vertex from graph
    /// </summary>
    /// <param name="removedVertexName">Removed vertex name</param>
    public void RemoveVertexByName(string removedVertexName)
    {
        if (_vertices.All(c => c.Name != removedVertexName))
            throw new Exception($"a vertex with {removedVertexName} name has not been added!");

        var removedEdges = _edges.Where(edge =>
            edge.FirstVertex.Name == removedVertexName || edge.SecondVertex.Name == removedVertexName).ToList();

        foreach (var edge in removedEdges)
            RemoveEdgeByName(edge.Name);

        var removedVertex = _vertices.First(c => c.Name == removedVertexName);
        _vertices.Remove(removedVertex);
    }

    /// <summary>
    /// Removing edge from graph
    /// </summary>
    /// <param name="removedEdgeName">Removed edge name</param>
    public void RemoveEdgeByName(string? removedEdgeName)
    {
        if (_edges.All(c => c.Name != removedEdgeName))
            throw new Exception($"an edge with {removedEdgeName} name has not been added!");

        var removedVertices = _vertices.Where(vertex => vertex.GraphEdges.Any(edge => edge.Name == removedEdgeName))
            .ToList();
        var removedEdge = _edges.First(c => c.Name == removedEdgeName);

        foreach (var vertex in removedVertices)
            vertex.RemoveEdge(removedEdge);

        _edges.Remove(removedEdge);
    }

    #endregion

    #region fileWork

    /// <summary>
    /// Removing vertex from graph
    /// </summary>
    /// <param name="pathToFile">Path to file</param>
    public void AddGraphToFile(string pathToFile)
    {
        try
        {
            //json for writing in file
            var jsonEdges = _edges.Select(edge => new JsonEdge
            {
                EdgeName = edge.Name, FirstVertexName = edge.FirstVertex.Name,
                SecondVertexName = edge.SecondVertex.Name, Weight = edge.Weight
            }).ToList();

            var jsonVertices = _vertices.Select(vertex => new JsonVertex { Name = vertex.Name }).ToList();

            var jsonGraph = new JsonGraph
                { JsonEdges = jsonEdges, JsonVertices = jsonVertices, IsOriented = IsOriented, Name = Name };

            //serializing json
            var data = JsonSerializer.Serialize(jsonGraph);

            //writing in file
            File.WriteAllText(pathToFile, data);
        }
        catch (Exception ex)
        {
            throw new Exception($"exception by trying write json to file by path {pathToFile}: {ex.Message}");
        }
    }

    /// <summary>
    /// Creating graph by file
    /// </summary>
    /// <param name="pathToFile">Path to file</param>
    public void CreateGraphByFile(string pathToFile)
    {
        try
        {
            //json from file
            var jsonText = File.ReadAllText(pathToFile);
            //deserializing json
            var jsonGraph = JsonSerializer.Deserialize<JsonGraph>(jsonText);

            var edges = new List<GraphEdge>();
            IsOriented = jsonGraph!.IsOriented;
            Name = jsonGraph.Name;

            //vertices from file
            var vertices = jsonGraph.JsonVertices.Select(vertex => new GraphVertex(vertex.Name)).ToList();

            //creating edges by vertices
            jsonGraph.JsonEdges.ForEach(edge =>
            {
                var firstVertex = vertices.First(vertex => vertex.Name == edge.FirstVertexName);
                var secondVertex = vertices.First(vertex => vertex.Name == edge.SecondVertexName);
                var addedEdge = new GraphEdge(edge.EdgeName, firstVertex, secondVertex, edge.Weight);

                edges.Add(addedEdge);
                firstVertex.AddEdge(addedEdge);
                secondVertex.AddEdge(addedEdge);
            });

            _vertices = new List<GraphVertex>(vertices);
            _edges = new List<GraphEdge>(edges);
        }
        catch (Exception ex)
        {
            throw new Exception($"exception reading json from file by path {pathToFile}: {ex.Message}");
        }
    }

    #endregion
}