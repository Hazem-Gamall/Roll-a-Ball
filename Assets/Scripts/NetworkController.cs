using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;

public class NetworkController : MonoBehaviour
{
    Thread thread;
    [SerializeField] int connectionPort = 25001;
    TcpListener server;
    TcpClient client;
    PlayerController player;
    bool running;


    void Start()
    {
        // Receive on a separate thread so Unity doesn't freeze waiting for data
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        ThreadStart ts = new ThreadStart(GetData);
        thread = new Thread(ts);
        thread.Start();
        print("Thread started");
    }

    void GetData()
    {
        // Create the server
        server = new TcpListener(IPAddress.Any, connectionPort);
        server.Start();

        // Create a client to get the data stream
        while (true)
        {
            client = server.AcceptTcpClient();
            print($"Client connected from {client.Client.RemoteEndPoint}");

            running = true;
            while (running)
            {
                Connection();
            }
        }
    }

    void Connection()
    {
        // Read data from the network stream
        NetworkStream nwStream = client.GetStream();
        byte[] buffer = new byte[client.ReceiveBufferSize];
        int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

        // Decode the bytes into a string
        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        // Make sure we're not getting an empty string
        //dataReceived.Trim();
        print($"data received {dataReceived}");
        if (dataReceived != null && dataReceived != "")
        {
            // Convert the received string of data to the format we are using
            ParseData(dataReceived);
        }
        else
        {
            running = false;
        }
    }

    // Use-case specific function, need to re-write this to interpret whatever data is being sent
    public void ParseData(string dataString)
    {
        if(dataString == "w")
        {
            player.Forward();
        }
        if(dataString == "s")
        {
            player.Back();
        }
        if(dataString == "a")
        {
            player.Left();
        }
        if(dataString == "d")
        {
            player.Right();
        }
        if(dataString == " ")
        {
            player.Jump();
        }
    }

    // Position is the data being received in this example
    Vector3 position = Vector3.zero;

    void Update()
    {
        // Set this object's position in the scene according to the position received
        transform.position = position;
    }

    private void OnDestroy()
    {
        server.Stop();
        thread.Abort();
    }
}