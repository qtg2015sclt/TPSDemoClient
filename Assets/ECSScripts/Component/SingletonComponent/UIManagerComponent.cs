using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManagerComponent : IComponent
{
    private void Awake()
    {
        GlobalInstance._UIManager = this;
    }
}
