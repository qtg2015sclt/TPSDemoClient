using UnityEngine;
using System.Collections;
using System;

public class OtherPlayerOperation : MonoBehaviour
{
    [SerializeField]
    private AudioClip damagedClip;
    [SerializeField]
    private AudioClip deathClip;
    [SerializeField]
    private AudioClip shootClip;

    [SerializeField]
    private GameObject grenade;
    [SerializeField]
    private GameObject gun;

    private Rigidbody playerRigidbody;
    private Animator anim;
    private AudioSource audioSource;

    // Use this for initialization
    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeAction(Int32 actionCode, bool moveForward, float moveSpeed)
    {
        switch (actionCode)
        {
            case Constants.PlayerShoot:
                anim.SetTrigger("Shoot");
                audioSource.clip = shootClip;
                audioSource.Play();
                break;
            case Constants.PlayerThrow:
                anim.SetTrigger("Throw");
                break;
            case Constants.PlayerReload:
                anim.SetTrigger("Reload");
                break;
            case Constants.PlayerTakgeDamage:
                anim.SetTrigger("Damaged");
                audioSource.clip = damagedClip;
                audioSource.Play();
                break;
            case Constants.PlayerDeath:
                anim.SetTrigger("Death");

                audioSource.clip = deathClip;
                audioSource.Play();
                Destroy(gameObject, 2.0f);
                break;
            case Constants.PlayerRun:
                anim.SetFloat("MoveSpeed", moveSpeed);
                anim.SetBool("MoveForward", moveForward);
                break;
            case Constants.PlayerWalk:
                anim.SetFloat("MoveSpeed", moveSpeed);
                anim.SetBool("MoveForward", moveForward);
                break;
            case Constants.PlayerIdle:
                anim.SetFloat("MoveSpeed", moveSpeed);
                anim.SetBool("MoveForward", moveForward);
                break;
            case Constants.PlayerUseGun:
                grenade.SetActive(false);
                gun.SetActive(true);
                break;
            case Constants.PlayerUserGrenade:
                grenade.SetActive(true);
                gun.SetActive(false);
                break;
        }
    }

    public void MovementAndTurning(float moveSpeed, float x, float z, float rotationX, float rotationY)
    {
        Vector3 destPos = new Vector3(x, 0f, z);
        //playerRigidbody.MovePosition(moveDirection * Time.deltaTime * moveSpeed + transform.position);
        //playerRigidbody.MovePosition(destPos);
        transform.position = destPos;

        Vector3 eulerAngles = new Vector3(rotationX, rotationY, 0);
        transform.eulerAngles = eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
