using System;
using System.Collections;
using System.Collections.Generic;

public class MyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
{
    private KeyValuePair<TKey, TValue>[] _items;
    private int _count;
    private const int DefaultCapacity = 4;

    public MyDictionary()
    {
        _items = new KeyValuePair<TKey, TValue>[DefaultCapacity];
        _count = 0;
    }

    public int Count => _count;

    public void Add(TKey key, TValue value)
    {
        // Проверка на дублирование ключей
        for (int i = 0; i < _count; i++)
        {
            if (_items[i].Key.Equals(key))
            {
                throw new ArgumentException($"Ключ '{key}' уже существует.");
            }
        }

        // Увеличение размера массива при необходимости
        if (_count == _items.Length)
        {
            Resize();
        }

        _items[_count++] = new KeyValuePair<TKey, TValue>(key, value);
    }

    public TValue this[TKey key]
    {
        get
        {
            for (int i = 0; i < _count; i++)
            {
                if (_items[i].Key.Equals(key))
                {
                    return _items[i].Value;
                }
            }
            throw new KeyNotFoundException($"Ключ '{key}' не найден.");
        }
    }

    private void Resize()
    {
        int newCapacity = _items.Length * 2;
        KeyValuePair<TKey, TValue>[] newItems = new KeyValuePair<TKey, TValue>[newCapacity];
        Array.Copy(_items, newItems, _count);
        _items = newItems;
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        for (int i = 0; i < _count; i++)
        {
            yield return _items[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

// Пример использования
class Program
{
    static void Main()
    {
        MyDictionary<string, int> myDictionary = new MyDictionary<string, int>();
        myDictionary.Add("Alice", 30);
        myDictionary.Add("Bob", 25);
        myDictionary.Add("Charlie", 35);

        Console.WriteLine($"Общее количество элементов: {myDictionary.Count}");

        foreach (var pair in myDictionary)
        {
            Console.WriteLine($"Ключ: {pair.Key}, Значение: {pair.Value}");
        }

        // Пример доступа по ключу
        Console.WriteLine($"Возраст Bob: {myDictionary["Bob"]}");
    }
}