using UnityEngine;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System;

public class QuickLogin : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    
    void Awake()
    {
        canvasGroup = GetComponentInParent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClickQuickLoginButton1()
    {
        User user = new User()
        {
            Name = "test1",
            Password = "163"
        };

        this.Login(user);
    }

    public void ClickQuickLoginButton2()
    {
        User user = new User()
        {
            Name = "test2",
            Password = "163"
        };

        this.Login(user);
    }

    public void ClickQuickLoginButton3()
    {
        User user = new User()
        {
            Name = "test3",
            Password = "163"
        };

        this.Login(user);
    }

    private void Login(User user)
    {
        DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(User));
        MemoryStream memoryStream = new MemoryStream();
        jsonSerializer.WriteObject(memoryStream, user);
        memoryStream.Position = 0;

        StreamReader streamReader = new StreamReader(memoryStream, Encoding.UTF8);
        String data = streamReader.ReadToEnd();
        memoryStream.Close();
        streamReader.Close();
        //Debug.Log("Send login data: " + data);

        Network.GetInstance().SendData(Constants.LoginServiceID, Constants.HandleLoginCommandID, data);

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        //Debug.Log("Quick Login.");
    }
}
