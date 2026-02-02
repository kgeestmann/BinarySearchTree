namespace ADS;

public interface IElementWithKey
{
    int Key { get; }
    int ChangeableKey { get; set; }
    Action ChangingAction { set; }
}


public class Person : IElementWithKey
{
    public int Key { get; }
    public string Name { get; set; }

    private int _changeableKey;
    private Action _changingKeyAction = () => { };
    
    public int ChangeableKey
    {
        get { return _changeableKey; }
        set
        {
            _changeableKey = value;
            _changingKeyAction();
        }
    }

    public Action ChangingAction
    {
        set { _changingKeyAction = value; }
    }
    
    public Person(int key, string name)
    {
        Key = key;
        Name = name;
        _changeableKey = key;
    }
}


public class Test : IElementWithKey
{
    public int Key { get; }

    private int _changeableKey;
    private Action _changingAction = () => { };

    public int ChangeableKey
    {
        get { return _changeableKey; }
        set
        {
            _changeableKey = value;
            _changingAction();
        }
    }

    public Action ChangingAction
    {
        set { _changingAction = value; }
    }

    public Test(int key, int changeableKey)
    {
        Key = key;
        _changeableKey = changeableKey;
    }

    public override string ToString()
    {
        return "Key=" + Key + ", ChangeableKey=" + ChangeableKey;
    }
}

