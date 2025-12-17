namespace ADS;

public class BinarySearchTree <T> where T : IElementWithKey
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
}