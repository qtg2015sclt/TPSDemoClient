using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public Text gameOverInfoText;
    public CanvasGroup gameCanvasGroup;
    public GameObject playerPrefab;

    private GameObject player;

    public GameObject tank;
    public Tank tankEntity = new Tank();

    private Player myself = null;
    
    private bool gameRunning = false;
    //private bool win = false;

    private bool sendPlayerTrigger = false;
    private bool sendTankTrigger = false;
    //private List<Tank> tanks;

    private void Awake()
    {
        if (null == instance)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }

    public void LoadInitResourceInfo(Resource resource)
    {
        Money.SetMoney(resource.Money);
        Ammunition.SetAmmunition(resource.Bullet, resource.Grenade, resource.TankBullet);
        myself = new Player()
        {
            PlayerID = resource.UserID,
            Name = resource.Name,
            ActionCode = Constants.PlayerIdle,
            PostionX = 100f,
            PostionZ = 100f,
            MoveForward = true,
            MoveSpeed = 0f,
            RotationX = 0f,
            RotationY = 0f
        };
        Vector3 position = new Vector3(myself.PostionX, 0f, myself.PostionZ);
        Quaternion rotation = new Quaternion();
        this.player = Instantiate(this.playerPrefab, position, rotation);
        this.gameRunning = true;

        StartCoroutine(SendClientData());
    }

    public void GameOver(bool win)
    {
        this.gameOverInfoText.text = win ? "Vitory!" : "Defeat!";
        this.gameCanvasGroup.alpha = 1;
        this.gameCanvasGroup.interactable = true;
        this.gameRunning = false;
        PlayerOperation playerOperation = this.player.GetComponent<PlayerOperation>();
        playerOperation.enabled = false;
        //PlayerAttack playerAttack = this.player.GetComponent<PlayerAttack>();
        //playerAttack.enabled = false;

        Resource resource = new Resource()
        {
            Money = Money.GetMoney(),
            Bullet = Ammunition.GetBulletShuttleNum(),
            Grenade = Ammunition.GetGrenades(),
            TankBullet = Ammunition.GetTankBulletShuttleNum(),
            Name = this.myself.Name,
            UserID = this.myself.PlayerID
        };

        this.SendDataToServer(Constants.GameServiceID, Constants.StoreResourceCommandID, resource);
        //DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Resource));
        //MemoryStream memoryStream = new MemoryStream();
        //jsonSerializer.WriteObject(memoryStream, resource);
        //memoryStream.Position = 0;

        //StreamReader streamReader = new StreamReader(memoryStream, Encoding.UTF8);
        //String data = streamReader.ReadToEnd();
        //memoryStream.Close();
        //streamReader.Close();
        ////Debug.Log("Send Player Data: " + data);
        //Network.GetInstance().SendData(Constants.GameServiceID, Constants.ClientSyncCommandID, data);
    }

    public void UpdatePostion(float x, float z)
    {
        this.myself.PostionX = x;
        this.myself.PostionZ = z;

        this.sendPlayerTrigger = true;
    }

    public void UpdateRotation(float x, float y)
    {
        this.myself.RotationX = x;
        this.myself.RotationY = y;

        this.sendPlayerTrigger = true;
    }

    public void UpdateActionCode(Int32 actionCode)
    {
        this.myself.ActionCode = actionCode;

        this.sendPlayerTrigger = true;
    }

    public void UpdateMoveSpeed(float moveSpeed)
    {
        this.myself.MoveSpeed = moveSpeed;

        this.sendPlayerTrigger = true;
    }

    public void UpdateMoveForward(bool moveForward)
    {
        this.myself.MoveForward = moveForward;

        this.sendPlayerTrigger = true;
    }

    public void UpdateTankPosition(float x, float z)
    {
        this.tankEntity.PostionX = x;
        this.tankEntity.PostionZ = z;
        this.sendTankTrigger = true;
    }

    public void UpdateTankRotation(float x, float y)
    {
        this.tankEntity.RotationX = x;
        this.tankEntity.RotationY = y;
        this.sendTankTrigger = true;
    }

    public void UpdateTankActionCode(Int32 actionCode)
    {
        this.tankEntity.ActionCode = actionCode;
        this.sendTankTrigger = true;
    }

    public void SyncDataFromServer(SyncData syncData)
    {
        if (!this.gameRunning)
            return;
        // Update all data
        syncData.Players.RemoveAll(player => player.PlayerID == this.myself.PlayerID);
        //Debug.Log("Other players num: " + syncData.Players.Count);
        OtherPlayerManager.instance.UpdatePlayers(syncData.Players);
        //foreach (Player player in syncData.Players)
        //{
        //    if (player.PlayerID == this.myself.PlayerID)
        //    {
        //        Debug.Log("Myself: " + player);
        //        break;
        //    }
        //}

        EnemyManager.instance.UpdateEnemies(syncData.Enemies);

        if (syncData.Tank == null)
            return;

        if (syncData.Tank.PlayerID != this.myself.PlayerID)
        {
            this.tankEntity = syncData.Tank;

            TankOperation tankOp = tank.GetComponent<TankOperation>();
            tankOp.TakeAction(tankEntity.ActionCode);
            tankOp.MovementAndTurning(tankEntity.PostionX, tankEntity.PostionZ, tankEntity.RotationX, tankEntity.RotationY);
            OtherPlayerManager.instance.HidePlayerOnTank(tankEntity.PlayerID);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    IEnumerator SendClientData()
    {
        while (true)
        {
            if (sendPlayerTrigger)
            {
                this.sendPlayerTrigger = false;

                this.SendDataToServer(Constants.GameServiceID, Constants.ClientSyncCommandID, this.myself);
                //DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Player));
                //MemoryStream memoryStream = new MemoryStream();
                //jsonSerializer.WriteObject(memoryStream, myself);
                //memoryStream.Position = 0;

                //StreamReader streamReader = new StreamReader(memoryStream, Encoding.UTF8);
                //String data = streamReader.ReadToEnd();
                //memoryStream.Close();
                //streamReader.Close();
                ////Debug.Log("Send Player Data: " + data);
                //Network.GetInstance().SendData(Constants.GameServiceID, Constants.ClientSyncCommandID, data);
            }

            if (this.sendTankTrigger)
            {
                this.sendTankTrigger = false;

                //Debug.Log("tank playerid = " + tankEntity.PlayerID);
                this.SendDataToServer(Constants.GameServiceID, Constants.GetOnTankCommandID, tankEntity);
            }

            yield return new WaitForSeconds(0.05f);
        }
    }
    
    public void TryModifyEnemy(Int32 enemyID, Int32 damageValue)
    {
        ModifyEnemy modifyEnemy = new ModifyEnemy()
        {
            EnemyID = enemyID,
            DamageValue = damageValue
        };

        this.SendDataToServer(Constants.GameServiceID, Constants.TryModifyEnemyCommandID, modifyEnemy);
        //DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(ModifyEnemy));
        //MemoryStream memoryStream = new MemoryStream();
        //jsonSerializer.WriteObject(memoryStream, modifyEnemy);
        //memoryStream.Position = 0;

        //StreamReader streamReader = new StreamReader(memoryStream, Encoding.UTF8);
        //String data = streamReader.ReadToEnd();
        //memoryStream.Close();
        //streamReader.Close();
        //Network.GetInstance().SendData(Constants.GameServiceID, Constants.TryModifyEnemyCommandID, data);
    }

    public void GetOnTank(bool getOn)
    {
        if (getOn)
        {
            this.tankEntity.PlayerID = this.myself.PlayerID;
            //Debug.Log("tankEntity player id = " + this.tankEntity.PlayerID);
        }
        else
        {
            this.tankEntity.PlayerID = 0;
            //Debug.Log("tankEntity player id = " + this.tankEntity.PlayerID);
            this.sendTankTrigger = true;
        }

        //this.SendDataToServer(Constants.GameServiceID, Constants.GetOnTankCommandID, this.myself.PlayerID);
    }

    private void SendDataToServer<T>(Int32 serviceID, Int32 commandID, T data)
    {
        if (!this.gameRunning)
            return;
        DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
        MemoryStream memoryStream = new MemoryStream();
        jsonSerializer.WriteObject(memoryStream, data);
        memoryStream.Position = 0;

        StreamReader streamReader = new StreamReader(memoryStream, Encoding.UTF8);
        String dataStr = streamReader.ReadToEnd();
        memoryStream.Close();
        streamReader.Close();
        Network.GetInstance().SendData(serviceID, commandID, dataStr);
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.gameRunning)
            return;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
