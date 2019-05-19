using UnityEngine;
using System.Collections;
using System;

public class UIManagerSystem : ISystem
{
    private Type[] m_required_components = { typeof(UIManagerComponent) };

    private void Awake()
    {
        m_components = m_required_components;

        m_system_level = WorldManager.SYSTEM_COMMON_LEVEL + 5;
        m_system_id = m_system_level * WorldManager.SYSTEM_LEVEL_FACTOR + WorldManager.GenerateSystemID();

        WorldManager.AddSystem(m_system_id, this);
    }

    public override void UpdateEntity(Entity entity)
    {
        
    }
}
