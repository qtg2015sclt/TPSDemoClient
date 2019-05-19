using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;

public class NetworkComponent : IComponent
{
    public string Host = "localhost";
    public int Port = 57890;

    public bool SocketReady = false;
    public TcpClient TCPSocket;
    public NetworkStream NetworkStream;

    public StreamReader SocketReader;
    public StreamWriter SocketWriter;

    public Queue<string> SendMsgQ = new Queue<string>();
}
