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
            CoreGenerate.GenerateObj("Boss/ShadowCar", 14, 0, 10, Camera.main.transform);

        }
    }

    public void BossDIE() 
    {
        GameObject.Find("Car").GetComponent<CombinateBG>().BossEND();
    }

}
