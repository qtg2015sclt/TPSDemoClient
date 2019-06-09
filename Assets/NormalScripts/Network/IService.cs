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
        if (local_auth_msg.UserID > 0)
        {
            Debug.Log("Login Success!");
            string username = local_auth_msg.UserName;
            string password = local_auth_msg.Password;

            // Store
            Debug.Log("store: " + username + ", " + password);
            PlayerPrefs.SetString(Constants.username, username);
            PlayerPrefs.SetString(Constants.password, password);
        }
        else if (0 == local_auth_msg.UserID)
        {
            Debug.Log("Login Failed! No such user.");
        }
        else if (-1 == local_auth_msg.UserID)
        {
            Debug.Log("Login Failed! Unknown problem.");
        }
    }

    private void HandleRegisterReturnMsg(string msg)
    {
        LocalAuthMsg local_auth_msg = JsonUtility.FromJson<LocalAuthMsg>(msg);
        if (local_auth_msg.UserID > 0)
        {
            Debug.Log("Register Success!");
        }
        else if (-1 == local_auth_msg.UserID)
        {
            Debug.Log("Register Failed!");
        }
        else if (0 == local_auth_msg.UserID)
        {
            Debug.Log("Cannot use the user name, this user name exist.");
        }
    }

    protected override void Register()
    {
        Commands.Add(NCommonEID.LoginMsgCID, HandleLoginReturnMsg);
        Commands.Add(NCommonEID.RegisterMsgCID, HandleRegisterReturnMsg);
    }
}