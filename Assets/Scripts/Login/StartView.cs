using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class StartView : MonoBehaviour
{
    [SerializeField]
    private Text receivedText;
    //[SerializeField]
    //private Network network;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //String output = network.ReadOutput();
        String output = Network.GetInstance().ReadOutput();
        if (output != "")
        {
            //Debug.Log(output);
            receivedText.text = output;
        }
    }
}
