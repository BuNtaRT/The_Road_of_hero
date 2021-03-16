using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Money_maneger 
{
    
    static int money = 0;

    public static int temp_money, money_monster, money_coll;
    public static int Xcoin = 1;

    public delegate void Money(int val);
    public static event Money OnMoney;

    static void OnMoneyChange(int val) 
    {
        OnMoney?.Invoke(val);
    }
    public static void ResetTempMoney() 
    {
        temp_money = 0;
        money_monster = 0;
        money_coll = 0;
        Xcoin = 1;
    }

    public static void Plus_collect(int val) 
    {
        money_coll += val;
        Plus_money(val);
    }
    public static void Plus_monster(int val) 
    {
        money_monster += val;
    }

    public static void SaveEndGame() 
    {
        Plus_money(temp_money * Xcoin);
    }

    public static void VideoAdvert () 
    {
        Debug.Log(Vault_data.singleton.GetCarCurPrice() / 8);
        Plus_money(Vault_data.singleton.GetCarCurPrice() / 8);
    }

    static void Plus_money(int val) 
    {
        money = PlayerPrefs.GetInt("money");
        money += val;
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.Save();
        OnMoneyChange(money);
    }

    public static bool Minus_Money(int val) 
    {
        if (Minus_check(val))
        {
            money = PlayerPrefs.GetInt("money");
            money -= val;
            PlayerPrefs.SetInt("money", money);
            PlayerPrefs.Save();
            OnMoneyChange(money);
            return true;
        }
        else
        {
            return false;
        }

    }

    static bool Minus_check(int val)
    {
        money = PlayerPrefs.GetInt("money");
        if (money - val >= 0)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    public static int GetMoney()
    {
        return PlayerPrefs.GetInt("money");
    }

}
