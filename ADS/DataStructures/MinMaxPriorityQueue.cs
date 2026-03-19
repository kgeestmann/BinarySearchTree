namespace ADS;

public enum MaxMin
{
    Max,
    Min
}

public class MaxMinPriorityQueue<T> where T : IElementWithKey
{
    // Heap-Array (Levelorder), size = Anzahl belegter Elemente
    private T[] data;
    private int size;

    // mode entscheidet: Max-Heap oder Min-Heap
    private MaxMin mode;
    
    public int Size
    {
        get { return size; }
    }

    public MaxMinPriorityQueue(int capacity, MaxMin mode)
    {
        data = new T[capacity];
        size = 0;
        this.mode = mode;
    }

    // Vergleich immer über ChangeableKey (nicht Key!)
    private bool Less(T object1, T object2)
    {
        return object1.ChangeableKey < object2.ChangeableKey;
    }

    private bool Greater(T object1, T object2)
    {
        return object1.ChangeableKey > object2.ChangeableKey;
    }

    // Heapify (bubble down): repariert Heap-Eigenschaft ab index
    private void Heapify(int index)
    {
        int left = 2 * index + 1;
        int right = 2 * index + 2;
        int target = index;

        // f = "besser als" je nach Modus (Max: Greater, Min: Less)
        Func<T, T, bool> f = mode == MaxMin.Max ? Greater : Less;

        // bestes Kind auswählen (für Max: größtes, für Min: kleinstes)
        if (left < size && f(data[left], data[target]))
        {
            target = left;
        }

        if (right < size && f(data[right], data[target]))
        {
            target = right;
        }

        // wenn Kind besser ist -> tauschen und rekursiv weiter
        if (target != index)
        {
            Swap(index, target);
            Heapify(target);
        }
    }
    
    // nach einem Swap hat ein Element eine neue Position -> Action muss aktualisiert werden
    private void Swap(int index1, int index2)
    {
        (data[index1], data[index2]) = (data[index2], data[index1]);

        UpdateChangingAction(index1);
        UpdateChangingAction(index2);
    }

    // First = Root des Heaps (Max oder Min)
    public T First()
    {
        if (size == 0)
            throw new InvalidOperationException("Schlange ist leer.");

        return data[0];
    }

    // ExtractFirst: Root entfernen (Max oder Min) -> letztes Element nach vorne -> Heapify
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

    // Insert: ans Ende hängen -> bubble up (wie Insert in Heap auf Papier)
    public void Insert(T element)
    {
        if (size == data.Length)
            throw new InvalidOperationException("Queue is full");

        int i = size;
        data[size++] = element;

        // jedes Element bekommt eine Action, die seine aktuelle Position kennt
        UpdateChangingAction(i);

        // bubble up: solange parent "schlechter" ist -> tauschen
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

    // IncreaseKey (bubble up):
    private void IncreaseKey(int position)
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
    
    // Wenn sich ChangeableKey eines Elements verbessert, ruft das Element seine ChangingAction auf,
    // dadurch wird IncreaseKey(position) ausgeführt und der Heap repariert sich automatisch.
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
        if (size == 0)
            return "";

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