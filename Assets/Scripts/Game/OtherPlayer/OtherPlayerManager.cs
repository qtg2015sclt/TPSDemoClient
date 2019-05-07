using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class OtherPlayerManager : MonoBehaviour
{
    public static OtherPlayerManager instance = null;

    public GameObject player;
    //private Dictionary<Int32, Player> players;
    private Dictionary<Int32, GameObject> players = new Dictionary<int, GameObject>();

    //public Transform spawnPoint;

    private void Awake()
    {
        if (null == instance)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }

    public void HidePlayerOnTank(Int32 playerID)
    {
        foreach (KeyValuePair<Int32, GameObject> kvp in this.players)
        {
            if (kvp.Key == playerID)
            {
                Debug.Log("hide player " + playerID);
                kvp.Value.SetActive(false);
            }
            else
            {
                kvp.Value.SetActive(true);
            }
        }
        //GameObject playerOnTank;
        //this.players.TryGetValue(playerID, out playerOnTank);
        //playerOnTank.SetActive(false);
    }

    public void UpdatePlayers(List<Player> players)
    {
        foreach(Player p in players)
        {
            if (Constants.PlayerCreate == p.ActionCode)
            {
                
                this.CreateAndAddPlayer(p);
                //GameObject newPlayer = Instantiate(this.player, position, rotation);
                //this.players.Add(p.PlayerID, newPlayer);
            }
            else
            {
                if (!this.players.ContainsKey(p.PlayerID))
                {
                    this.CreateAndAddPlayer(p);
                }
                GameObject curPlayer = this.players[p.PlayerID];
                OtherPlayerOperation op = curPlayer.GetComponent<OtherPlayerOperation>();
                op.TakeAction(p.ActionCode, p.MoveForward, p.MoveSpeed);
                op.MovementAndTurning(p.MoveSpeed, p.PostionX, p.PostionZ, p.RotationX, p.RotationY);
            }
        }
        //this.players = players.ToDictionary(player => player.PlayerID, player => player);
        
        //foreach (KeyValuePair<Int32,Player> kvp in this.players)
        //{
        //    if (Constants.PlayerCreate == kvp.Value.ActionCode)
        //    {
        //        //GameObject newPlayer = Instantiate(player, spawnPoint.position, spawnPoint.rotation);
        //        Vector3 postion = new Vector3(kvp.Value.PostionX, 0, kvp.Value.PostionZ);
        //        Quaternion rotation = new Quaternion();
        //        GameObject newPlayer = Instantiate(this.player, postion, rotation);
        //        newPlayer.transform.parent = transform;
        //    }
        //    else
        //    {
                
        //    }
        //}
    }

    private void CreateAndAddPlayer(Player player)
    {
        Vector3 position = new Vector3(player.PostionX, 0f, player.PostionZ);
        Quaternion rotation = new Quaternion();
        GameObject newPlayer = Instantiate(this.player, position, rotation);
        this.players.Add(player.PlayerID, newPlayer);
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
