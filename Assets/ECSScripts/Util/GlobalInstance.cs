using UnityEngine;
using System.Collections;

public static class GlobalInstance
{
    public static UIManagerComponent _UIManager;

    public static NetworkComponent _Network = new NetworkComponent();

    private static Entity m_main_player = null;
    public static Entity _MainPlayer
    {
        get { return m_main_player; }
        set { m_main_player = value; }
    }
}
