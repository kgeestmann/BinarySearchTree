namespace ADS;

public enum MaxMin
{
    Max,
    Min
}

public class MaxMinPriorityQueue<T> where T : IElementWithKey
{
    private T[] data;
    private int size;
    private MaxMin mode;
    
    public MaxMinPriorityQueue(int capacity, MaxMin mode)
    {
        data = new T[capacity];
        size = 0;
        this.mode = mode;
    }

    public bool Less(T object1, T object2)
    {
        return object1.ChangeableKey < object2.ChangeableKey;
    }

    public bool Greater(T object1, T object2)
    {
        return object1.ChangeableKey > object2.ChangeableKey;
    }

    private void Heapify(int index)
    {
        int left = 2 * index + 1;
        int right = 2 * index + 2;
        int target = index;

        Func<T,T,bool> f = mode == MaxMin.Max ? Greater : Less;
        
        if (left < size && f(data[left], data[target]))
           // ((mode == MaxMin.Max && Greater(data[left], data[target])) ||
           //  (mode == MaxMin.Min && Less(data[left], data[target]))))
        {
            target = left;
        }

        if (right < size && f(data[right], data[target]))
        {
            target = right;
        }

        if (target != index)
        {
            Swap(index, target);
            //(data[index], data[target]) = (data[target], data[index]);
            Heapify(target);
        }
    }

    private void Swap(int index1, int index2)
    {
        (data[index1], data[index2]) = (data[index2], data[index1]);
        
        UpdateChangingAction(index1);
        UpdateChangingAction(index2);
        
    }
    
    public T First()
    {
        if (size == 0)
            throw new InvalidOperationException("Schlange ist leer.");
        
        return data[0];
    }

    public T ExtractFirst()
    {
        if (size == 0)
            throw new InvalidOperationException("Schlange ist leer.");
        
        T firstElement = data[0];
        data[0] = data[size - 1];
        size--;
        
        if (size > 0)
            UpdateChangingAction(0);
        
        Heapify(0);
        return firstElement;
    }

    public void Insert(T element)
    {
        if (size == data.Length)
            throw new InvalidOperationException("Queue is full");

        int i = size;
        data[size++] = element;
        UpdateChangingAction(i);

        while (i > 0)
        {
            int parent = (i - 1) / 2;

            bool swap =
                (mode == MaxMin.Max && Greater(data[i], data[parent])) ||
                (mode == MaxMin.Min && Less(data[i], data[parent]));

            if (!swap)
                break;

            Swap(i, parent);
            i = parent;
        }
    }

    public void IncreaseKey(int position)
    {
        int i = position;

        while (i > 0)
        {
            int parent = (i - 1) / 2;
            
            bool swap = 
                (mode == MaxMin.Max && Greater(data[i], data[parent])) ||
                (mode == MaxMin.Min && Less(data[i], data[parent]));
            
            if (!swap)
                break;
            
            Swap(i, parent);
            i = parent;
        }
    }

    public Action CreateChangingKeyAction(int position)
    {
        return () => IncreaseKey(position);
    }

    private void UpdateChangingAction(int index)
    {
        data[index].ChangingAction = CreateChangingKeyAction(index);
    }
    
    public override string ToString()
    {
        if (size == 0) return "";

        string result = data[0].ChangeableKey.ToString();
        for (int i = 1; i < size; i++)
        {
            result += ", " + data[i].ChangeableKey;
        }
        return result;
    }

    
}

public class MaxPriorityQueue<T> : MaxMinPriorityQueue<T> where T : IElementWithKey
{
    public MaxPriorityQueue(int capacity) 
        : base(capacity, MaxMin.Max)
    {
    }
}

public class MinPriorityQueue<T> : MaxMinPriorityQueue<T> where T : IElementWithKey
{
    public MinPriorityQueue(int capacity)
        : base(capacity, MaxMin.Min)
    {
    }
}
