namespace ADS;

public interface IElementWithKey
{
    int Key { get; }

    int ChangeableKey { get; set; }

    Action ChangingKeyAction { set; }
}


public class Person : IElementWithKey
{
    public int Key { get; }
    public string Name { get; set; }

    private int _changeableKey;
    private Action _changingKeyAction;
    
    public int ChangeableKey
    {
        get { return _changeableKey; }
        set
        {
            _changeableKey = value;
            _changingKeyAction();
        }
    }

    public Action ChangingKeyAction
    {
        set { _changingKeyAction = value; }
    }
    
    public Person(int key, string name)
    {
        Key = key;
        Name = name;
    }
}