namespace ADS;

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