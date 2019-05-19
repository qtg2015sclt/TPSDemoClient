using UnityEngine;
using System.Collections;
using System;
using System.Net.Sockets;
using System.IO;

public class NetworkSystem : ISystem
{
    private Type[] m_required_components = { typeof(NetworkComponent) };

    private void Awake()
    {
        m_components = m_required_components;

        m_system_level = WorldManager.SYSTEM_COMMON_LEVEL - 4;
        m_system_id = m_system_level * WorldManager.SYSTEM_LEVEL_FACTOR + WorldManager.GenerateSystemID();

        WorldManager.AddSystem(m_system_id, this);
    }

    private void Start()
    {
        SetupSocket();
    }

    private void OnApplicationQuit()
    {
        CloseSocket();
    }

    public override void UpdateEntity(Entity entity)
    {
        NetworkComponent _network = GlobalInstance._Network;
        while (_network.SendMsgQ.Count > 0)
        {
            string network_msg_data = _network.SendMsgQ.Dequeue();
            if (!_network.SocketReady)
                return;

            //network_msg_data += "\r\n";
            _network.SocketWriter.Write(network_msg_data);
            _network.SocketWriter.Flush();
        }
    }

    private void SetupSocket()
    {
        NetworkComponent _network = GlobalInstance._Network;
        try
        {
            _network.TCPSocket = new TcpClient(_network.Host, _network.Port);
            _network.NetworkStream = _network.TCPSocket.GetStream();
            _network.SocketReader = new StreamReader(_network.NetworkStream);
            _network.SocketWriter = new StreamWriter(_network.NetworkStream);

            _network.SocketReady = true;
        }
        catch (Exception e)
        {
            Debug.Log("Socket Error: " + e);
        }
    }

    private void CloseSocket()
    {
        NetworkComponent _network = GlobalInstance._Network;
        if (!_network.SocketReady)
            return;

        _network.SocketReader.Close();
        _network.SocketWriter.Close();
        _network.NetworkStream.Close();
        _network.TCPSocket.Close();
        _network.SocketReady = false;
    }
}
