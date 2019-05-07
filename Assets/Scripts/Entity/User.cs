using System.Runtime.Serialization;
using System;

[DataContract]
public class User
{
    [DataMember]
    public Int32 UserID { get; set; }

    [DataMember]
    public String Name { get; set; }

    [DataMember]
    public String Password { get; set; }
}