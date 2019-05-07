using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ammunition : MonoBehaviour
{
    private static int bulletShuttleNum;
    private static int grenades;
    private static int tankBulletShuttleNum;

    public static int curBullets = 24;

    private Text ammunitionText;

    //public GameObject weapon;

    private void Awake()
    {
        ammunitionText = GetComponent<Text>();
        bulletShuttleNum = 0;
        grenades = 0;
        tankBulletShuttleNum = 0;
    }
    // Use this for initialization
    void Start()
    {

    }

    public static int GetBulletShuttleNum()
    {
        return bulletShuttleNum;
    }

    public static int GetGrenades()
    {
        return grenades;
    }

    public static int GetTankBulletShuttleNum()
    {
        return tankBulletShuttleNum;
    }

    public static void SetAmmunition(int bullet, int grenade, int tankbullet)
    {
        bulletShuttleNum = bullet;
        grenades = grenade;
        tankBulletShuttleNum = tankbullet;
    }

    public static void AddShuttle()
    {
        bulletShuttleNum += 1;
    }

    public static bool SubtractShuttle()
    {
        if (bulletShuttleNum <= 0)
            return false;
        bulletShuttleNum -= 1;
        return true;
    }

    public static void AddGrenade()
    {
        grenades += Constants.grenadesNumPerBox;
    }

    public static void SubtractGrenade()
    {
        if (grenades <= 0)
            return;
        grenades -= 1;
    }

    public static void AddTankBulletShuttle()
    {
        tankBulletShuttleNum += 1;
    }

    public static void SubtractTankBulletShuttle()
    {
        if (tankBulletShuttleNum <= 0)
            return;
        tankBulletShuttleNum -= 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Weapon weaponScript = weapon.GetComponent<Weapon>();
        //Debug.Log("Ammunition: cur bullets: " + weaponScript.curBullets);
        ammunitionText.text = "Ammunition:\n"
                            + "Current bullets in shuttle: " + curBullets + "\n"
                            + "Press 1 buy bullets.\n"
                            + "(24 per shuttle, " + Constants.bulletPricePerShuttle + "$)\n"
                            + "Press 2 buy grenades.\n"
                            + "(3 per box, " + Constants.grenadePricePerBox + "$).\n"
                            + "Press 3 buy tank bullets.\n"
                            + "(10 per shuttle, " + Constants.tankBulletpricePerShuttle + "$)\n"
                            + "Bullets: " + Constants.bulletsNumPerShuttle + " * " + bulletShuttleNum + "\n"
                            + "Grenades: " + grenades + "\n"
                            + "Tank bullets: " + Constants.tankBulletsNumPerShuttle + " * " + tankBulletShuttleNum + "\n";
    }
}
