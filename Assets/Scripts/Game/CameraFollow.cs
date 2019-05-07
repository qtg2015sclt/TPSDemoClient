using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    private Transform target;

    public void BindToPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.SetParent(target);
    }
}
