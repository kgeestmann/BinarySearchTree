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

    public T? Delete(int key)
    {
        BinaryNode<T>? node = FindNode(key);
        if (node == null)
        {
            return default;
        }
        
        T deletedElement = node.Element;
        
        // kein linkes Kind
        if (node.Left is not BinaryNode<T>)
        {
            Transplant(node, node.Right);
        }
        // kein rechtes Kind
        else if (node.Right is not BinaryNode<T>)
        {
            Transplant(node, node.Left);
        }
        // zwei Kinder
        else
        {
            BinaryNode<T> successor = MinimumNode(node.Right)!;

            if (successor.Parent != node)
            {
                Transplant(successor, successor.Right);
                successor.Right = node.Right;
                if (successor.Right is BinaryNode<T> rightChild)
                    rightChild.Parent = successor;
            }
            
            Transplant(node, successor);
            successor.Left = node.Left;
            if (successor.Left is BinaryNode<T> leftChild)
                leftChild.Parent = successor;
        }
        return deletedElement;
    }

    // Knoten mit dem Key suchen
    private BinaryNode<T>? FindNode(int key)
    {
        IBinaryNode<T> current = root;

        while (current is BinaryNode<T> node)
        {
            if (key < node.Element.Key)
            {
                current = node.Left;
            }
            else if (key > node.Element.Key)
            {
                current = node.Right;
            }
            else
            {
                return node;}
        }
        return null;
    }

    // Knoten u mit v ersetzen
    private void Transplant(BinaryNode<T> u, IBinaryNode<T> v)
    {
        if (u.Parent is EmptyNode<T>)
        {
            root = v;
        }
        else if (u.Parent is BinaryNode<T> parent)
        {
            if (parent.Left == u)
            {
                parent.Left = v;
            }
            else
            {
                parent.Right = v;
            }
        }

        if (v is BinaryNode<T> node)
        {
            node.Parent = u.Parent;
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

    private BinaryNode<T>? MinimumNode(IBinaryNode<T> node)
    {
        if (node is not BinaryNode<T> current)
            return null;

        while (current.Left is BinaryNode<T> left)
        {
            current = left;
        }
        
        return current;
    }

    public T? TreeMinimum()
    {
        var minNode = MinimumNode(root);

        if (minNode is null)
        {
            return default;
        }
        return minNode.Element;
    }

    public T? Find(int key)
    {
        IBinaryNode<T> current = root;

        while (current is BinaryNode<T> node)
        {
            if (key < node.Element.Key)
            {
                current = node.Left;
            }
            else if (key > node.Element.Key)
            {
                current = node.Right;
            }
            else
            {
                return node.Element;
            }
        }
        
        return default;
    }

    public override string ToString()
    {
        var elements = new List<string>();
        InOrder(T => elements.Add(elements.ToString() ?? ""));
        return string.Join(", ", elements);
    }
}
