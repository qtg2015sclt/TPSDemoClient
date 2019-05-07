using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    private int damagePerShot = 10;
    private float timeBetweenBullets = 0.15f;
    private float range = 100f;
    private float timer;

    private RaycastHit shootHit;
    private int shootableMask;
    private AudioSource weaponAudioSource;
    [SerializeField]
    private AudioClip shootClip;

    public Animator anim;

    public int curBullets = Constants.bulletsNumPerShuttle;

    private void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        weaponAudioSource = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1"))
        {
            if (this.curBullets <= 0)
                return;
            this.curBullets -= 1;
            Debug.Log("cur bullets: " + this.curBullets);
            Ammunition.curBullets = this.curBullets;
            
            Shoot();
            GameManager.instance.UpdateActionCode(Constants.PlayerShoot);
        }
    }

    public void Shoot()
    {
        if (timer < timeBetweenBullets)
        {
            //Debug.Log("Cannot shoot.");
            return;
        }

        //Debug.Log("Shoot.");
        timer = 0f;
        weaponAudioSource.clip = shootClip;
        weaponAudioSource.Play();
        anim.SetTrigger("Shoot");

        Ray shootRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            //Debug.Log("Shoot " + shootHit.collider);
            EnemyOperation enemyOps = shootHit.collider.GetComponent<EnemyOperation>();

            GameManager.instance.TryModifyEnemy(enemyOps.enemyID, this.damagePerShot);
        }
    }
}
