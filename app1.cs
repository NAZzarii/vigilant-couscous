using System;

public interface IOutput
{
    void Show();
    void Show(string info);
}

public class ArrayHandler : IOutput
{
    private int[] _array;

    public ArrayHandler(int[] array)
    {
        _array = array;
    }

    public void Show()
    {
        Console.WriteLine(string.Join(", ", _array));
    }

    public void Show(string info)
    {
        Console.WriteLine(info);
        Show();
    }
}

public class Program
{
    public static void Main()
    {
        int[] numbers = { 10, 25, 33, 48, 50 };
        ArrayHandler handler = new ArrayHandler(numbers);

        handler.Show();
        Console.WriteLine();
        handler.Show("Ось поточний масив чисел:");
    }
}
