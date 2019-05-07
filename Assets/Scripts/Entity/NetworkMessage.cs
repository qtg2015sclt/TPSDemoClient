using System.Runtime.Serialization;
using System;

[DataContract]
public class NetworkMessage
{
    [DataMember]
    public Int32 ServiceID { get; set; }

    [DataMember]
    public Int32 CommandID { get; set; }

    [DataMember]
    public String Data { get; set; }
}