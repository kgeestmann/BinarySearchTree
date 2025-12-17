namespace ADS;

public interface IElementWithKey
{
    int Key {get;}
}

public class Person : IElementWithKey
{
    public int Key {get;}
    public string Name {get; set;}
    
    public Person(int key, string name)
    {
        Key = key;
        Name = name;
    }
}