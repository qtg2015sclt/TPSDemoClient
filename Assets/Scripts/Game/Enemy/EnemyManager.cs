using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance = null;

    //private Dictionary<Int32, Enemy> enemies;
    private Dictionary<Int32, GameObject> enemies = new Dictionary<int, GameObject>();

    //private PlayerHealth playerHealth;
    public GameObject enemy01;

    public GameObject enemy02;
    //private float spawnTime = 3f;
    //public Transform[] spawnPoints;

    private void Awake()
    {
        if (null == instance)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }

    public void UpdateEnemies(List<Enemy> enemies)
    {
        foreach (Enemy e in enemies)
        {
            if (!this.enemies.ContainsKey(e.EnemyID))
            {
                this.CreateAndAddEnemy(e);
            }
            GameObject curEnemy = this.enemies[e.EnemyID];
            EnemyOperation op = curEnemy.GetComponent<EnemyOperation>();
            op.enemyID = e.EnemyID;
            op.TakeAction(e.ActionCode);
            op.MovementAndTurning(e.PostionX, e.PostionZ, e.RotationX, e.RotationY);

            //if (Constants.EnemyCreate == e.ActionCode)
            //{
            //    this.CreateAndAddEnemy(e);
            //    //if (Constants.EnemyType01 == e.EnemyType)
            //    //{
            //    //    this.CreateEnemy(e.EnemyID, position, rotation, this.enemy01);
            //    //    //GameObject newEnemy = Instantiate(this.enemy01, position, rotation);
            //    //    //this.enemies.Add(e.EnemyID, newEnemy);
            //    //}
            //    //else if (Constants.EnemyType02 == e.EnemyType)
            //    //{
            //    //    this.CreateEnemy(e.EnemyID, position, rotation, this.enemy02);
            //    //    //GameObject newPlayer = Instantiate(this.enemy02, position, rotation);
            //    //    //this.enemies.Add(e.EnemyID, newEnemy);
            //    //}
            //}
            //else
            //{
            //}
        }
        //this.enemies = enemies.ToDictionary(enemy => enemy.EnemyID, enemy => enemy);
    }

    private void CreateAndAddEnemy(Enemy enemy)
    {
        Vector3 position = new Vector3(enemy.PostionX, 0f, enemy.PostionZ);
        Quaternion rotation = new Quaternion();
        GameObject newEnemy = Instantiate(Constants.EnemyType01 == enemy.EnemyType ? this.enemy01 : this.enemy02, position, rotation);
        this.enemies.Add(enemy.EnemyID, newEnemy);
    }

    // Use this for initialization
    void Start()
    {
        //playerHealth = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerHealth>();
        //InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    //void Spawn()
    //{
    //    if (playerHealth.curHealth <= 0f)
    //    {
    //        return;
    //    }

    //    // Find a random index between zero and one less than the number of spawn points.
    //    int spawnPointIndex = Random.Range(0, spawnPoints.Length);

    //    // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
    //    Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    //}
}
