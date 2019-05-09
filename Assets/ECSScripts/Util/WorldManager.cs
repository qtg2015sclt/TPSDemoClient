using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;

public class WorldManager : MonoBehaviour
{
    public static int SYSTEM_LEVEL_FACTOR = 100;

    public static int SYSTEM_COMMON_LEVEL = 5;

    private static int m_entity_count = 0;

    private static int m_system_count = 0;

    private static Dictionary<int, ISystem> m_system_dict = new Dictionary<int, ISystem>();
    public static Dictionary<int, ISystem> SystemDict
    {
        get { return m_system_dict; }
        set { m_system_dict = value; }
    }

    private static Dictionary<int, Entity> m_entity_dict = new Dictionary<int, Entity>();
    public static Dictionary<int, Entity> EntityDict
    {
        get { return m_entity_dict; }
        set { m_entity_dict = value; }
    }

    private static Queue<Entity> m_entity_add_queue = new Queue<Entity>();
    private static Queue<Entity> m_entity_remove_queue = new Queue<Entity>();

    private static Dictionary<int, HashSet<int>> m_system_entity_match_dict = new Dictionary<int, HashSet<int>>();

    private void Start()
    {
        // sort system with id(level)
        m_system_dict = m_system_dict.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        foreach (var system in m_system_dict.Values)
        {
            m_system_entity_match_dict[system.SystemID] = new HashSet<int>();
        }
    }

    private void Update()
    {
        UpdateEntityDict();

        foreach (var system in m_system_dict.Values)
        {
            ExecuteUpdate();
        }
    }

    private void FixedUpdate()
    {
        UpdateEntityDict();

        foreach (var system in m_system_dict.Values)
        {
            ExecuteUpdate();
        }
    }

    private void LateUpdate()
    {
        UpdateEntityDict();

        foreach (var system in m_system_dict.Values)
        {
            ExecuteUpdate();
        }
    }

    public static void AddEntity(Entity entity)
    {
        m_entity_add_queue.Enqueue(entity);
    }

    public static void RemoveEntity(Entity entity)
    {
        m_entity_remove_queue.Enqueue(entity);
    }

    private static void UpdateEntityDict()
    {

    }

    public static void DoAddEntity(Entity entity)
    {
        if (!m_entity_dict.ContainsKey(entity.EntityID))
        {
            m_entity_dict.Add(entity.EntityID, entity);
            MatchEntity(entity);
        }
        else
        {
            Debug.Log("This Entity has existed.");
        }
    }

    public static void DoRemoveEntity(Entity entity)
    {
        Debug.Log("Entity Removed " + entity.EntityID);

        if (!EntityDict.ContainsKey(entity.EntityID))
        {
            Debug.Log("Remove Entity Failed " + entity.EntityID);
        }
        else
        {
            m_entity_dict.Remove(entity.EntityID);
            RemoveEntityMatch(entity);
        }
    }

    public static void AddSystem(int system_id, ISystem system)
    {
        if (-1 ==system_id)
        {
            Debug.Log("System ID Error.");
        }
        else
        {
            m_system_dict.Add(system_id, system);
        }
    }

    public static int GenerateEntityID()
    {
        Debug.Log("Entity ID Generated.");
        return m_entity_count++;
    }

    public static int GenerateSystemID()
    {
        return m_system_count++;
    }

    // TODO: match when add instead of every frame
    public static bool MatchComponents(Entity entity, params Type[] components)
    {
        foreach (var elem in components)
        {
            if (!entity.GetComponent(elem))
                return false;
        }
        return true;
    }

    public static void ExecuteUpdate()
    {

    }

    public static void MatchEntity(Entity entity)
    {

    }

    private static void RemoveEntityMatch(Entity entity)
    {

    }
}
