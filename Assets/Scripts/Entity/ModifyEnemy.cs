using System.Runtime.Serialization;
using System;

[DataContract]
public class ModifyEnemy
{
    [DataMember]
    public Int32 EnemyID { get; set; }

    [DataMember]
    public Int32 DamageValue { get; set; }
}