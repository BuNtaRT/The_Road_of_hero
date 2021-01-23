﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Money_maneger 
{

    static int money = 0;

    public static void Plus_money(int val) 
    {
        money = PlayerPrefs.GetInt("money");
        money += val;
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.Save();
    }

    public static bool Minus_Money(int val) 
    {
        if (Minus_check(val))
        {
            money = PlayerPrefs.GetInt("money");
            money -= val;
            PlayerPrefs.SetInt("money", money);
            PlayerPrefs.Save();
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
}
