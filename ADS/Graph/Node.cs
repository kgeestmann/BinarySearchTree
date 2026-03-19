namespace ADS;

public enum Color
{
    White,
    Grey,
    Black
}

public class Node : IElementWithKey
{
    // interne ID des Knotens
    private int id;

    // ===== Accessoren =====
    public int ID
    {
        get { return id; }
    }

    // IElementWithKey verlangt Key
    public int Key
    {
        get { return id; }
    }

    // Parent im BFS/DFS/Dijkstra-Baum
    public Node Parent { get; set; }

    // Farbe für BFS/DFS
    public Color Color { get; set; }

    // Distance ist Alias für ChangeableKey
    public int Distance
    {
        get { return ChangeableKey; }
        set { ChangeableKey = value; }
    }

    // Adjazenzliste als BST von Edges
    public BinarySearchTree<Edge> Adjacent { get; set; }

    // ===== PriorityQueue / Dijkstra =====
    private int distance;
    private Action _changingAction = () => { };

    // Wird von PQ genutzt (Relax -> Heap reparieren)
    public int ChangeableKey
    {
        get { return distance; }
        set
        {
            distance = value;
            _changingAction();
        }
    }

    public Action ChangingAction
    {
        set { _changingAction = value; }
    }

    // ===== Konstruktor =====
    public Node(int id)
    {
        this.id = id;
        Parent = null;
        Color = Color.White;
        distance = int.MaxValue;
        Adjacent = new BinarySearchTree<Edge>();
    }

    // ===== Graph-Operationen =====

    // gerichtete Kante hinzufügen
    public void AddEdge(Node other, int weight)
    {
        Adjacent.Insert(new Edge(other, weight));
    }

    // Nachbarn löschen (über Ziel-ID)
    public Edge DeleteNeighbor(Node neighbor)
    {
        return Adjacent.Delete(neighbor.ID);
    }

    // Iteration über alle Nachbarn (InOrder über BST)
    public void IterateThroughNeighbors(Action<Node> action)
    {
        Adjacent.InOrder(e => action(e.Node));
    }

    // Gewicht einer Kante abfragen
    public int GetWeight(Node neighbor)
    {
        var e = Adjacent.Find(neighbor.ID);
        if (e == null)
            throw new InvalidOperationException(
                "Keine Kante zu diesem Nachbarn vorhanden."
            );

        return e.Weight;
    }

    public override string ToString()
    {
        return "Node(" + ID + ")";
    }
}