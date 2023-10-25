namespace esinFirstTask;

internal static class Program
{
    private delegate void MyMethodDelegate(Dictionary<string, Graph> graphs);

    private static void Main()
    {
        Dictionary<string, Graph> graphs = new();
        var commands = new Dictionary<string, MyMethodDelegate>
        {
            { "1", ChooseGraph },
            { "2", ShowGraphs },
            { "3", AddGraph },
            { "4", DeleteGraph },
            { "5", CopyGraph}
        };

        Console.WriteLine("Started...");
        
        while (true)
        {
            Console.WriteLine("5 - show commands");
            Console.WriteLine("0 - exit");
            Console.WriteLine("Input command: ");
            var input = Console.ReadLine();

            if (input == "0")
                break;
            
            if (input == "5")
            {
                ShowCommands(commands);
                continue;
            }

            if (string.IsNullOrEmpty(input) || !commands.ContainsKey(input))
            {
                Console.WriteLine("Incorrect command!");
                continue;
            }

            commands[input].Invoke(graphs);
        }
    }

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

    private static void ShowGraphs(Dictionary<string, Graph> graphs)
    {
        Console.Write("Your graphs: ");

        foreach (var graph in graphs)
        {
            var orientation = graph.Value.IsOriented? "Oriented" : "Undirected";
            Console.WriteLine($"{graph.Key} - {graph.Value.Name} ({orientation})");
        }
    }

    private static void AddGraph(Dictionary<string, Graph> graphs)
    {
        Console.WriteLine("Input new graph name: ");
        var graphName = Console.ReadLine();
        Console.WriteLine("Input new graph key");
        var graphKey = Console.ReadLine();
        Console.WriteLine("Is graph oriented? y/n");
        var graphOrientation = Console.ReadLine();


        if (string.IsNullOrEmpty(graphName) || string.IsNullOrEmpty(graphKey) || string.IsNullOrEmpty(graphOrientation))
        {
            Console.WriteLine("Check input!");
            return;
        }

        if (graphOrientation.ToLower() != "y" && graphOrientation.ToLower() != "n")
        {
            Console.WriteLine("Check input!");
            return;
        }

        graphs.Add(graphKey, new Graph(graphName, graphOrientation == "y"));
        Console.WriteLine("Graph added successfully!");
    }

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
    
    private static void ShowCommands(Dictionary<string, MyMethodDelegate> commands)
    {
        foreach (var command in commands)
            Console.WriteLine($"{command.Key} - {command.Value.Method.Name}");
    }    
    
    private static void CopyGraph(Dictionary<string, Graph> graphs)
    {
        ShowGraphs(graphs);
        Console.WriteLine("Select the source graph key: ");
        var sourceGraphKey = Console.ReadLine();
        Console.WriteLine("Select graph to copy: ");
        var graphToCopyKey = Console.ReadLine();

        if (string.IsNullOrEmpty(sourceGraphKey) || string.IsNullOrEmpty(graphToCopyKey) ||
            !graphs.ContainsKey(sourceGraphKey) || !graphs.ContainsKey(graphToCopyKey))
        {
            Console.WriteLine("Check input!");
            return;
        }

        graphs[graphToCopyKey] = new Graph(graphs[sourceGraphKey]);
        
        Console.WriteLine("Graph copied successfully!");
    }
}