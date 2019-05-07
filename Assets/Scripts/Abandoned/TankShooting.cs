using UnityEngine;
using System.Collections;

public class TankShooting : MonoBehaviour
{
    private int damagePerShot = 20;
    private float timeBetweenBullets = 0.15f;
    private float range = 100f;
    private float timer;

    private RaycastHit shootHit;
    private int shootableMask;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip shootClip;
    
    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
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
            Debug.Log("Shoot " + shootHit.collider);
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot);
            }
        }
    }
}
