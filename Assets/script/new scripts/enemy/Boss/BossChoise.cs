using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChoise : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void BossFightStart(int bossNum) 
    {
        if (bossNum == 1)
        {
            CoreGenerate.GenerateObj("Boss/Bee", 14, 0,10,Camera.main.transform);
        }
        else if (bossNum == 2) 
        {
        }
        else if (bossNum == 3)
        {
        }
    }

    public void BossDIE() 
    {
        GameObject.Find("Car").GetComponent<CombinateBG>().BossEND();
    }

}
