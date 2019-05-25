using UnityEngine;
using System.Collections;

public class MsgDispatchHelper : MonoBehaviour
{
    public static void Dispatch(string msg)
    {
        GlobalInstance._Dispatcher.MsgNeedDispatch.Enqueue(msg);
    }

    /// <summary>
    /// Register to MsgDispatch.
    /// </summary>
    /// <param name="sid">service id</param>
    /// <param name="service">the certain service</param>
    public static void Register(int sid, IService service)
    {
        if (GlobalInstance._Dispatcher.Services.ContainsKey(sid))
        {
            Debug.Log("sid = " + sid + "of service has already been registered.");
            return;
        }

        GlobalInstance._Dispatcher.Services.Add(sid, service);
    }
}
