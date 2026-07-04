using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Оберіть завдання: 1 - Вітання, 2 - Дата/Час");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            _ = Task.Run(() => ServerTask1());
            await Task.Delay(500);
            await ClientTask1();
        }
        else
        {
            _ = Task.Run(() => ServerTask2());
            await Task.Delay(500);
            await ClientTask2();
        }
    }

    #region Завдання 1: Вітання
    static void ServerTask1()
    {
        TcpListener listener = new TcpListener(IPAddress.Loopback, 8881);
        listener.Start();
        using TcpClient client = listener.AcceptTcpClient();
        using NetworkStream stream = client.GetStream();

        byte[] buffer = new byte[1024];
        int read = stream.Read(buffer, 0, buffer.Length);
        string msg = Encoding.UTF8.GetString(buffer, 0, read);
        Console.WriteLine($"\nСервер: О {DateTime.Now:HH:mm} від {client.Client.RemoteEndPoint} отримано рядок: {msg}");

        byte[] response = Encoding.UTF8.GetBytes("Привіт, клієнт!");
        stream.Write(response, 0, response.Length);
    }

    static async Task ClientTask1()
    {
        using TcpClient client = new TcpClient("127.0.0.1", 8881);
        using NetworkStream stream = client.GetStream();

        byte[] data = Encoding.UTF8.GetBytes("Привіт, сервер!");
        await stream.WriteAsync(data, 0, data.Length);

        byte[] buffer = new byte[1024];
        int read = await stream.ReadAsync(buffer, 0, buffer.Length);
        Console.WriteLine($"Клієнт: О {DateTime.Now:HH:mm} від {client.Client.RemoteEndPoint} отримано рядок: {Encoding.UTF8.GetString(buffer, 0, read)}");
    }
    #endregion

    #region Завдання 2: Дата/Час
    static void ServerTask2()
    {
        TcpListener listener = new TcpListener(IPAddress.Loopback, 8882);
        listener.Start();
        using TcpClient client = listener.AcceptTcpClient();
        using NetworkStream stream = client.GetStream();

        byte[] buffer = new byte[1024];
        int read = stream.Read(buffer, 0, buffer.Length);
        string request = Encoding.UTF8.GetString(buffer, 0, read).ToLower();
        
        string response = (request == "date") ? DateTime.Now.ToShortDateString() : DateTime.Now.ToLongTimeString();
        byte[] data = Encoding.UTF8.GetBytes(response);
        
        stream.Write(data, 0, data.Length);
        // З'єднання розривається автоматично через using при виході з методу
    }

    static async Task ClientTask2()
    {
        Console.Write("Введіть запит (date/time): ");
        string input = Console.ReadLine();

        using TcpClient client = new TcpClient("127.0.0.1", 8882);
        using NetworkStream stream = client.GetStream();

        byte[] data = Encoding.UTF8.GetBytes(input);
        await stream.WriteAsync(data, 0, data.Length);

        byte[] buffer = new byte[1024];
        int read = await stream.ReadAsync(buffer, 0, buffer.Length);
        Console.WriteLine($"Відповідь сервера: {Encoding.UTF8.GetString(buffer, 0, read)}");
    }
    #endregion
}
