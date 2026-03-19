namespace ADS;

public class Program
{
    public static void Main()
    {
        // ============================================================
        // 1) BINARY SEARCH TREE (Person)
        // ============================================================

        var tree = new BinarySearchTree<Person>();

        // Insert (Key bestimmt Position im Baum)
        tree.Insert(new Person(5, "Phillip"));
        tree.Insert(new Person(3, "Lars"));
        tree.Insert(new Person(7, "Uta"));
        tree.Insert(new Person(1, "Dennis"));
        tree.Insert(new Person(4, "Anna"));
        tree.Insert(new Person(6, "Tom"));
        tree.Insert(new Person(8, "Eva"));

        Console.WriteLine("\nInOrder (sortiert nach Key):");
        tree.InOrder(p =>
            Console.WriteLine("Key=" + p.Key + ", Name=" + p.Name)
        );

        // TreeMinimum
        try
        {
            var min = tree.TreeMinimum();
            Console.WriteLine("\nTreeMinimum: " + min.Key + " (" + min.Name + ")");
        }
        catch (Exception e)
        {
            Console.WriteLine("TreeMinimum EX: " + e.Message);
        }

        // Find
        try
        {
            var f = tree.Find(4);
            Console.WriteLine("\nFind(4): " + f.Name);
        }
        catch (Exception e)
        {
            Console.WriteLine("Find EX: " + e.Message);
        }

        // Delete
        try
        {
            var d = tree.Delete(3);
            Console.WriteLine("\nDelete(3): " + d.Name);
        }
        catch (Exception e)
        {
            Console.WriteLine("Delete EX: " + e.Message);
        }

        Console.WriteLine("\nInOrder nach Delete:");
        tree.InOrder(p =>
            Console.WriteLine("Key=" + p.Key + ", Name=" + p.Name)
        );

        // ============================================================
        // PRIORITY QUEUE (ChangeableKey-Mechanismus)
        // ============================================================

        var pq = new MaxPriorityQueue<Test>(20);
        Test[] arr = new Test[10];

        // Insert: ChangeableKey = 1..10
        for (int i = 0; i < 10; i++)
        {
            arr[i] = new Test(i + 1, i + 1);
            pq.Insert(arr[i]);
        }

        Console.WriteLine("\nFirst (soll 10 sein): " + pq.First().ChangeableKey);

        // ChangeableKey erhöhen -> Heap repariert sich automatisch
        arr[0].ChangeableKey = 100;
        Console.WriteLine("First nach Erhöhung (soll 100 sein): " + pq.First().ChangeableKey);

        Console.WriteLine("\nExtractFirst (absteigend):");
        while (pq.Size > 0)
        {
            Console.WriteLine(pq.ExtractFirst());
        }
        
        // ============================================================
        // GRAPH
        // ============================================================

        var g = new Graph(7); // Node 0 wird nicht benutzt

        g.AddEdge(1, 2, 6);
        g.AddEdge(1, 3, 4);
        g.AddEdge(1, 4, 3);

        g.AddEdge(2, 5, 7);
        g.AddEdge(2, 6, 2);

        g.AddEdge(3, 6, 3);

        g.AddEdge(6, 4, 6);

        // ---------- BFS ----------
        Console.WriteLine("\n--- BFS ab 1 ---");
        g.BFS(1);

        for (int i = 1; i <= 6; i++)
        {
            var n = g.Nodes[i];
            string parent = n.Parent == null ? "-" : n.Parent.ID.ToString();
            Console.WriteLine("Node " + n.ID +
                              ": Dist=" + n.Distance +
                              ", Parent=" + parent);
        }

        Console.WriteLine("Pfad BFS 1 -> 4:");
        g.PrintPath(g.Nodes[1], g.Nodes[4]);

        // ---------- DFS ----------
        Console.WriteLine("\n--- DFS ab 1 ---");
        g.DFS(1);

        for (int i = 1; i <= 6; i++)
        {
            var n = g.Nodes[i];
            string parent = n.Parent == null ? "-" : n.Parent.ID.ToString();
            Console.WriteLine("Node " + n.ID +
                              ": Dist=" + n.Distance +
                              ", Parent=" + parent);
        }

        Console.WriteLine("Pfad DFS 1 -> 4:");
        g.PrintPath(g.Nodes[1], g.Nodes[4]);

        // ---------- Dijkstra ----------
        Console.WriteLine("\n--- Dijkstra ab 1 ---");
        g.Dijkstra(1);

        for (int i = 1; i <= 6; i++)
        {
            var n = g.Nodes[i];
            string parent = n.Parent == null ? "-" : n.Parent.ID.ToString();
            Console.WriteLine("Node " + n.ID +
                              ": Dist=" + n.Distance +
                              ", Parent=" + parent);
        }

        Console.WriteLine("Pfad Dijkstra 1 -> 4:");
        g.PrintPath(g.Nodes[1], g.Nodes[4]);
    }
}
