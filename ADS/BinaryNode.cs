namespace ADS;

public interface IBinaryNode<T> where T : IElementWithKey
{
    public bool IsEmptyNode();
}

public class EmptyNode<T> : IBinaryNode<T> where T : IElementWithKey
{
    public bool IsEmptyNode() => true;
}

public class EmptyLeaf<T> : IBinaryNode<T> where T : IElementWithKey
{
    public IBinaryNode<T> Parent { get; set; }
    
    public bool IsEmptyNode() => true;
    
    public EmptyLeaf(IBinaryNode<T> parent)
    {
        Parent = parent;
    }
}

public class BinaryNode<T> : IBinaryNode<T> where T : IElementWithKey
{
    public T Element {get; set;}
    public IBinaryNode<T> Left {get; set;}
    public IBinaryNode<T> Right {get; set;}
    public IBinaryNode<T> Parent {get; set;}
    
    public bool IsEmptyNode() => false;

    public BinaryNode(T element, IBinaryNode<T> left, IBinaryNode<T> right, IBinaryNode<T> parent)
    {
        Element = element;
        Left = left;
        Right = right;
        Parent = parent;
    }

    public BinaryNode(T element, IBinaryNode<T> parent)
    {
        Element = element;
        Parent = parent;
        Left = new EmptyLeaf<T>(this);
        Right = new EmptyLeaf<T>(this);
    }

    public BinaryNode(T element)
    {
        Element = element;
        Left = new EmptyLeaf<T>(this);
        Right = new EmptyLeaf<T>(this);
        Parent = new EmptyNode<T>();
    }
}

