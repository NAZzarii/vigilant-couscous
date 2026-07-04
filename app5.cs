using System;
using System.Collections;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // 1. Тест Swap
        int x = 5, y = 10;
        Swap(ref x, ref y);
        Console.WriteLine($"Swap: x={x}, y={y}");

        // 2. Тест Stack
        var stack = new MyStack<string>();
        stack.Push("First");
        stack.Push("Second");
        Console.WriteLine($"Stack: Peek={stack.Peek()}, Count={stack.Count}");

        // 3. Тест Океанаріум
        var oceanarium = new Oceanarium();
        oceanarium.Add(new Shark("Great White"));
        oceanarium.Add(new Dolphin("Flipper"));
        foreach (var creature in oceanarium)
            Console.WriteLine($"Oceanarium inhabitant: {creature.Name}");

        // 4. Тест Менеджер співробітників
        var manager = new EmployeeManager();
        manager.Add("ivan", "pass123");
        Console.WriteLine($"Manager: Password for ivan is {manager.GetPassword("ivan")}");
    }

    // --- Завдання 1 ---
    static void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }
}

// --- Завдання 2 ---
public class MyStack<T>
{
    private List<T> _items = new List<T>();
    public int Count => _items.Count;
    public void Push(T item) => _items.Add(item);
    public T Pop() { T item = _items[_items.Count - 1]; _items.RemoveAt(_items.Count - 1); return item; }
    public T Peek() => _items[_items.Count - 1];
}

// --- Завдання 3 ---
public abstract class SeaCreature { public string Name { get; set; } }
public class Shark : SeaCreature { public Shark(string n) => Name = n; }
public class Dolphin : SeaCreature { public Dolphin(string n) => Name = n; }

public class Oceanarium : IEnumerable<SeaCreature>
{
    private List<SeaCreature> _list = new List<SeaCreature>();
    public void Add(SeaCreature c) => _list.Add(c);
    public IEnumerator<SeaCreature> GetEnumerator() => _list.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

// --- Завдання 4 ---
public class EmployeeManager
{
    private Dictionary<string, string> _db = new Dictionary<string, string>();
    public void Add(string login, string pass) => _db[login] = pass;
    public void Remove(string login) => _db.Remove(login);
    public void Update(string login, string pass) => _db[login] = pass;
    public string GetPassword(string login) => _db.ContainsKey(login) ? _db[login] : "Not found";
}
