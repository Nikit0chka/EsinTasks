namespace esinFirstTask;

public class ConsoleGraphWork
{
    private Graph _graph;
    private delegate void MyMethodDelegate();
    private readonly Dictionary<string, MyMethodDelegate> _commands;

    public ConsoleGraphWork(Graph graph)
    {
        _graph = graph;
        _commands = new Dictionary<string, MyMethodDelegate>
        {
            { "1", AddEdge }, { "2", AddVertex }, { "3", RemoveEdge },
            { "4", RemoveVertex }, { "5", WriteGraphToFile },
            { "6", CreateGraphByFile }, { "7", WriteEdges },
            { "8", WriteVertices }, { "9", WriteCommands }
        };
    }

    public void Start()
    {
        string? input = "";
        WriteCommands();

        while (input != "0")
        {
            Console.WriteLine("Input command: ");
            input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
                continue;

            if (_commands.TryGetValue(input, out MyMethodDelegate? command))
                command.Invoke();
        }
    }

    private void WriteGraphToFile()
    {
        Console.WriteLine("Input path to file: ");
        var pathToFile = Console.ReadLine();

        if (string.IsNullOrEmpty(pathToFile))
        {
            Console.WriteLine("Path to file can not be empty!");
            return;
        }

        try
        {
            _graph.AddGraphToFile(pathToFile);
            Console.WriteLine("Graph added to file successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private void CreateGraphByFile()
    {
        Console.WriteLine("Input path to file: ");
        var pathToFile = Console.ReadLine();

        if (string.IsNullOrEmpty(pathToFile))
        {
            Console.WriteLine("Path to file can not be empty!");
            return;
        }

        try
        {
            _graph = new Graph(pathToFile);
            Console.WriteLine("Graph created by file successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private void AddEdge()
    {
        WriteVertices();
        WriteEdges();

        Console.WriteLine("Input edge name: ");
        var edgeName = Console.ReadLine();
        Console.WriteLine("Input first vertex name: ");
        var firstVertexName = Console.ReadLine();
        Console.WriteLine("Input second vertex name: ");
        var secondVertexName = Console.ReadLine();

        if (string.IsNullOrEmpty(edgeName))
        {
            Console.WriteLine("Edge name can not be empty!");
            return;
        }

        if (string.IsNullOrEmpty(firstVertexName))
        {
            Console.WriteLine("First vertex name can not be empty!");
            return;
        }

        if (string.IsNullOrEmpty(secondVertexName))
        {
            Console.WriteLine("Second vertex name can not be empty!");
            return;
        }

        try
        {
            _graph.AddEdge(edgeName, firstVertexName, secondVertexName);
            Console.WriteLine("Edge added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private void RemoveEdge()
    {
        WriteVertices();
        WriteEdges();

        Console.WriteLine("Input removed edge name: ");
        var removedEdgeName = Console.ReadLine();

        if (string.IsNullOrEmpty(removedEdgeName))
        {
            Console.WriteLine("Removed edge name can not be empty!");
            return;
        }

        try
        {
            _graph.RemoveEdgeByName(removedEdgeName);
            Console.WriteLine("Edge removed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private void RemoveVertex()
    {
        WriteVertices();
        WriteEdges();

        Console.WriteLine("Input removed vertex name: ");
        var removedVertexName = Console.ReadLine();

        if (string.IsNullOrEmpty(removedVertexName))
        {
            Console.WriteLine("Removed vertex name can not be empty!");
            return;
        }

        try
        {
            _graph.RemoveVertexByName(removedVertexName);
            Console.WriteLine("Vertex removed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private void AddVertex()
    {
        WriteVertices();
        WriteEdges();

        Console.WriteLine("Input vertex name: ");
        string? name = Console.ReadLine();

        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Vertex name can not be empty!");
            return;
        }

        try
        {
            _graph.AddVertex(new GraphVertex(name));
            Console.WriteLine("Edge added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private void WriteVertices()
    {
        Console.WriteLine("Your vertices: ");

        foreach (GraphVertex vertex in _graph.GetVerticesList())
            Console.WriteLine(vertex.Name);
    }

    private void WriteEdges()
    {
        Console.WriteLine("Your edges: ");

        foreach (GraphEdge edge in _graph.GetEdgesList())
            Console.Write($"{edge.Name} ");
    }

    private void WriteCommands()
    {
        foreach (var command in _commands)
            Console.WriteLine($"{command.Key} - {command.Value.Method.Name}");
    }
}