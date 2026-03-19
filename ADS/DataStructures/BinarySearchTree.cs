using System.Runtime.InteropServices;

namespace ADS;

public class BinarySearchTree<T> where T : IElementWithKey
{
    private IBinaryNode<T> root;

    // Root-Accessor (read-only)
    public IBinaryNode<T> Root
    {
        get { return root; }
    }

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
        // leerer Baum -> neues Root
        if (root is EmptyNode<T>)
        {
            root = new BinaryNode<T>(element);
            return;
        }

        IBinaryNode<T> current = root;
        IBinaryNode<T> previous = root;

        // runterlaufen bis EmptyLeaf
        while (current is BinaryNode<T> node)
        {
            previous = current;

            if (element.Key < node.Element.Key)
            {
                current = node.Left;
            }
            else if (element.Key > node.Element.Key)
            {
                current = node.Right;
            }
            else
            {
                // gleicher Key -> Element ersetzen
                node.Element = element;
                return;
            }
        }

        // EmptyLeaf erreicht -> neuen BinaryNode einhängen
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

    public T Delete(int key)
    {
        // 3 Fälle (0/1/2 Kinder) + Transplant + Successor
        BinaryNode<T>? node = FindNode(key);
        if (node == null)
        {
            throw new InvalidOperationException("Delete: Key nicht gefunden: " + key);
        }

        T deletedElement = node.Element;

        // Fall 1: kein linkes Kind -> ersetze node durch Right
        if (node.Left is not BinaryNode<T>)
        {
            Transplant(node, node.Right);
        }
        // Fall 2: kein rechtes Kind -> ersetze node durch Left
        else if (node.Right is not BinaryNode<T>)
        {
            Transplant(node, node.Left);
        }
        // Fall 3: zwei Kinder -> successor = Minimum(rechter Teilbaum)
        else
        {
            BinaryNode<T> successor = MinimumNode(node.Right)!;

            // successor ist nicht direktes rechtes Kind -> erst successor "hochholen"
            if (successor.Parent != node)
            {
                Transplant(successor, successor.Right);

                successor.Right = node.Right;
                if (successor.Right is BinaryNode<T> rightChild)
                    rightChild.Parent = successor;
            }

            // node durch successor ersetzen
            Transplant(node, successor);

            successor.Left = node.Left;
            if (successor.Left is BinaryNode<T> leftChild)
                leftChild.Parent = successor;
        }

        return deletedElement;
    }

    // Suche nach Knoten (liefert Node oder null)
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
                return node;
            }
        }

        return null;
    }

    // Transplant(u, v): ersetzt Teilbaum u durch v (inkl. Parent-Update von v)
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

    // MinimumNode: gehe links runter bis kein BinaryNode mehr
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

    public T TreeMinimum()
    {
        var minNode = MinimumNode(root);

        if (minNode is null)
            throw new InvalidOperationException("TreeMinimum: Baum ist leer.");

        return minNode.Element;
    }

    public T Find(int key)
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

        throw new InvalidOperationException("Find: Key nicht gefunden: " + key);
    }

    public override string ToString()
    {
        var elements = new List<string>();
        InOrder(e => elements.Add(e.Key.ToString()));
        return string.Join(", ", elements);
    }
}