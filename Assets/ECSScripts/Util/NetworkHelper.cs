using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System;

public class NetworkHelper
{
    public static void SendData(string msg)
    {
        GlobalInstance._Network.SendMsgQ.Enqueue(msg + "\n");
    }
    //public static void SendData<T>(int service_id, int command_ID, T msg_need_write)
    //{
    //    NetworkMsg<T> network_msg = new NetworkMsg<T>()
    //    {
    //        SID = service_id,
    //        CID = command_ID,
    //        Data = msg_need_write
    //    };
    //    DataContractJsonSerializer json_serializer = new DataContractJsonSerializer(typeof(NetworkMsg<T>));
    //    MemoryStream memory_stream = new MemoryStream();
    //    json_serializer.WriteObject(memory_stream, network_msg);
    //    memory_stream.Position = 0;

    //    StreamReader stream_reader = new StreamReader(memory_stream, Encoding.UTF8);
    //    string network_msg_data = stream_reader.ReadToEnd();
    //    memory_stream.Close();
    //    stream_reader.Close();
    //    Debug.Log("network_msg_data: " + network_msg_data);
    //    GlobalInstance._Network.SendMsgQ.Enqueue(network_msg_data);
    //}

    //public static T GetData<T>(string received_data)
    //{
    //    //Debug.Log("GetJsonContent input string = " + input);
    //    T t = Activator.CreateInstance<T>();
    //    DataContractJsonSerializer json_serializer = new DataContractJsonSerializer(t.GetType());
    //    MemoryStream memory_stream = new MemoryStream(Encoding.Default.GetBytes(received_data));
    //    memory_stream.Position = 0;
    //    T output = (T)json_serializer.ReadObject(memory_stream);
    //    return output;
    //}
}