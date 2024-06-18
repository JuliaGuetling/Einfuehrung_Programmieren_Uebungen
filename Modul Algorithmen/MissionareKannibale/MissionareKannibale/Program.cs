using MissionareKannibale;
using System.Runtime.ConstrainedExecution;

class Program
{
    static void Main()
    {
        // Instanziierung Anfangssituation (3 M. und 3 K. am Westufer)
        var start = new MCState(MCState.MaxNum, MCState.MaxNum, true);

        // Schachtelung der aufzurufenden Funktionen
        static bool goalTest(MCState state) => state.GoalTest();
        static List<MCState> successors(MCState state) => state.Successors();

        // Ermittlung der Lösung mittels BFS Algorithmus 
        var solution = new BFS().Bfs(start, goalTest, successors);

        if (solution == null)
        {
            Console.WriteLine("Keine Lösung gefunden!");
        }
        else
        {
            var path = NodeToPath(solution);
            DisplaySolution(path);
        }
    }

    /// <summary>
    /// Anzeige der Zustände in Reihenfolge, die zum schnellsten zulässigen Ergebnis führen 
    /// </summary>
    /// <param name="path"></param>
    static void DisplaySolution(List<MCState> path)
    {

        if (path.Count == 0)
        {
            return;
        }

        var oldState = path[0];
        Console.WriteLine(oldState.DisplaySolution());
        Console.WriteLine(Environment.NewLine);

        for (int i = 1; i < path.Count; i++)
        {
            var currentState = path[i];
            if (currentState.Boat)
            {
                Console.WriteLine($"{oldState.EastM - currentState.EastM} Missionare und {oldState.EastC - currentState.EastC} Kannibalen vom Ostufer zum Westufer transportiert.");
            }
            else
            {
                Console.WriteLine($"{oldState.WestM - currentState.WestM} Missionare und {oldState.WestC - currentState.WestC} Kannibalen vom Westufer zum Ostufer transportiert.");
            }
            Console.WriteLine();
            Console.WriteLine(currentState.DisplaySolution());
            Console.WriteLine(Environment.NewLine);
            oldState = currentState;
        }
    }

    /// <summary>
    /// Erstellen der aufeinanderfolgenden Zustände, die zur schnellsten zulässigen Lösung führen
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    static List<MCState> NodeToPath(Node<MCState> node)
    {
        List<MCState> path = [node.State];

        while (node.Parent != null)
        {
            node = node.Parent;
            path.Add(node.State);
        }
        path.Reverse();
        return path;
    }
}
