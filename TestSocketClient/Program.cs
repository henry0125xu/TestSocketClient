using System.Net.Sockets;
using System.Text;
using System.Transactions;

string serverIpAddress = "127.0.0.1";
int serverPort = 3000;
// Create a TCP client and connect to the server
TcpClient client = new TcpClient();

try
{
    client.Connect(serverIpAddress, serverPort);

    Console.WriteLine("Connected to server.");

    // Get the network stream associated with the client socket
    NetworkStream stream = client.GetStream();


    while (true)
    {
        // Send data to the server
        Console.WriteLine("Input the message (input \"exit\" to terminate the client): ");
        string? message = Console.ReadLine();
        if (message == "exit") break;

        byte[] sendData = Encoding.ASCII.GetBytes(message);
        stream.Write(sendData, 0, sendData.Length);
        Console.WriteLine("Sent to server: " + message);

        // Receive data from the server
        byte[] receiveData = new byte[4096];
        int bytesRead = stream.Read(receiveData, 0, receiveData.Length);
        string response = Encoding.ASCII.GetString(receiveData, 0, bytesRead);
        Console.WriteLine("Received from server: " + response);
    }

    // Close the client connection
    client.Close();
    Console.WriteLine("Disconnected from server. Press any key to exit.");
    Console.ReadKey();
}
catch (Exception e)
{
    Console.WriteLine("Exception occur: ");
    Console.WriteLine();
    Console.WriteLine(e.ToString());
}


