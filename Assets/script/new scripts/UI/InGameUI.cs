using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public Text PlusCoin;
    public Text CollectCoin;
    private void Start()
    {
        Money_maneger.ResetTempMoney();
    }
    //public void loseScreen2() 
    //{
    //    PlusCoin.text = Money_maneger.temp_money.ToString();
    //    if (PlayerPrefs.GetInt("lg") == 0)
    //    {
    //        CollectCoin.text = "Collected coins +" + Money_maneger.money_coll.ToString() ;

    //    }


    //    CollectCoin.text ="" + Money_maneger.money_coll.ToString();
    //}
}
