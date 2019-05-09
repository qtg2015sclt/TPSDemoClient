using UnityEngine;
using System.Collections;
using System;

public class ISystem : MonoBehaviour
{
    protected Type[] m_components;
    protected int m_system_id = -1;
    protected int m_system_level = -1;
    public virtual void UpdateEntity(Entity entity) { }
    public virtual void FixedUpdateEntity(Entity entity) { }
    public virtual void LateUpdateEntity(Entity entity) { }
    public virtual Type[] GetAttachedComponents()
    {
        return m_components;
    }

    public int SystemID
    {
        get { return m_system_id; }
    }

}
