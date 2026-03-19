namespace ADS;

public class Graph
{
    private Node[] nodes;

    public Node[] Nodes
    {
        get { return nodes; }
    }

    public Graph(int n)
    {
        nodes = new Node[n];
        for (int i = 0; i < n; i++)
            nodes[i] = new Node(i);
    }

    public void AddEdge(int fromId, int toId, int weight)
    {
        nodes[fromId].AddEdge(nodes[toId], weight);
    }

    // ===== BFS(int id) =====
    public void BFS(int id)
    {
        BFS(nodes[id]);
    }

    // interner BFS wie im Pseudocode (mit Properties)
    private void BFS(Node s)
    {
        foreach (var n in nodes)
        {
            n.Color = Color.White;
            n.Distance = -1;
            n.Parent = null;
        }

        s.Color = Color.Grey;
        s.Distance = 0;

        var q = new Queue<Node>();
        q.Enqueue(s);

        while (q.Count > 0)
        {
            var u = q.Dequeue();

            u.IterateThroughNeighbors(v =>
            {
                if (v.Color == Color.White)
                {
                    v.Color = Color.Grey;
                    v.Distance = u.Distance + 1;
                    v.Parent = u;
                    q.Enqueue(v);
                }
            });

            u.Color = Color.Black;
        }
    }

    // ===== DFS(int id) =====
    public void DFS(int id)
    {
        foreach (var n in nodes)
        {
            n.Color = Color.White;
            n.Distance = -1;
            n.Parent = null;
        }

        // Start wie gefordert bei nodes[id]
        DFSVisit(nodes[id], 0);

        // Optional (wie in der Vorlesung): restliche Komponenten auch besuchen
        foreach (var n in nodes)
        {
            if (n.Color == Color.White)
                DFSVisit(n, 0);
        }
    }

    private void DFSVisit(Node n, int dist)
    {
        n.Distance = dist;
        n.Color = Color.Grey;

        n.IterateThroughNeighbors(x =>
        {
            if (x.Color == Color.White)
            {
                x.Parent = n;
                DFSVisit(x, dist + 1);
            }
        });

        n.Color = Color.Black;
    }

    // ===== PrintPath(Node s, Node v) =====
    public void PrintPath(Node s, Node v)
    {
        if (v == s)
        {
            Console.WriteLine(s.Key);
        }
        else if (v.Parent == null)
        {
            Console.WriteLine("es gibt keinen Pfad von " + s.Key + " nach " + v.Key);
        }
        else
        {
            PrintPath(s, v.Parent);
            Console.WriteLine(v.Key);
        }
    }

    // ===== Dijkstra(int id) =====
    public void Dijkstra(int id)
    {
        Dijkstra(nodes[id]);
    }

    // interner Dijkstra wie im Pseudocode (MinPriorityQueue über Distance/ChangeableKey)
    private void Dijkstra(Node s)
    {
        var q = new MinPriorityQueue<Node>(nodes.Length);

        foreach (var v in nodes)
        {
            if (v != s) v.Distance = int.MaxValue;
            else v.Distance = 0;

            v.Parent = null;
            v.Color = Color.White; // nicht zwingend für Dijkstra, aber ok
            q.Insert(v);
        }

        // wie im Pseudocode: solange Queue nicht leer
        while (q.Size > 0)
        {
            var x = q.ExtractFirst();

            x.IterateThroughNeighbors(y =>
            {
                int w = x.GetWeight(y);

                if (x.Distance != int.MaxValue &&
                    y.Distance >= x.Distance + w)
                {
                    y.Distance = x.Distance + w; // triggert ChangingAction -> Heap steigt an
                    y.Parent = x;
                }
            });
        }
    }
}