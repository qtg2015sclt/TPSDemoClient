using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : CharacterHealth
{
    [SerializeField]
    private Slider healthSlider;

    private PlayerOperation playerOps;
    private PlayerAttack playerAttack;

    protected override void Awake()
    {
        playerOps = GetComponent<PlayerOperation>();
        playerAttack = GetComponentInChildren<PlayerAttack>();
    }
    // Start is called before the first frame update
    public override void Start()
    {
        
        this.startHealth = 100;
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
        healthSlider.value = this.curHealth;
        GameManager.instance.UpdateActionCode(Constants.PlayerTakgeDamage);
    }

    protected override void Death()
    {
        base.Death();
        playerOps.enabled = false;
        playerAttack.enabled = false;
        GameManager.instance.UpdateActionCode(Constants.PlayerDeath);
    }
}
