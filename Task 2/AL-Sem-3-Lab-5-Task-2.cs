using System;
using System.Collections;

public class MyList<T> : IEnumerable<T>
{
    private T[] _items;  // Массив для хранения элементов
    private int _count;   // Количество добавленных элементов
    private const int DefaultCapacity = 4; // Начальная ёмкость массива

    // Конструктор по умолчанию
    public MyList()
    {
        _items = new T[DefaultCapacity];
        _count = 0;
    }

    // Индексатор для доступа к элементам списка
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
            {
                throw new IndexOutOfRangeException("Индекс находится вне диапазона.");
            }
            return _items[index];
        }
        set
        {
            if (index < 0 || index >= _count)
            {
                throw new IndexOutOfRangeException("Индекс находится вне диапазона.");
            }
            _items[index] = value;
        }
    }

    // Свойство для получения общего количества элементов
    public int Count => _count;

    // Метод для добавления элемента в список
    public void Add(T item)
    {
        if (_count == _items.Length) // Если массив заполнен, увеличиваем его размер
        {
            Resize();
        }
        _items[_count++] = item; // Добавляем новый элемент и увеличиваем счётчик
    }

    // Метод для изменения размера массива
    private void Resize()
    {
        int newCapacity = _items.Length * 2; // Увеличиваем размер в два раза
        T[] newItems = new T[newCapacity]; // Создаём новый массив
        Array.Copy(_items, newItems, _count); // Копируем элементы из старого массива
        _items = newItems; // Устанавливаем новый массив
    }

    // Реализация интерфейса IEnumerable<T>
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _count; i++)
        {
            yield return _items[i];
        }
    }

    // Реализация интерфейса IEnumerable
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
        MyList<int> myList = new MyList<int>
        {
            1,
            2,
            3
        };

        myList.Add(4);
        myList.Add(5);

        Console.WriteLine("Общее количество элементов: " + myList.Count);

        foreach (var item in myList)
        {
            Console.WriteLine(item);
        }
    }
}