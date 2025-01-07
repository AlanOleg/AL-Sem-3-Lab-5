using System;

public class MyMatrix
{
    private int[,] _matrix;
    private int _rows;
    private int _columns;
    private Random _random = new Random();

    // Конструктор класса
    public MyMatrix(int rows, int columns, int minValue, int maxValue)
    {
        _rows = rows;
        _columns = columns;
        _matrix = new int[rows, columns];
        Fill(minValue, maxValue);
    }

    // Метод для заполнения матрицы случайными значениями
    public void Fill(int minValue, int maxValue)
    {
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                _matrix[i, j] = _random.Next(minValue, maxValue);
            }
        }
    }

    // Метод для изменения размеров матрицы
    public void ChangeSize(int newRows, int newColumns)
    {
        int[,] newMatrix = new int[newRows, newColumns];

        // Копируем значения существующей матрицы
        for (int i = 0; i < Math.Min(_rows, newRows); i++)
        {
            for (int j = 0; j < Math.Min(_columns, newColumns); j++)
            {
                newMatrix[i, j] = _matrix[i, j];
            }
        }


        // Заполняем новую матрицу случайными числами, если она больше
        if (newRows > _rows || newColumns > _columns)
        {
            for (int i = 0; i < newRows; i++)
            {
                for (int j = 0; j < newColumns; j++)
                {
                    if (i >= _rows || j >= _columns)
                    {
                        newMatrix[i, j] = _random.Next(0, 100); // Заполняем случайными числами
                    }
                }
            }
        }

        _matrix = newMatrix;
        _rows = newRows;
        _columns = newColumns;
    }

    // Метод для вывода части матрицы
    public void ShowPartialy(int startRow, int startCol, int endRow, int endCol)
    {
        for (int i = startRow; i <= endRow && i < _rows; i++)
        {
            for (int j = startCol; j <= endCol && j < _columns; j++)
            {
                Console.Write(_matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }

    // Метод для вывода всей матрицы
    public void Show()
    {
        ShowPartialy(0, 0, _rows - 1, _columns - 1);
    }

    // Индексатор для доступа к элементам матрицы
    public int this[int row, int col]
    {
        get
        {
            if (row < 0 || row >= _rows || col < 0 || col >= _columns)
                throw new IndexOutOfRangeException("Indices are out of range.");
            return _matrix[row, col];
        }
        set
        {
            if (row < 0 || row >= _rows || col < 0 || col >= _columns)
                throw new IndexOutOfRangeException("Indices are out of range.");
            _matrix[row, col] = value;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Запрос данных у пользователя
        Console.Write("Введите количество строк: ");
        int rows = int.Parse(Console.ReadLine());

        Console.Write("Введите количество столбцов: ");
        int columns = int.Parse(Console.ReadLine());

        Console.Write("Введите минимальное значение: ");
        int minValue = int.Parse(Console.ReadLine());

        Console.Write("Введите максимальное значение: ");
        int maxValue = int.Parse(Console.ReadLine());

        // Создание матрицы
        MyMatrix matrix = new MyMatrix(rows, columns, minValue, maxValue);

        // Показать всю матрицу
        Console.WriteLine("Исходная матрица:");
        matrix.Show();

        // Изменение размера матрицы
        matrix.ChangeSize(5, 4);
        Console.WriteLine("\nМатрица после изменения размера:");
        matrix.Show();

        // Вывод части матрицы
        Console.WriteLine("\nЧасть матрицы (строки 1-3, колонки 1-2):");
        matrix.ShowPartialy(1, 1, 3, 2);

        // Пример использования индексатора
        Console.WriteLine($"\nЭлемент [2, 2]: {matrix[2, 2]}");
        matrix[2, 2] = 99; // Изменение значения
        Console.WriteLine($"Элемент [2, 2] после изменения: {matrix[2, 2]}");
    }
}