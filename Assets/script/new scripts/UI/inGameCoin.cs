using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inGameCoin : MonoBehaviour
{
    Text textMoney; 
    private void Awake()
    {
        Money_maneger.OnMoney += CoinChange;
    }
    private void Start()
    {
        textMoney = gameObject.GetComponent<Text>();
        CurCoin = Money_maneger.GetMoney();
        textMoney.text = CurCoin.ToString();
        GoCoin = CurCoin;
    }
    int CurCoin;
    int GoCoin;
    void CoinChange(int val) 
    {
        GoCoin = val;
    }

    private void OnDestroy()
    {
        Money_maneger.OnMoney -= CoinChange;
    }

    private void Update()
    {
        if (CurCoin != GoCoin)
        {
            CurCoin++;
            textMoney.text = CurCoin.ToString();
        }
    }
}
