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
        return object1.Key < object2.Key;
    }

    public bool Greater(T object1, T object2)
    {
        return object1.Key > object2.Key;
    }

    private void Heapify(int index)
    {
        int left = 2 * index + 1;
        int right = 2 * index + 2;
        int target = index;

        if (left < size &&
            ((mode == MaxMin.Max && Greater(data[left], data[target])) ||
             (mode == MaxMin.Min && Less(data[left], data[target]))))
        {
            target = left;
        }

        if (right < size &&
            ((mode == MaxMin.Max && Greater(data[right], data[target])) ||
             (mode == MaxMin.Min && Less(data[right], data[target]))))
        {
            target = right;
        }

        if (target != index)
        {
            (data[index], data[target]) = (data[target], data[index]);
            Heapify(target);
        }
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
        
        Heapify(0);
        return firstElement;
    }

    public void Insert(T element)
    {
        if (size == data.Length)
            throw new InvalidOperationException("Queue is full");

        int i = size;
        data[size++] = element;

        while (i > 0)
        {
            int parent = (i - 1) / 2;

            bool swap =
                (mode == MaxMin.Max && Greater(data[i], data[parent])) ||
                (mode == MaxMin.Min && Less(data[i], data[parent]));

            if (!swap)
                break;

            (data[i], data[parent]) = (data[parent], data[i]);
            i = parent;
        }
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
