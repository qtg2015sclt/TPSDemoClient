using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

//namespace Assets.NormalScripts
//{
//    class LocalAuthMsg
//    {
//    }
//}

[Serializable]
public class NetworkMsg : object
{
    public int SID;
    public int CID;

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}

public class LocalAuthMsg: NetworkMsg
{
    public long UserID;
    public string UserName;
    public string Password;
}

//[DataContract]
//public class NetworkMsg<T>
//{
//    [DataMember]
//    public int SID { get; set; }

//    [DataMember]
//    public int CID { get; set; }

//    [DataMember]
//    public T Data { get; set; }
//}

//[DataContract]
//class LocalAuthMsg
//{
//    [DataMember]
//    public int UserID { get; set; }

//    [DataMember]
//    public string UserName { get; set; }

//    [DataMember]
//    public string Password { get; set; }
//}