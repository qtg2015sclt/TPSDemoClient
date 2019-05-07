using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System;

public class Login : MonoBehaviour
{
    [SerializeField]
    private InputField accountInputField;
    [SerializeField]
    private InputField passwordInputField;

    //private CanvasGroup canvasGroup;

    void Awake()
    {
        //canvasGroup = GetComponentInParent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickLoginButton()
    {
        User user = new User()
        {
            Name = accountInputField.text,
            Password = passwordInputField.text
        };

        DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(User));
        MemoryStream memoryStream = new MemoryStream();
        jsonSerializer.WriteObject(memoryStream, user);
        memoryStream.Position = 0;

        StreamReader streamReader = new StreamReader(memoryStream, Encoding.UTF8);
        String data = streamReader.ReadToEnd();
        memoryStream.Close();
        streamReader.Close();
        Debug.Log("Login Data: " + data);

        Network.GetInstance().SendData(Constants.LoginServiceID, Constants.HandleLoginCommandID, data);

        //canvasGroup.alpha = 0;
        //canvasGroup.interactable = false;
    }
}
