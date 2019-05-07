using System.Runtime.Serialization;
using System;

[DataContract]
public class Resource
{
    [DataMember]
    public Int32 Money { get; set; }

    [DataMember]
    public Int32 Bullet { get; set; }

    [DataMember]
    public Int32 Grenade { get; set; }

    [DataMember]
    public Int32 TankBullet { get; set; }

    [DataMember]
    public Int32 UserID { get; set; }

    [DataMember]
    public String Name { get; set; }
}