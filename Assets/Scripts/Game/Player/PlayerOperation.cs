using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOperation : MonoBehaviour
{
    private bool moveForward = true;
    private float walkSpeed = 3.0f;
    private float runSpeed = 6.0f;
    private Rigidbody playerRigidbody;
    private CharacterController characterController;
    private Animator anim;

    private Vector2 rotation = Vector2.zero;
    private float mouseSensitivity = 3.0f;
    private float MinX = -20f;
    private float MaxX = 30f;

    private float tankRange = 10f;

    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private GameObject grenade;

    //private Camera mainCamera;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        Camera mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        CameraFollow cameraFollow = mainCamera.GetComponent<CameraFollow>();
        cameraFollow.BindToPlayer();
    }

    private void Update()
    {
        // Get on viechle(tank)
        Ray shootRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
        RaycastHit shootHit;
        int shootableMask = LayerMask.GetMask("ShootablePlayer");
        if (Input.GetButtonDown("Fire2"))
        {
            if (Physics.Raycast(shootRay, out shootHit, tankRange, shootableMask))
            {
                //Debug.Log("Right Click on " + shootHit.transform);
                Transform tankTransform = shootHit.transform;
                //Debug.Log("tanktransform name = " + tankTransform.gameObject.name);
                if ("Tank" == tankTransform.gameObject.name)
                {
                    tankTransform.GetChild(0).GetChild(3).GetChild(0).gameObject.SetActive(true);

                    //TankShooting tankShooting = tankTransform.GetComponent<TankShooting>();
                    TankOperation tankOperation = tankTransform.GetComponent<TankOperation>();
                    //tankShooting.enabled = true;
                    tankOperation.enabled = true;

                    tankOperation.player = gameObject;
                    gameObject.SetActive(false);
                    gameObject.transform.parent = tankTransform;

                    // Send GetOnTank msg to server
                    GameManager.instance.GetOnTank(true);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && weapon.activeSelf)
        {
            anim.SetTrigger("Reload");
            if (Ammunition.SubtractShuttle())
            {
                Weapon weaponScript = weapon.GetComponent<Weapon>();
                weaponScript.curBullets = 24;
                GameManager.instance.UpdateActionCode(Constants.PlayerReload);
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            // change weapon
            if (weapon.activeSelf)
            {
                weapon.SetActive(false);
                grenade.SetActive(true);
                GameManager.instance.UpdateActionCode(Constants.PlayerUserGrenade);
            }
            else
            {
                weapon.SetActive(true);
                grenade.SetActive(false);
                GameManager.instance.UpdateActionCode(Constants.PlayerUseGun);
            }
        }

        // Purchase
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            if (Money.EnoughToBuyBullets())
            {
                Money.BuyBullets();
                Ammunition.AddShuttle();
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            if (Money.EnoughToBuyGrenades())
            {
                Money.BuyGrenades();
                Ammunition.AddGrenade();
            }
        }
    }

    void FixedUpdate()
    {
        float speed = 0f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            speed = walkSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runSpeed;
            }
            Vector3 moveDirection = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                moveDirection += transform.forward;
                moveForward = true;
            }

            if (Input.GetKey(KeyCode.S))
            {
                moveDirection -= transform.forward;
                moveForward = false;
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
            playerRigidbody.MovePosition(nextPos);
            GameManager.instance.UpdatePostion(nextPos.x, nextPos.z);
        }

        Turning();

        Animate(speed);
        
        if (speed < 2)
        {
            GameManager.instance.UpdateActionCode(Constants.PlayerIdle);
        }
        else if (speed < 4)
        {
            GameManager.instance.UpdateActionCode(Constants.PlayerWalk);
        }
        else if (speed < 8)
        {
            GameManager.instance.UpdateActionCode(Constants.PlayerRun);
        }
        
    }

    private void Turning()
    {
        rotation.y += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        rotation.x += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        rotation.x = Mathf.Clamp(rotation.x, MinX, MaxX);

        Vector3 eulerAngles = new Vector3(-rotation.x, rotation.y, 0);
        transform.eulerAngles = eulerAngles;
        GameManager.instance.UpdateRotation(eulerAngles.x, eulerAngles.y);

    }

    private void Animate(float speed)
    {
        //Debug.Log("MoveSpeed = " + speed);
        anim.SetFloat("MoveSpeed", speed);
        GameManager.instance.UpdateMoveSpeed(speed);
        //Debug.Log("MoveForward: " + moveForward);
        anim.SetBool("MoveForward", moveForward);
        GameManager.instance.UpdateMoveForward(moveForward);
    }
}
