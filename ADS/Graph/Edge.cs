namespace ADS;

public class Edge : IElementWithKey
{
    // Zielknoten der Kante
    public Node Node { get; set; }

    // Kantengewicht (für Dijkstra)
    public int Weight { get; set; }

    public Edge(Node node, int weight)
    {
        Node = node;
        Weight = weight;
    }

    // Key für BST: eindeutig über Zielknoten-ID
    public int Key
    {
        get { return Node.Key; }
    }

    // Weiterleitung für PriorityQueue / Dijkstra
    public int ChangeableKey
    {
        get { return Node.ChangeableKey; }
        set { Node.ChangeableKey = value; }
    }

    // Action wird an den Node weitergereicht
    public Action ChangingAction
    {
        set { Node.ChangingAction = value; }
    }
}