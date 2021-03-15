using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Donat : MonoBehaviour
{
    public GameObject ButtonReborn, ButtonXcoin;
    public GameObject CarDonat;
    public Transform CarScroll;
    public Transform MapScroll;

    public Text Descr, Info, price;
    public GameObject WatchVideoB, GetPremiumB, DonatCanvas;

    private void Start()
    {
        if (PlayerPrefs.GetInt("PremCar") == 2)//убрать на 1 
        {
            CarDonat.SetActive(false);
        }
    }

    public void OpenPlusCoins()
    {
        DonatCanvas.SetActive(true);
    }

    public void OpenPremiumCarWin()
    {
        int money = Vault_data.singleton.GetCarCurPrice()/8;

        DonatCanvas.SetActive(true);
        if (PlayerPrefs.GetInt("lg") == 1)
            SetText("Посмотреть рекламу", "Посмотрите рекламу и получите +" + money + " монет");
        else
            SetText("View advertisement", "Watch the video and get +" + money + " coins");

    }

    void SetText(string inf,string descr) 
    {
        Info.text = inf;
        Descr.text = descr;
    }

    public void X2Coins()
    {
        Money_maneger.Xcoin = 3;
        GameObject.Find("Scripts").GetComponent<UI>().StateOneEndGame();
        Destroy(ButtonXcoin);
    }

    public void DonatCarBuy() 
    {
        PlayerPrefs.SetInt("PremCar", 1);
        Vault_data.singleton.InitPremCar(CarScroll);
        Vault_data.singleton.PremMap(MapScroll);

    }

    public void WatchVideo() 
    {
        
    }


    public void Reborn()
    {
       GameObject.Find("Scripts").GetComponent<UI>().reburnCar();
       Destroy(ButtonReborn);

    }

    public void CloseWin() 
    {
        DonatCanvas.SetActive(false);
    }

}
