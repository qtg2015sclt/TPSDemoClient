using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
    private int m_entity_id = -1;
    public int EntityID
    {
        get { return m_entity_id; }
        set { m_entity_id = value; }
    }

    private void Awake()
    {
        if (-1 == EntityID)
        {
            //EntityID;
        }
    }

    private void OnDestroy()
    {
        
    }
}
