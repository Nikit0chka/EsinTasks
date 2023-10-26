namespace esinFirstTask;

/// <summary>
/// Working with graphs
/// </summary>
internal static class ManageGraphs
{
    private delegate void MyMethodDelegate(Dictionary<string, Graph> graphs);
    //Commands
    private enum Command
    {
        ChooseGraph = 1,
        ShowGraphs = 2,
        AddGraph = 3,
        DeleteGraph = 4,
        CopyGraph = 5,
        ShowCommands = 6,
        CreateComplementGraph = 7,
        CreateJoinGraph = 8,
        CreateUnixGraph = 9,
        Exit = 0
    }

    /// <summary>
    /// Entry program point
    /// </summary>
    private static void Main()
    {
        //graphs
        Dictionary<string, Graph> graphs = new();
        var commands = new Dictionary<Command, MyMethodDelegate>
        {
            { Command.ChooseGraph, ChooseGraph },
            { Command.ShowGraphs, ShowGraphs },
            { Command.AddGraph, AddGraph },
            { Command.DeleteGraph, DeleteGraph },
            { Command.CopyGraph, CopyGraph },
            { Command.CreateComplementGraph, CreateComplementGraph },
            { Command.CreateJoinGraph, CreateJoinGraph },
            { Command.CreateUnixGraph, CreateUnixGraph }
        };

        Console.WriteLine("Started...");

        while (true)
        {
            Console.WriteLine($"{(int)Command.ShowCommands} - Show commands");
            Console.WriteLine($"{(int)Command.Exit} - Exit");
            Console.WriteLine("Input command: ");

            if (!Enum.TryParse<Command>(Console.ReadLine(), out var input))
            {
                Console.WriteLine("Incorrect command!");
                continue;
            }

            if (input == Command.Exit)
                break;

            if (input == Command.ShowCommands)
            {
                ShowCommands(commands);
                continue;
            }

            if (!commands.ContainsKey(input))
            {
                Console.WriteLine("Incorrect command!");
                continue;
            }

            try
            {
                commands[input].Invoke(graphs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Choosing graph to work
    /// </summary>
    /// <param name="graphs">graphs for choose</param>
    private static void ChooseGraph(Dictionary<string, Graph> graphs)
    {
        ShowGraphs(graphs);
        Console.WriteLine("Input graph key to choose: ");
        var graphKey = Console.ReadLine();

        if (string.IsNullOrEmpty(graphKey) || !graphs.ContainsKey(graphKey))
        {
            Console.WriteLine("There are no graph with this key");
            return;
        }

        ConsoleGraphWork.Start(graphs[graphKey]);
    }

    /// <summary>
    /// Writing to console graphs
    /// </summary>
    /// <param name="graphs">graphs for writing</param>
    private static void ShowGraphs(Dictionary<string, Graph> graphs)
    {
        Console.WriteLine("Your graphs: ");

        foreach (var graph in graphs)
        {
            var orientation = graph.Value.IsOriented? "Oriented" : "Undirected";
            Console.WriteLine($"{graph.Key} - {graph.Value.Name} ({orientation})");
        }
    }

    /// <summary>
    /// Adding a new graph to param
    /// </summary>
    /// <param name="graphs">graphs for adding new graph</param>
    private static void AddGraph(Dictionary<string, Graph> graphs)
    {
        Console.WriteLine("Input new graph name: ");
        var graphName = Console.ReadLine();
        Console.WriteLine("Input new graph key");
        var graphKey = Console.ReadLine();
        Console.WriteLine("Is graph oriented? y/n");
        var graphOrientation = Console.ReadLine();


        if (string.IsNullOrEmpty(graphName) || string.IsNullOrEmpty(graphKey) || string.IsNullOrEmpty(graphOrientation) ||
            graphOrientation.ToLower() != "y" && graphOrientation.ToLower() != "n")
        {
            Console.WriteLine("Check input!");
            return;
        }

        if (graphs.ContainsKey(graphKey))
        {
            Console.WriteLine("Graph with such a key has already been added");
            return;
        }

        graphs.Add(graphKey, new Graph(graphName, graphOrientation == "y"));
        Console.WriteLine("Graph added successfully!");
    }

    /// <summary>
    /// Deleting a graph from param
    /// </summary>
    /// <param name="graphs">graphs for deleting graph</param>
    private static void DeleteGraph(Dictionary<string, Graph> graphs)
    {
        ShowGraphs(graphs);
        Console.WriteLine("Input graph key to choose: ");
        var key = Console.ReadLine();

        if (string.IsNullOrEmpty(key) || !graphs.ContainsKey(key))
        {
            Console.WriteLine("There are no graph with this key!");
            return;
        }

        graphs.Remove(key);
        Console.WriteLine("Graph removed successfully!");
    }

    /// <summary>
    /// Writing commands to console
    /// </summary>
    /// <param name="commands">commands for writing to console</param>
    private static void ShowCommands(Dictionary<Command, MyMethodDelegate> commands)
    {
        foreach (var command in commands)
            Console.WriteLine($"{command.Key} - {command.Value.Method.Name}");
    }

    /// <summary>
    /// Coping one graph from another
    /// </summary>
    /// <param name="graphs">graphs for choosing graphs</param>
    private static void CopyGraph(Dictionary<string, Graph> graphs)
    {
        ShowGraphs(graphs);
        Console.WriteLine("Select graph to copy: ");
        var graphToCopyKey = Console.ReadLine();
        Console.WriteLine("Select the source graph key: ");
        var sourceGraphKey = Console.ReadLine();

        if (string.IsNullOrEmpty(sourceGraphKey) || string.IsNullOrEmpty(graphToCopyKey) ||
            !graphs.ContainsKey(sourceGraphKey) || !graphs.ContainsKey(graphToCopyKey))
        {
            Console.WriteLine("Check input!");
            return;
        }

        graphs[graphToCopyKey] = new Graph(graphs[sourceGraphKey]);
        Console.WriteLine("Graph copied successfully!");
    }

    /// <summary>
    /// Creating a complement graph by another graph
    /// </summary>
    /// <param name="graphs">graphs for choosing graph</param>
    private static void CreateComplementGraph(Dictionary<string, Graph> graphs)
    {
        ShowGraphs(graphs);
        Console.WriteLine("Select graph key to complement: ");
        var complementGraphKey = Console.ReadLine();
        Console.WriteLine("Select the source graph key: ");
        var sourceGraphKey = Console.ReadLine();

        if (string.IsNullOrEmpty(sourceGraphKey) || string.IsNullOrEmpty(complementGraphKey) ||
            !graphs.ContainsKey(sourceGraphKey) || !graphs.ContainsKey(complementGraphKey))
        {
            Console.WriteLine("Check input!");
            return;
        }

        graphs[complementGraphKey].CreateComplementGraph(graphs[sourceGraphKey]);
        Console.WriteLine("Graph complimented successfully!");
    }

    /// <summary>
    /// Creating a joined graph by another two
    /// </summary>
    /// <param name="graphs">Graphs to choose</param>
    private static void CreateJoinGraph(Dictionary<string, Graph> graphs)
    {
        ShowGraphs(graphs);
        Console.WriteLine("Select graph key to join: ");
        var graphToJoinKey = Console.ReadLine();
        Console.WriteLine("Select first source graph: ");
        var firstSourceGraphKey = Console.ReadLine();
        Console.WriteLine("Select second source graph: ");
        var secondJoinGraphKey = Console.ReadLine();


        if (string.IsNullOrEmpty(graphToJoinKey) || string.IsNullOrEmpty(firstSourceGraphKey) || string.IsNullOrEmpty(secondJoinGraphKey) ||
            !graphs.ContainsKey(graphToJoinKey) || !graphs.ContainsKey(firstSourceGraphKey) || !graphs.ContainsKey(secondJoinGraphKey))
        {
            Console.WriteLine("Check input!");
            return;
        }

        graphs[graphToJoinKey].CreateJoinGraph(graphs[firstSourceGraphKey], graphs[secondJoinGraphKey]);
        Console.WriteLine("Graph joined successfully!");
    }

    /// <summary>
    /// Creating an unix graph by another two
    /// </summary>
    /// <param name="graphs">Graphs to choose</param>
    private static void CreateUnixGraph(Dictionary<string, Graph> graphs)
    {
        ShowGraphs(graphs);
        Console.WriteLine("Select graph key to unix: ");
        var graphToUnix = Console.ReadLine();
        Console.WriteLine("Select first source graph: ");
        var firstSourceGraphKey = Console.ReadLine();
        Console.WriteLine("Select second source graph: ");
        var secondJoinGraphKey = Console.ReadLine();


        if (string.IsNullOrEmpty(graphToUnix) || string.IsNullOrEmpty(firstSourceGraphKey) || string.IsNullOrEmpty(secondJoinGraphKey) ||
            !graphs.ContainsKey(graphToUnix) || !graphs.ContainsKey(firstSourceGraphKey) || !graphs.ContainsKey(secondJoinGraphKey))
        {
            Console.WriteLine("Check input!");
            return;
        }

        graphs[graphToUnix].CreateUnionGraph(graphs[firstSourceGraphKey], graphs[secondJoinGraphKey]);
        Console.WriteLine("Graph union successfully!");
    }
}