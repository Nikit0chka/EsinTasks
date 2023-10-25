namespace esinFirstTask;

public static class ConsoleGraphWork
{
    private delegate void MyMethodDelegate(Graph graph);

    public static void Start(Graph graph)
    {
        var commands = new Dictionary<string, MyMethodDelegate>
        {
            { "1", AddEdge }, { "2", AddVertex }, { "3", RemoveEdge },
            { "4", RemoveVertex }, { "5", WriteGraphToFile },
            { "6", CreateGraphByFile }, { "7", WriteEdges },
            { "8", WriteVertices }
        };

        while (true)
        {
            Console.WriteLine("9 - Show commands");
            Console.WriteLine("0 - Back to manage graphs");

            Console.WriteLine("Input command: ");
            var input = Console.ReadLine();

            if (input == "0")
                break;

            if (input == "9")
            {
                ShowCommands(commands);
                continue;
            }

            if (string.IsNullOrEmpty(input) || !commands.ContainsKey(input))
            {
                Console.WriteLine("Incorrect input!");
                continue;
            }

            commands[input].Invoke(graph);
        }
    }

    private static void WriteGraphToFile(Graph graph)
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
            graph.AddGraphToFile(pathToFile);
            Console.WriteLine("Graph added to file successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void CreateGraphByFile(Graph graph)
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
            graph.CreateGraphByFile(pathToFile);
            Console.WriteLine("Graph created by file successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void AddEdge(Graph graph)
    {
        WriteVertices(graph);
        WriteEdges(graph);

        Console.WriteLine("Input edge name: ");
        var edgeName = Console.ReadLine();
        Console.WriteLine("Input first vertex name: ");
        var firstVertexName = Console.ReadLine();
        Console.WriteLine("Input second vertex name: ");
        var secondVertexName = Console.ReadLine();
        var edgeWeight = 0;

        if (graph.IsOriented)
        {
            Console.WriteLine("Input edge weight: ");
            var edgeWeightInput = Console.ReadLine();

            if (string.IsNullOrEmpty(edgeWeightInput) || !int.TryParse(edgeWeightInput, out edgeWeight))
            {
                Console.WriteLine("Check edge weight input! ");
                return;
            }
        }

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
            graph.AddEdge(edgeName, firstVertexName, secondVertexName, edgeWeight);
            Console.WriteLine("Edge added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void RemoveEdge(Graph graph)
    {
        WriteVertices(graph);
        WriteEdges(graph);

        Console.WriteLine("Input removed edge name: ");
        var removedEdgeName = Console.ReadLine();

        if (string.IsNullOrEmpty(removedEdgeName))
        {
            Console.WriteLine("Removed edge name can not be empty!");
            return;
        }

        try
        {
            graph.RemoveEdgeByName(removedEdgeName);
            Console.WriteLine("Edge removed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void RemoveVertex(Graph graph)
    {
        WriteVertices(graph);
        WriteEdges(graph);

        Console.WriteLine("Input removed vertex name: ");
        var removedVertexName = Console.ReadLine();

        if (string.IsNullOrEmpty(removedVertexName))
        {
            Console.WriteLine("Removed vertex name can not be empty!");
            return;
        }

        try
        {
            graph.RemoveVertexByName(removedVertexName);
            Console.WriteLine("Vertex removed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void AddVertex(Graph graph)
    {
        WriteVertices(graph);
        WriteEdges(graph);

        Console.WriteLine("Input vertex name: ");
        var name = Console.ReadLine();

        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Vertex name can not be empty!");
            return;
        }

        try
        {
            graph.AddVertex(new GraphVertex(name));
            Console.WriteLine("Vertex added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void WriteVertices(Graph graph)
    {
        foreach (var vertex in graph.GetVerticesList())
            Console.WriteLine(vertex.ToString());
    }

    private static void WriteEdges(Graph graph)
    {
        foreach (var edge in graph.GetEdgesList())
            Console.WriteLine(edge.ToString());
    }

    private static void ShowCommands(Dictionary<string, MyMethodDelegate> commands)
    {
        foreach (var command in commands)
            Console.WriteLine($"{command.Key} - {command.Value.Method.Name}");
    }
}