namespace ADS;

public class Programm
{
    public static void Main()
    {
        var tree = new BinarySearchTree<Person>();
        
        tree.Insert(new Person(5, "Phillip"));
        tree.Insert(new Person(3, "Lars"));
        tree.Insert(new Person(7, "Uta"));
        tree.Insert(new Person(1, "Dennis"));
    }
}