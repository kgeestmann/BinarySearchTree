namespace ADS;

public interface IElementWithKey
{
    int Key { get; }
    int ChangeableKey { get; set; }
    Action ChangingAction { set; }
}

