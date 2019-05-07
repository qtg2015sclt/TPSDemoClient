using System;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using UnityEngine;
using System.Collections;

public class Network : Singleton<Network>
{
    private String host = "localhost";
    private Int32 port = 57890;

    private Boolean socketReady = false;
    TcpClient tcpSocket;
    NetworkStream networkStream;

    private StreamReader socketReader;
    private StreamWriter socketWriter;

    private Queue sendMsgQ = new Queue();

    public CanvasGroup canvasGroup;

    protected override void Awake()
    {
        base.Awake();
        SetupSocket();
    }

    private void OnApplicationQuit()
    {
        CloseSocket();
    }

    // Update is called once per frame
    void Update()
    {
        if (0 == sendMsgQ.Count)
            return;
        String msg = sendMsgQ.Dequeue() as String;
        if (null != msg)
            WriteSocket(msg);
    }

    private void SetupSocket()
    {
        try
        {
            tcpSocket = new TcpClient(host, port);
            networkStream = tcpSocket.GetStream();
            socketReader = new StreamReader(networkStream);
            socketWriter = new StreamWriter(networkStream);

            socketReady = true;
        }
        catch (Exception e)
        {
            Debug.Log("Socket Error: " + e);
        }
    }

    public void SendData(Int32 serviceID, Int32 commandID, String data)
    {
        NetworkMessage networkMessage = new NetworkMessage()
        {
            ServiceID = serviceID,
            CommandID = commandID,
            Data = data
        };

        DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(NetworkMessage));
        MemoryStream memoryStream = new MemoryStream();
        jsonSerializer.WriteObject(memoryStream, networkMessage);
        memoryStream.Position = 0;

        StreamReader streamReader = new StreamReader(memoryStream, Encoding.UTF8);
        String msg = streamReader.ReadToEnd();
        memoryStream.Close();
        streamReader.Close();

        sendMsgQ.Enqueue(msg);
        //WriteSocket(msg);
    }

    public String ReadOutput()
    {
        String message = this.ReadSocket();
        if (message == "")
        {
            return "";
        }

        //Debug.Log("Receive message: " + message);
        //DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(NetworkMessage));
        //MemoryStream memoryStream = new MemoryStream(Encoding.Default.GetBytes(message));
        //NetworkMessage networkMessage = jsonSerializer.ReadObject(memoryStream) as NetworkMessage;

        //Debug.Log("Start GetJsonContent of NetworkMessage");
        NetworkMessage networkMessage = this.GetJsonContent<NetworkMessage>(message);
        //Debug.Log("End GetJsonContent of NetworkMessage");

        switch(networkMessage.ServiceID)
        {
            case Constants.LoginServiceID:
                switch(networkMessage.CommandID)
                {
                    case Constants.LoginSuccessCommandID:
                        Resource resource = this.GetJsonContent<Resource>(networkMessage.Data);
                        GameManager.instance.LoadInitResourceInfo(resource);
                        canvasGroup.alpha = 0;
                        canvasGroup.interactable = false;
                        break;
                    case Constants.LoginFailedCommandID:
                        message = "Name or Password is wrong, or you have logged in from other position. Login failed.\n";
                        break;
                    default:
                        message = "Invalide CommandID: " + networkMessage.CommandID;
                        Debug.Log("Invalide CommandID: " + networkMessage.CommandID);
                        break;
                }
                break;
            case Constants.GameServiceID:
                //Debug.Log("Receive sync data.");
                switch (networkMessage.CommandID)
                {
                    case Constants.ClientSyncCommandID:
                        //Debug.Log("Start GetJsonContent of SyncData");
                        SyncData syncData = this.GetJsonContent<SyncData>(networkMessage.Data);
                        //String test = "{ \"Players\": [{\"PlayerID\": 1, \"PostionX\": 100, \"Name\": \"test1\", \"PostionZ\": 100, \"ActionCode\": 3001}], \"Enemies\": []}";
                        //SyncData syncData = this.GetJsonContent<SyncData>(test);
                        //Debug.Log("End GetJsonContent of SyncData");
                        GameManager.instance.SyncDataFromServer(syncData);
                        break;
                    case Constants.GameDefeatCommandID:
                        GameManager.instance.GameOver(false);
                        break;
                    case Constants.GameVictoryCommandID:
                        //Debug.Log("game over.");
                        GameManager.instance.GameOver(true);
                        break;
                    default:
                        message = "Invalide CommandID: " + networkMessage.CommandID;
                        Debug.Log("Invalide CommandID: " + networkMessage.CommandID);
                        break;
                }
                break;
            default:
                Debug.Log("Invalid ServiceID: " + networkMessage.CommandID);
                break;
        }

        //jsonSerializer = new DataContractJsonSerializer(typeof(Resource));
        //memoryStream = new MemoryStream(Encoding.Default.GetBytes(networkMessage.Data));
        //Resource resource = jsonSerializer.ReadObject(memoryStream) as Resource;
        //Debug.Log("Resource: " + resource.Money + ", " + resource.Bullet + ", " + resource.Grenade + ", " + resource.TankBullet + ".");

        return message;
    }

    private T GetJsonContent<T>(String input)
    {
        //Debug.Log("GetJsonContent input string = " + input);
        T t = Activator.CreateInstance<T>();
        DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(t.GetType());
        MemoryStream memoryStream = new MemoryStream(Encoding.Default.GetBytes(input));
        memoryStream.Position = 0;
        T output = (T)jsonSerializer.ReadObject(memoryStream);
        return output;
    }

    private void WriteSocket(String input)
    {
        if (!socketReady)
            return;

        //input += "\r\n";
        socketWriter.Write(input);
        socketWriter.Flush();
    }

    private String ReadSocket()
    {
        if (!socketReady)
        {
            Debug.Log("Socket is not ready.");
            return "";
        }

        if (networkStream.DataAvailable)
        {
            return socketReader.ReadLine();
        }

        //Debug.Log("No Data.");
        return "";
    }

    private void CloseSocket()
    {
        if (!socketReady)
            return;

        socketReader.Close();
        socketWriter.Close();
        networkStream.Close();
        tcpSocket.Close();
        socketReady = false;
    }
}
