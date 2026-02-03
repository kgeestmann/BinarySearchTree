namespace ADS;

public class Edge : IElementWithKey
{
    public Node node;
    public int weight;

    public Edge(Node node, int weight)
    {
        this.node = node;
        this.weight = weight;
    }

    public int Key => node.Key;

    public int ChangeableKey
    {
        get { return node.ChangeableKey; }
        set { node.ChangeableKey = value; }
    }

    public Action ChangingAction
    {
        set { node.ChangingAction = value; }
    }
}