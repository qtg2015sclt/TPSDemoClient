using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MsgDispatchComponent : IComponent
{
    [HideInInspector]
    public Queue<string> MsgNeedDispatch = new Queue<string>();

    public Dictionary<int, IService> Services = new Dictionary<int, IService>();

    private void Awake()
    {
        GlobalInstance._Dispatcher = this;
    }
}
