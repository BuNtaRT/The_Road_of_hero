using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.Purchasing;
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
        if(Advertisement.isSupported)
            Advertisement.Initialize("4051421",false);

        //PlayerPrefs.SetInt("money", 10000000); // Убрать


        if (PlayerPrefs.GetInt("PremCar") == 1)
        {
            CarDonat.SetActive(false);
        }
    }






    public void OpenPlusCoins()
    {
        int money = Vault_data.singleton.GetCarCurPrice() / 8;
        DonatCanvas.SetActive(true);
        WatchVideoB.SetActive(true);
        GetPremiumB.SetActive(false);
        if (PlayerPrefs.GetInt("lg") == 1)
            SetText("Посмотреть рекламу", "Посмотрите рекламу и получите +" + money + " монет");
        else
            SetText("View advertisement", "Watch the video and get +" + money + " coins");
    }

    public void OpenPremiumCarWin()
    {
        WatchVideoB.SetActive(false);
        GetPremiumB.SetActive(true);
        DonatCanvas.SetActive(true);
        if (PlayerPrefs.GetInt("lg") == 1)
            SetText("Получить премиум", "-Отсутствие рекламы-\n-Уникальная карта-\n- 5 уникальных автомобилей-");
        else
            SetText("Get premium", "-No ads-\n-Unique map-\n- 5 unique cars-");



    }

    void SetText(string inf,string descr) 
    {
        Info.text = inf;
        Descr.text = descr;
    }

    public void X2Coins()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo");
            Money_maneger.Xcoin = 3;
            PlayerPrefs.SetInt("Dies", 0);
            GameObject.Find("Scripts").GetComponent<UI>().StateOneEndGame();
            Destroy(ButtonXcoin);
        }
    }



    public void OnPurchaseComplite(Product product) 
    {
        if(product.definition.id == "car_premium") 
        {
            CarDonat.SetActive(false);
            PlayerPrefs.SetInt("PremCar", 1);
            Vault_data.singleton.InitPremCar(CarScroll);
            Vault_data.singleton.PremMap(MapScroll);
            CloseWin();
        }
    }

    public void OnPurchaseFailure(Product product,PurchaseFailureReason reason)
    {
        Debug.Log("FailPurchase");
        CloseWin();
    }


    public void WatchVideo() 
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo");
            Money_maneger.VideoAdvert();
            PlayerPrefs.SetInt("Dies", 0);

            CloseWin();
        }

    }

    public void PLyerDie() 
    {
        if (PlayerPrefs.GetInt("PremCar") == 0)
        {
            int dies = PlayerPrefs.GetInt("Dies");
            if (dies >= Random.Range(4, 6) && Advertisement.IsReady())
            {
                Advertisement.Show("video");
                PlayerPrefs.SetInt("Dies", 0);

            }
            else
                PlayerPrefs.SetInt("Dies", dies++);
        }
    }

    public void Reborn()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo");
            PlayerPrefs.SetInt("Dies", 0);
            GameObject.Find("Scripts").GetComponent<UI>().reburnCar();
            Destroy(ButtonReborn);
        }

    }

    public void CloseWin() 
    {
        DonatCanvas.SetActive(false);
    }





}
