using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : CharacterHealth
{
    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private int moneyValue = 10;

    protected override void Awake()
    {
        
    }

    // Start is called before the first frame update
    public override void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();
        this.startHealth = 20;
        base.Start();
        this.curHealth = this.startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        this.damaged = false;
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
    }

    protected override void Death()
    {
        base.Death();
        enemyMovement.enabled = false;
        enemyAttack.enabled = false;

        GetComponent<NavMeshAgent>().enabled = false;

        Money.AddMoney(moneyValue);
        Destroy(gameObject, 2.0f);
    }
}
