using System;
using System.Collections.Generic;
using System.Linq;

// --- Завдання 1 ---
public static class ArrayExtensions
{
    public static List<int> GetNumbers(int[] array, Predicate<int> condition)
    {
        return array.Where(x => condition(x)).ToList();
    }

    public static bool IsPrime(int number)
    {
        if (number < 2) return false;
        for (int i = 2; i <= Math.Sqrt(number); i++)
            if (number % i == 0) return false;
        return true;
    }

    public static bool IsFibonacci(int n)
    {
        int a = 0, b = 1;
        while (b < n) { int temp = a; a = b; b = temp + b; }
        return b == n || n == 0;
    }
}

// --- Завдання 2 ---
public class Utility
{
    public Action ShowTime = () => Console.WriteLine(DateTime.Now.ToLongTimeString());
    public Action ShowDate = () => Console.WriteLine(DateTime.Now.ToShortDateString());
    public Action ShowDay = () => Console.WriteLine(DateTime.Now.DayOfWeek);

    public Func<double, double, double> TriangleArea = (b, h) => 0.5 * b * h;
    public Func<double, double, double> RectangleArea = (a, b) => a * b;
}

// --- Завдання 3 ---
public class CreditCard
{
    public string CardNumber { get; set; }
    public string OwnerName { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string Pin { get; private set; }
    public decimal CreditLimit { get; set; }
    public decimal Balance { get; private set; }

    public event Action<decimal> OnDeposit;
    public event Action<decimal> OnWithdraw;
    public event Action OnCreditUsed;
    public event Action OnLimitReached;
    public event Action OnPinChanged;

    public void Deposit(decimal amount)
    {
        Balance += amount;
        OnDeposit?.Invoke(amount);
    }

    public void Withdraw(decimal amount)
    {
        if (Balance >= 0 && Balance - amount < 0) OnCreditUsed?.Invoke();
        Balance -= amount;
        if (Balance <= -CreditLimit) OnLimitReached?.Invoke();
        OnWithdraw?.Invoke(amount);
    }

    public void ChangePin(string newPin)
    {
        Pin = newPin;
        OnPinChanged?.Invoke();
    }
}

// --- Завдання 4 ---
public class StringProcessor
{
    public delegate int StringHandler(string input);

    public int CountVowels(string s) => s.Count(c => "aeiouаеєиіїоуюя".Contains(char.ToLower(c)));
    public int CountConsonants(string s) => s.Count(c => char.IsLetter(c) && !"aeiouаеєиіїоуюя".Contains(char.ToLower(c)));
    public int GetLength(string s) => s.Length;
}

// --- Приклад роботи ---
public class Program
{
    public static void Main()
    {
        // Завдання 1
        int[] nums = { 1, 2, 3, 5, 8, 10, 13 };
        var evens = ArrayExtensions.GetNumbers(nums, x => x % 2 == 0);
        var primes = ArrayExtensions.GetNumbers(nums, ArrayExtensions.IsPrime);

        // Завдання 4
        string text = "Hello World";
        StringProcessor sp = new StringProcessor();
        StringProcessor.StringHandler handler = sp.CountVowels;
        Console.WriteLine($"Голосні: {handler(text)}");
        
        handler = sp.CountConsonants;
        Console.WriteLine($"Приголосні: {handler(text)}");
        
        handler = sp.GetLength;
        Console.WriteLine($"Довжина: {handler(text)}");
    }
}
