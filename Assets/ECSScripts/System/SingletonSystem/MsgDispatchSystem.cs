using UnityEngine;
using System.Collections;
using System;

public class MsgDispatchSystem : ISystem
{
    private Type[] m_required_components = { typeof(MsgDispatchComponent) };

    private void Awake()
    {
        m_components = m_required_components;

        m_system_level = WorldManager.SYSTEM_COMMON_LEVEL - 4;
        m_system_id = m_system_level * WorldManager.SYSTEM_LEVEL_FACTOR + WorldManager.GenerateSystemID();

        WorldManager.AddSystem(m_system_id, this);
    }

    private void Start()
    {
        GlobalInstance._Dispatcher.Services.Add(NCommonEID.LoginSID, new LoginService());
    }

    public override void UpdateEntity(Entity entity)
    {
        while (GlobalInstance._Dispatcher.MsgNeedDispatch.Count > 0)
        {
            string msg = GlobalInstance._Dispatcher.MsgNeedDispatch.Dequeue();
            NetworkMsg network_msg = JsonUtility.FromJson<NetworkMsg>(msg);
            Debug.Log("network_msg SID = " + network_msg.SID + ", CID = " + network_msg.CID);

            IService service;
            GlobalInstance._Dispatcher.Services.TryGetValue(network_msg.SID, out service);
            if (null != service)
            {
                service.Handle(msg);
            }
            else
            {
                Debug.Log("There is no service with sid = " + network_msg.SID);
            }
        }
    }
}
