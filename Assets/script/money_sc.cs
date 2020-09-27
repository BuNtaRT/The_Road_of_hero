using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class money_sc : MonoBehaviour
{
    private int money = 0;

    public Text in_game;

    private void Start()
    {
        //money = PlayerPrefs.GetInt("money");
    }

    public void Get_coin(int coin) {
        money += coin;
        in_game.text = money.ToString();
    }


}
