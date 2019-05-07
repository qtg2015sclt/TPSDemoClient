using UnityEngine;
using System.Collections;
using System;
using UnityEngine.AI;

public class EnemyOperation : MonoBehaviour
{
    [SerializeField]
    private AudioClip damagedClip;
    [SerializeField]
    private AudioClip deathClip;
    [SerializeField]
    private AudioClip shootClip;

    [SerializeField]
    private GameObject gun;

    private Rigidbody enemyRigidbody;
    private Animator anim;
    private AudioSource audioSource;

    public Int32 enemyID;

    NavMeshAgent nav;

    void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //Debug.Log("Get anim = " + anim);
        audioSource = GetComponent<AudioSource>();
        nav = GetComponent<NavMeshAgent>();
    }

    public void TakeAction(Int32 actionCode)
    {
        //Debug.Log(anim.GetCurrentAnimatorStateInfo(0).IsName("handgun_guard_walk"));
        AnimatorStateInfo animatorStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (animatorStateInfo.IsName("handgun_take_damage") || animatorStateInfo.IsName("handgun_combat_shoot_burst"))
        {
            Debug.Log("handgun_take_damage: " + anim.GetCurrentAnimatorStateInfo(0).IsName("handgun_take_damage"));
            return;
        }
        switch (actionCode)
        {
            case Constants.EnemyShoot:
                anim.SetTrigger("Shoot");
                audioSource.clip = shootClip;
                audioSource.Play();
                break;
            case Constants.EnemyExplosion:
                break;
            case Constants.EnemyTakeDamage:
                anim.SetTrigger("Damaged");
                //Debug.Log("Enemy " + this.enemyID + " is damaged");
                audioSource.clip = damagedClip;
                audioSource.Play();
                break;
            case Constants.EnemyDeath:
                anim.SetTrigger("Death");
                //Debug.Log("Enemy " + this.enemyID + " is dead");
                audioSource.clip = deathClip;
                audioSource.Play();
                Destroy(gameObject, 2.0f);
                break;
            case Constants.EnemyMove:
                //Debug.Log("Enemy " + this.enemyID + " is moving");
                anim.SetTrigger("Move");
                break;
            case Constants.EnemyIdle:
                anim.SetTrigger("Idle");
                break;
        }
    }

    public void MovementAndTurning(float x, float z, float rotationX, float rotationY)
    {
        Vector3 destPos = new Vector3(x, 0f, z);
        //Debug.Log("(x, y, z) = " + destPos);
        //enemyRigidbody.MovePosition(moveDirection * Time.deltaTime * Constants.EnemyMoveSpeed + transform.position);
        //enemyRigidbody.MovePosition(destPos);
        transform.position = destPos;
        //nav.SetDestination(destPos);

        Vector3 eulerAngles = new Vector3(rotationX, rotationY, 0);
        transform.eulerAngles = eulerAngles;
    }

    public void TakeDamageFinished()
    {
        //Debug.Log("take damage finished.");
        GameManager.instance.TryModifyEnemy(this.enemyID, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
