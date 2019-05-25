using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

//namespace Assets.NormalScripts.Network
//{
//    class IService
//    {
//    }
//}

public class IService: object
{
    protected Dictionary<int, Action<string>> Commands = new Dictionary<int, Action<string>>();

    public void Handle(string msg)
    {
        NetworkMsg network_msg = JsonUtility.FromJson<NetworkMsg>(msg);
        Action<string> command;
        Commands.TryGetValue(network_msg.CID, out command);
        if (null != command)
        {
            command(msg);
        }
        else
        {
            Debug.Log("There is no command with cid = " + network_msg.CID);
        }
    }

    protected virtual void Register() { }
}

public class LoginService : IService
{
    public LoginService()
    {
        Register();
    }

    private void HandleLoginReturnMsg(string msg)
    {
        LocalAuthMsg local_auth_msg = JsonUtility.FromJson<LocalAuthMsg>(msg);
        if (1 == local_auth_msg.UserID)
        {
            Debug.Log("Login Success!");
        }
        else if (-1 == local_auth_msg.UserID)
        {
            Debug.Log("Login Failed!");
        }
    }

    protected override void Register()
    {
        Commands.Add(NCommonEID.LoginMsgSendCID, HandleLoginReturnMsg);
    }
}