using esinFirstTask;

class Program
{
    private delegate void MyMethodDelegate();
    private Dictionary<string, Graph> graphs;

    private Dictionary<string, MyMethodDelegate> _commands; 
    
    static void Main(string[] args)
    {
        _commands = new Dictionary<string, MyMethodDelegate>
        {
            { "1", ChooseGraph },
            { "2", ShowGraphs },
            { "3", AddGraph },
            { "4", DeleteGraph }
        };
        Console.WriteLine("Input 'break' to exit");
        Console.WriteLine("Started...");

        var input = "";
        while (input != "break")
        {
            Console.WriteLine("Input command: ");
            input = Console.ReadLine();

            switch (input)
            {
                case "1":
            }
        }
    }
    private void ChooseGraph()
    {
        ShowGraphs();
        Console.WriteLine("Input graph key to choose: ");
        var key = Console.ReadLine();
    }

    private void ShowGraphs()
    {
        foreach (var graph in graphs)
            Console.WriteLine($"{graph.Key} - {graph.Value.Name}");
    }

    private void AddGraph()
    {
        Console.WriteLine("Input new graph name: ");
        var graphName = Console.ReadLine();
    }

    private void DeleteGraph()
    {
        ShowGraphs();
        Console.WriteLine("Input graph key to choose: ");
        var key = Console.ReadLine();

        if (string.IsNullOrEmpty(key))
        {
            Console.WriteLine("Graph key can not be empty!");
            return;
        }

        if (!graphs.ContainsKey(key))
        {
            Console.WriteLine("There are no graph with this key!");
            return;
        }

        graphs.Remove(key);
    }
}