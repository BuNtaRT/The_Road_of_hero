using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreBoss : MonoBehaviour
{
    protected Transform carP;               // Transform car player "Car"
    protected ControllCar car;              // from "Scripts"
    protected float[] PosYEnemy;
    protected float[] PosYBoss;
    protected string namemonster;           // name in "Resource/Boss/enemy/...'
    protected BoxCollider2D BossColider;    // Heart boss for attack
    protected BossShow bossShow;            // UI obj in "scene/BossCanvas"

    protected int tempHp;
    protected int hp;
    protected int hpSteap;

    protected int curLine = 1;

    // use for undie
    protected void undie(bool val)
    {
        BossColider.enabled = !val;
    }

    // check minus hp car and controll for tempHp
    protected bool CHminusHP()
    {

        tempHp--;

        bossShow.MInusHp();
        if (tempHp <= 0)
        {
            undie(true);
            if (hp - hpSteap < 0)
            {
                return true;
            }
            else
            {
                tempHp += hpSteap;
                hp -= hpSteap;
                return false;
            }
        }
        return false;
    }

    // SpawnMonster
    protected GameObject SpawnMonster(int lin)
    {
        int layout = 0;
        if (lin == 2)
            layout = 10;
        else if (lin == 1)
            layout = 12;
        else if (lin == 0)
            layout = 15;

        return CoreGenerate.GenerateObj("Boss/enemy/"+ namemonster, carP.position.x + 30, PosYEnemy[lin], layout);
    }


    //Switch line for boss
    protected void SwitchLine(int line)
    {
        curLine = line;
        StartCoroutine(GoLine(PosYBoss[line]));

    }
    IEnumerator GoLine(float posY)
    {
        float Speed = 0.25f;
        Vector3 StartPos, EndPos;
        StartPos = transform.localPosition;
        EndPos = new Vector3(StartPos.x, posY, StartPos.z);

        for (float time = 0; time < Speed; time += Time.deltaTime)
        {
            float progress = time / Speed;
            gameObject.transform.localPosition = Vector3.Lerp(StartPos, EndPos, progress);
            yield return null;
        }
    }
}
