using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donat : MonoBehaviour
{
    public GameObject ButtonReborn,ButtonXcoin;
    public void OpenPlusCoins()
    {

    }

    public void OpenPremiumCarWin()
    {
    
    }

    public void X2Coins()
    {
        Money_maneger.Xcoin = 3;
        GameObject.Find("Scripts").GetComponent<UI>().StateOneEndGame();
        Destroy(ButtonXcoin);
    }

    public void Reborn()
    {
       GameObject.Find("Scripts").GetComponent<UI>().reburnCar();
       Destroy(ButtonReborn);

    }

}
