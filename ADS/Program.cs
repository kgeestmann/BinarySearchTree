namespace ADS;

public class Program
{
    public static void Main()
    {
        // Binary Search Tree Test
        var tree = new BinarySearchTree<Person>();

        // Insert nodes
        tree.Insert(new Person(5, "Phillip"));
        tree.Insert(new Person(3, "Lars"));
        tree.Insert(new Person(7, "Uta"));
        tree.Insert(new Person(1, "Dennis"));
        tree.Insert(new Person(4, "Anna"));
        tree.Insert(new Person(6, "Tom"));
        tree.Insert(new Person(8, "Eva"));

        Console.WriteLine("=== BST InOrder Traversal ===");
        tree.InOrder(p => Console.WriteLine($"Key: {p.Key}, Name: {p.Name}"));

        var min = tree.TreeMinimum();
        if (min != null)
            Console.WriteLine($"\nBST Minimum element: Key = {min.Key}, Name = {min.Name}");

        int findKey = 4;
        var found = tree.Find(findKey);
        Console.WriteLine(found != null
            ? $"\nBST Found element with Key={findKey}: Name = {found.Name}"
            : $"\nBST Element with Key={findKey} not found");

        int deleteKey = 3;
        var deleted = tree.Delete(deleteKey);
        Console.WriteLine(deleted != null
            ? $"\nBST Deleted element with Key={deleteKey}: Name = {deleted.Name}"
            : $"\nBST Element with Key={deleteKey} not found for deletion");

        Console.WriteLine("\n=== BST InOrder Traversal After Deletion ===");
        tree.InOrder(p => Console.WriteLine($"Key: {p.Key}, Name: {p.Name}"));
        
        // MaxPriorityQueue Test
        Console.WriteLine("\n=== MaxPriorityQueue Test ===");
        var maxQueue = new MaxPriorityQueue<Person>(10);
        maxQueue.Insert(new Person(5, "Phillip"));
        maxQueue.Insert(new Person(3, "Lars"));
        maxQueue.Insert(new Person(7, "Uta"));
        maxQueue.Insert(new Person(1, "Dennis"));

        Console.WriteLine("Max element (First):");
        var maxFirst = maxQueue.First(); // einfach aufrufen
        Console.WriteLine($"Key: {maxFirst.Key}, Name: {maxFirst.Name}");

        Console.WriteLine("Extract elements from MaxPriorityQueue:");
        for (int i = 0; i < 4; i++)
        {
            var extracted = maxQueue.ExtractFirst(); // direkt aufrufen
            Console.WriteLine($"Key: {extracted.Key}, Name: {extracted.Name}");
        }
        
        // MinPriorityQueue Test
        Console.WriteLine("\n=== MinPriorityQueue Test ===");
        var minQueue = new MinPriorityQueue<Person>(10);
        minQueue.Insert(new Person(5, "Phillip"));
        minQueue.Insert(new Person(3, "Lars"));
        minQueue.Insert(new Person(7, "Uta"));
        minQueue.Insert(new Person(1, "Dennis"));

        Console.WriteLine("Min element (First):");
        var minFirst = minQueue.First();
        Console.WriteLine($"Key: {minFirst.Key}, Name: {minFirst.Name}");

        Console.WriteLine("Extract elements from MinPriorityQueue:");
        for (int i = 0; i < 4; i++)
        {
            var extracted = minQueue.ExtractFirst();
            Console.WriteLine($"Key: {extracted.Key}, Name: {extracted.Name}");
        }
        
        Console.WriteLine("\n=== 8.3 Test mit Test-Klasse (MaxPriorityQueue) ===");

        var pq = new MaxPriorityQueue<Test>(20);
        Test[] arr = new Test[10];

// 10 Objekte mit ChangeableKey 1..10
        for (int i = 0; i < 10; i++)
        {
            arr[i] = new Test(i + 1, i + 1);
            pq.Insert(arr[i]);
        }

        Console.WriteLine("First (soll 10 sein): " + pq.First().ChangeableKey);

// ChangeableKey verbessern → Action → IncreaseKey
        arr[0].ChangeableKey = 100;

        Console.WriteLine("First nach Erhöhung (soll 100 sein): " + pq.First().ChangeableKey);

        Console.WriteLine("\nExtractFirst:");
        while (true)
        {
            try
            {
                Console.WriteLine(pq.ExtractFirst());
            }
            catch
            {
                break;
            }
        }

    }
}