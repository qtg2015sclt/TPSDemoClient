using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour
{
    private int damagePerShot = 10;
    private float timeBetweenGrenades = 0.2f;
    private float range = 100f;
    private float timer;

    private RaycastHit attackHit;
    private int shootableMask;
    private AudioSource grenadeAudioSource;
    [SerializeField]
    private AudioClip throwClip;
    // Use this for initialization
    void Start()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        grenadeAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.T))
        {
            ThrowGrenade();
            // Need Add anim and logic
            //anim.SetTrigger("Throw");
            Ammunition.SubtractGrenade();
        }
    }

    public void ThrowGrenade()
    {
        if (timer < timeBetweenGrenades)
        {
            //Debug.Log("Cannot shoot.");
            return;
        }

        //Debug.Log("Shoot.");
        timer = 0f;
        grenadeAudioSource.clip = throwClip;
        grenadeAudioSource.Play();

        Ray shootRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));

        if (Physics.Raycast(shootRay, out attackHit, range, shootableMask))
        {
            Debug.Log("AOE " + attackHit.collider);
            EnemyHealth enemyHealth = attackHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot);
            }
        }
    }
}
