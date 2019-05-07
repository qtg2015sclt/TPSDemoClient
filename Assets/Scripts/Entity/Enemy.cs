using System.Runtime.Serialization;
using System;

[DataContract]
public class Enemy
{
    [DataMember]
    public Int32 EnemyID { get; set; }

    [DataMember]
    public Int32 EnemyType { get; set; }

    [DataMember]
    public Int32 ActionCode { get; set; }

    [DataMember]
    public float PostionX { get; set; }

    [DataMember]
    public float PostionZ { get; set; }

    [DataMember]
    public float RotationX { get; set; }

    [DataMember]
    public float RotationY { get; set; }
}