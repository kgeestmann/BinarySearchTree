namespace ADS;

public class BinarySearchTree<T> where T : IElementWithKey
{
    private IBinaryNode<T> root;

    public BinarySearchTree(T element)
    {
        root = new BinaryNode<T>(element);
    }

    public BinarySearchTree()
    {
        root = new EmptyNode<T>();
    }

    public void Insert(T element)
    {
        if (root is EmptyNode<T>)
        {
            root = new BinaryNode<T>(element);
            return;
        }

        IBinaryNode<T> current = root;
        IBinaryNode<T> previous = root;

        while (current is BinaryNode<T> node)
        {
            previous = current;

            if (element.Key < node.Element.Key)
            {
                current = node.Left;
            }
            else if (element.Key > node.Element.Key)
                current = node.Right;
            else
            {
                node.Element = element;
                return;
            }
        }

        if (current is EmptyLeaf<T> leaf)
        {
            BinaryNode<T> newNode = new BinaryNode<T>(element, leaf.Parent);

            if (previous is BinaryNode<T> parentNode)
            {
                if (element.Key < parentNode.Element.Key)
                {
                    parentNode.Left = newNode;
                }
                else
                {
                    parentNode.Right = newNode;
                }
            }
        }
    }

    public void InOrder(Action<T> action)
    {
        InOrderRecursive(root, action);
    }

    // InOrder: Left -> Middle -> Right
    private void InOrderRecursive(IBinaryNode<T> node, Action<T> action)
    {
        if (node is BinaryNode<T> binaryNode)
        {
            InOrderRecursive(binaryNode.Left, action);
            action(binaryNode.Element);
            InOrderRecursive(binaryNode.Right, action);
        }
    }

    public void PreOrder(Action<T> action)
    {
        PreOrderRecursive(root, action);
    }

    // PreOrder: Middle -> Left -> Right
    private void PreOrderRecursive(IBinaryNode<T> node, Action<T> action)
    {
        if (node is BinaryNode<T> binaryNode)
        {
            action(binaryNode.Element);
            PreOrderRecursive(binaryNode.Left, action);
            PreOrderRecursive(binaryNode.Right, action);
        }
    }

    public void PostOrder(Action<T> action)
    {
        PostOrderRecursive(root, action);
    }

    // PostOrder: Left -> Right -> Middle
    private void PostOrderRecursive(IBinaryNode<T> node, Action<T> action)
    {
        if (node is BinaryNode<T> binaryNode)
        {
            PostOrderRecursive(binaryNode.Left, action);
            PostOrderRecursive(binaryNode.Right, action);
            action(binaryNode.Element);
        }
    }

    public override string ToString()
    {
        var elements = new List<string>();
        InOrder(T => elements.Add(elements.ToString() ?? ""));
        return string.Join(", ", elements);
    }
}
