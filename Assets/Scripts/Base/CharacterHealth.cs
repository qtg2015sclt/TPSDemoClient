using UnityEngine;
using System.Collections;

public class CharacterHealth : MonoBehaviour
{
    public int startHealth;
    public int curHealth;

    [SerializeField]
    protected AudioClip damagedClip;
    [SerializeField]
    protected AudioClip deathClip;
    protected bool isDead;
    protected bool damaged;

    private Animator anim;
    private AudioSource audioSource;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    // Use this for initialization
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void TakeDamage(int amount)
    {
        damaged = true;
        this.curHealth -= amount;
        if (this.curHealth <= 0)
        {
            //Debug.Log(gameObject.name + " is dead.");
        }
        anim.SetTrigger("Damaged");
        audioSource.clip = damagedClip;
        audioSource.Play();
        if (this.curHealth <= 0 && !this.isDead)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        isDead = true;

        anim.SetTrigger("Death");

        audioSource.clip = deathClip;
        audioSource.Play();
    }
}
