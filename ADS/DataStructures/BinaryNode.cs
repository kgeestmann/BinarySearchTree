namespace ADS;

public interface IBinaryNode<T> where T : IElementWithKey
{
    // Sentinel-Check: true bei EmptyNode/EmptyLeaf, false bei BinaryNode
    bool IsEmptyNode();
}

public class EmptyNode<T> : IBinaryNode<T> where T : IElementWithKey
{
    // Repräsentiert: "kein Knoten" (z.B. Root-Parent)
    public bool IsEmptyNode() => true;
}

public class EmptyLeaf<T> : IBinaryNode<T> where T : IElementWithKey
{
    // Repräsentiert: "leeres Kind" (Blatt-Sentinel) mit Parent-Referenz
    private IBinaryNode<T> _parent;

    public IBinaryNode<T> Parent
    {
        get { return _parent; }
        set { _parent = value; }
    }

    public bool IsEmptyNode() => true;

    public EmptyLeaf(IBinaryNode<T> parent)
    {
        Parent = parent;
    }
}

public class BinaryNode<T> : IBinaryNode<T> where T : IElementWithKey
{
    // Normaler Baumknoten: Element + Left/Right + Parent
    public T Element { get; set; }
    public IBinaryNode<T> Left { get; set; }
    public IBinaryNode<T> Right { get; set; }
    public IBinaryNode<T> Parent { get; set; }

    public bool IsEmptyNode() => false;

    // Voll-Konstruktor (z.B. beim Umhängen/Transplant hilfreich)
    public BinaryNode(T element, IBinaryNode<T> left, IBinaryNode<T> right, IBinaryNode<T> parent)
    {
        Element = element;
        Left = left;
        Right = right;
        Parent = parent;
    }

    // Neuer Knoten unter einem Parent (Children starten als EmptyLeaf)
    public BinaryNode(T element, IBinaryNode<T> parent)
    {
        Element = element;
        Parent = parent;
        Left = new EmptyLeaf<T>(this);
        Right = new EmptyLeaf<T>(this);
    }

    // Root-Knoten (Parent = EmptyNode)
    public BinaryNode(T element)
    {
        Element = element;
        Left = new EmptyLeaf<T>(this);
        Right = new EmptyLeaf<T>(this);
        Parent = new EmptyNode<T>();
    }
}