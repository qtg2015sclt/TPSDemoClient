using System.Runtime.Serialization;
using System;
using UnityEngine;
using System.Collections.Generic;

[DataContract]
public class SyncData
{
    [DataMember]
    public List<Player> Players { get; set; }

    [DataMember]
    public List<Enemy> Enemies { get; set; }

    //[DataMember]
    //public List<Tank> Tanks { get; set; }

    [DataMember]
    public Tank Tank { get; set; }
}