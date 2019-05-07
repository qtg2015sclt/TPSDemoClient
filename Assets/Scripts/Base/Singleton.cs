using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T instance;

    public static T GetInstance()
    {
        if (!instance)
        {
            new GameObject(typeof(T).Name).AddComponent<T>();
            DontDestroyOnLoad(instance);
        }
        return instance;
    }

    protected virtual void Awake()
    {
        if (!instance)
        {
            instance = this as T;
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
