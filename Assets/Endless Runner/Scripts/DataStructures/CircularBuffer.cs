using System;

public class CircularBuffer<T>
{
    private readonly T[] buffer;
    private int writeIndex;
    private int count;

    public int Capacity => buffer.Length;
    public int Count => count;

    public CircularBuffer(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentException("Capacity must be greater than zero");

        buffer = new T[capacity];
        writeIndex = 0;
        count = 0;
    }

    /// <summary>
    /// Adds a new element to the buffer.
    /// Oldest element is overwritten when buffer is full.
    /// </summary>
    public void Add(T item)
    {
        buffer[writeIndex] = item;
        writeIndex = (writeIndex + 1) % buffer.Length;

        if (count < buffer.Length)
            count++;
    }

    /// <summary>
    /// Gets element relative to the newest entry.
    /// offset = 0 → newest
    /// offset = 1 → one step older
    /// </summary>
    public T GetFromNewest(int offset)
    {
        if (offset < 0 || offset >= count)
            throw new IndexOutOfRangeException("Invalid offset");

        int index = writeIndex - 1 - offset;
        if (index < 0)
            index += buffer.Length;

        return buffer[index];
    }

    /// <summary>
    /// Gets the oldest stored element.
    /// </summary>
    public T GetOldest()
    {
        if (count == 0)
            throw new InvalidOperationException("Buffer is empty");

        int index = (writeIndex - count + buffer.Length) % buffer.Length;
        return buffer[index];
    }

    public void Clear()
    {
        writeIndex = 0;
        count = 0;
    }
}
