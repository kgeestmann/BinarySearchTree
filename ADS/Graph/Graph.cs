namespace ADS;

public class Graph
{
    public Node[] nodes;

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

    public void BFS(Node s)
    {
        foreach (var n in nodes)
        {
            n.color = Color.White;
            n.ChangeableKey = -1; 
            n.parent = null;
        }

        s.color = Color.Grey;
        s.ChangeableKey = 0;

        var q = new Queue<Node>();
        q.Enqueue(s);

        while (q.Count > 0)
        {
            var u = q.Dequeue();

            u.IterateThroughNeighbors(v =>
            {
                if (v.color == Color.White)
                {
                    v.color = Color.Grey;
                    v.ChangeableKey = u.ChangeableKey + 1;
                    v.parent = u;
                    q.Enqueue(v);
                }
            });

            u.color = Color.Black;
        }
    }

    public void DFS()
    {
        foreach (var n in nodes)
        {
            n.color = Color.White;
            n.ChangeableKey = -1; 
            n.parent = null;
        }

        foreach (var n in nodes)
        {
            if (n.color == Color.White)
                DFSVisit(n, 0);
        }
    }

    private void DFSVisit(Node n, int dist)
    {
        n.ChangeableKey = dist;
        n.color = Color.Grey;

        n.IterateThroughNeighbors(x =>
        {
            if (x.color == Color.White)
            {
                x.parent = n;
                DFSVisit(x, dist + 1);
            }
        });

        n.color = Color.Black;
    }

    public void PrintPath(Node s, Node v)
    {
        if (v == s)
        {
            Console.WriteLine(s.Key);
        }
        else if (v.parent == null)
        {
            Console.WriteLine("es gibt keinen Pfad von " + s.Key + " nach " + v.Key);
        }
        else
        {
            PrintPath(s, v.parent);
            Console.WriteLine(v.Key);
        }
    }
    
    public void Dijkstra(Node s)
    {
        var q = new MinPriorityQueue<Node>(nodes.Length);

        foreach (var v in nodes)
        {
            if (v != s) v.ChangeableKey = int.MaxValue;
            else v.ChangeableKey = 0;

            v.parent = null;
            v.color = Color.White;
            q.Insert(v);
        }
        
        for (int i = 0; i < nodes.Length; i++)
        {
            var x = q.ExtractFirst(); 

            x.IterateThroughNeighbors(y =>
            {
                int w = x.GetWeight(y);
                
                if (x.ChangeableKey != int.MaxValue &&
                    y.ChangeableKey >= x.ChangeableKey + w)
                {
                    y.ChangeableKey = x.ChangeableKey + w;
                    y.parent = x;
                }
            });
        }
    }
}