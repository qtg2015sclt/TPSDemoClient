using UnityEngine;
using System.Collections;

public static class GlobalInstance
{
    private static Entity m_main_player = null;
    public static Entity _MainPlayer
    {
        get { return m_main_player; }
        set { m_main_player = value; }
    }
}
