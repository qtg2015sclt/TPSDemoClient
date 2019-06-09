using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    public InputField AccountIF;
    public InputField PasswordIF;

    public void QuickLoginBtnOnClick()
    {
        Debug.Log("QuickLoginBtnOnClick");
        string username = PlayerPrefs.GetString(Constants.username);
        string password = PlayerPrefs.GetString(Constants.password);
        LocalAuthMsg msg = new LocalAuthMsg()
        {
            SID = NSendEID.LoginSID,
            CID = NSendEID.LoginMsgSendCID,
            UserName = username,
            Password = password
        };
        NetworkHelper.SendData(msg.ToJson());
    }

    public void LoginBtnOnClick()
    {
        Debug.Log("LoginBtnOnClick");
        LocalAuthMsg msg = new LocalAuthMsg()
        {
            SID = NSendEID.LoginSID,
            CID = NSendEID.LoginMsgSendCID,
            UserName = AccountIF.text,
            Password = PasswordIF.text
        };
        NetworkHelper.SendData(msg.ToJson());
    }

    public void RegisterBtnOnClick()
    {
        Debug.Log("RegisterBtnOnClick");
        LocalAuthMsg msg = new LocalAuthMsg()
        {
            SID = NCommonEID.LoginSID,
            CID = NCommonEID.RegisterMsgCID,
            UserName = AccountIF.text,
            Password = PasswordIF.text
        };
        NetworkHelper.SendData(msg.ToJson());
    }
}
