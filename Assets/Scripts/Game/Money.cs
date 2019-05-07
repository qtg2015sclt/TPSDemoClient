using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Money : MonoBehaviour
{
    private static int money;
    private Text moneyText;

    private void Awake()
    {
        moneyText = GetComponent<Text>();
        money = 0;
    }

    // Use this for initialization
    void Start()
    {

    }

    public static int GetMoney()
    {
        return money;
    }

    public static bool EnoughToBuyBullets()
    {
        return money >= Constants.bulletPricePerShuttle;
    }

    public static bool EnoughToBuyGrenades()
    {
        return money >= Constants.grenadePricePerBox;
    }

    public static bool EnoughToBuyTankBullets()
    {
        return money >= Constants.tankBulletpricePerShuttle;
    }

    public static void SetMoney(int value)
    {
        money = value;
    }

    public static void AddMoney(int value)
    {
        money += value;
    }

    public static void BuyBullets()
    {
        money -= Constants.bulletPricePerShuttle;
    }

    public static void BuyGrenades()
    {
        money -= Constants.grenadePricePerBox;
    }

    public static void BuyTankBullets()
    {
        money -= Constants.tankBulletpricePerShuttle;
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "Money: " + money;
    }
}
