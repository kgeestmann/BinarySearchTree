namespace ADS;

public enum Color
{
    White,
    Grey,
    Black
}

public class Node : IElementWithKey
{
    public int id { get; }
    public Node parent;
    public Color color;
    private int distance; 
    private Action _changingAction = () => { };
    private BinarySearchTree<Edge> adjacent;

    // IElementWithKey
    public int Key => id;

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

    public Node(int id)
    {
        this.id = id;
        parent = null;
        color = Color.White;
        distance = int.MaxValue;
        adjacent = new BinarySearchTree<Edge>();
    }
    
    public void AddEdge(Node other, int weight)
    {
        adjacent.Insert(new Edge(other, weight));
    }
    
    public Edge DeleteNeighbor(Node neighbor)
    {
        return adjacent.Delete(neighbor.id);
    }

    public void IterateThroughNeighbors(Action<Node> action)
    {
        adjacent.InOrder(e => action(e.node));
    }

    public int GetWeight(Node neighbor)
    {
        var e = adjacent.Find(neighbor.id);
        if (e == null)
            throw new InvalidOperationException("Keine Kante zu diesem Nachbarn vorhanden.");
        return e.weight;
    }

    public override string ToString()
    {
        return "Node(" + id + ")";
    }
}