using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SendInput : MonoBehaviour
{
    [SerializeField]
    private InputField inputField;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void ClickSendButton()
    {
        //Network.GetInstance().SendInput(inputField.text);
    }
}
