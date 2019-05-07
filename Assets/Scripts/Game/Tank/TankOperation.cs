using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankOperation : MonoBehaviour
{
    private float speed = 3.0f;
    private Rigidbody tankRigidbody;

    private Vector2 rotation = Vector2.zero;
    private float mouseSensitivity = 3.0f;
    private float MinX = -10f;
    private float MaxX = 15f;

    public GameObject player;

    private Animator anim;
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip shootClip;
    private int damagePerShot = 20;
    private float timeBetweenBullets = 0.15f;
    private float range = 100f;
    private float timer;
    private RaycastHit shootHit;
    private int shootableMask;

    //[SerializeField]
    //private Camera camera;

    void Awake()
    {
        tankRigidbody = GetComponent<Rigidbody>();
        shootableMask = LayerMask.GetMask("Shootable");
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Get off the viechle(tank)
        if (Input.GetKeyDown(KeyCode.V))
        {
            transform.GetChild(0).GetChild(3).GetChild(0).gameObject.SetActive(false);
            player.SetActive(true);
            player.transform.parent = null;

            this.enabled = false;
            TankShooting tankShooting = transform.GetComponent<TankShooting>();
            tankShooting.enabled = false;

            GameManager.instance.GetOnTank(false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Ammunition.SubtractTankBulletShuttle();
        }

        // Purchase
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            if (Money.EnoughToBuyTankBullets())
            {
                Money.BuyTankBullets();
                Ammunition.AddTankBulletShuttle();
            }
        }

        timer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public void TakeAction(Int32 actionCode)
    {
        switch (actionCode)
        {
            case Constants.TankShoot:
                //anim.SetTrigger("Shoot");
                audioSource.clip = shootClip;
                audioSource.Play();
                break;
            case Constants.TankNoAction:
                break;
        }
    }

    public void MovementAndTurning(float x, float z, float rotationX, float rotationY)
    {
        Vector3 destPos = new Vector3(x, 0f, z);
        transform.position = destPos;

        Vector3 eulerAngles = new Vector3(rotationX, rotationY, 0);
        transform.eulerAngles = eulerAngles;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Vector3 moveDirection = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                moveDirection += transform.forward;
            }

            if (Input.GetKey(KeyCode.S))
            {
                moveDirection -= transform.forward;
            }

            if (Input.GetKey(KeyCode.A))
            {
                moveDirection -= transform.right;
            }

            if (Input.GetKey(KeyCode.D))
            {
                moveDirection += transform.right;
            }

            Vector3 nextPos = moveDirection * Time.deltaTime * speed + transform.position;
            tankRigidbody.MovePosition(nextPos);
            GameManager.instance.UpdateTankPosition(nextPos.x, nextPos.z);
        }

        TurretTurning();
    }

    private void TurretTurning()
    {
        rotation.y += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        rotation.x += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        rotation.x = Mathf.Clamp(rotation.x, MinX, MaxX);

        Vector3 eulerAngles = new Vector3(-rotation.x, rotation.y, 0);
        Transform turretTransform = transform.GetChild(0).GetChild(3).transform;
        turretTransform.eulerAngles = eulerAngles;
    }

    private void Shoot()
    {
        if (timer < timeBetweenBullets)
        {
            //Debug.Log("Cannot shoot.");
            return;
        }

        //Debug.Log("Shoot.");
        timer = 0f;
        audioSource.clip = shootClip;
        audioSource.Play();

        Camera camera = transform.GetChild(0).GetChild(3).GetComponentInChildren<Camera>();

        Ray shootRay = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            //Debug.Log("Shoot " + shootHit.collider);
            EnemyOperation enemyOps = shootHit.collider.GetComponent<EnemyOperation>();

            GameManager.instance.TryModifyEnemy(enemyOps.enemyID, this.damagePerShot);
        }
    }
}
