using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : CoreBoss, IBossCore
{

    // for the following - special boss logics 

    public Animator beeAnimator;
    public ParticleSystem AttackParticle;
    public BoxCollider2D BossAttackColider;
    public GameObject Shadow;

    IEnumerator AnimaAttack1() 
    {
        beeAnimator.SetBool("Line0", true);
        yield return new WaitForSeconds(0.83f);
        BossAttackColider.enabled = true;
        yield return new WaitForSeconds(0.4f);
        BossAttackColider.enabled = false;
        beeAnimator.SetBool("Line0", false);
    }
    IEnumerator AnimaAttack2()
    {
        beeAnimator.SetBool("Line1", true);
        yield return new WaitForSeconds(1f);
        BossAttackColider.enabled = true;
        yield return new WaitForSeconds(0.6f);
        BossAttackColider.enabled = false;
        beeAnimator.SetBool("Line1", false);
    }
    IEnumerator AnimaAttack0() 
    {
        beeAnimator.SetBool("Attack", true);

        yield return new WaitForSeconds(1.30f);
        AttackParticle.Play();
        yield return new WaitForSeconds(0.72f);
        BossAttackColider.enabled = true;
        yield return new WaitForSeconds(1.1f);
        BossAttackColider.enabled = false;
        beeAnimator.SetBool("EndAttack", true);
        beeAnimator.SetBool("Attack", false);

        yield return new WaitForSeconds(1.5f);

        beeAnimator.SetBool("EndAttack", false);
    }
    IEnumerator Behavor() 
    {
        while (true)
        {
            Shadow.SetActive(true);

            yield return new WaitForSeconds(1f);
            
            //// тут пчела бегает по линиям 
            int rand = Random.Range(1, 4);
            undie(false);
            for (int i = 0; i <= rand; i++)
            {
                int randLine = 0;
                do
                {
                    randLine = Random.Range(0, 3);
                } while (randLine == curLine);

                SwitchLine(randLine);
                yield return new WaitForSeconds(1.5f);
            }



            undie(true);


            //// после пчела встает на линию на которой игрок и проводит атаку
            SwitchLine(car.GetLine());
            yield return new WaitForSeconds(1f);
            rand = Random.Range(0, 3);

            if (rand == 0)
                StartCoroutine(AnimaAttack0());
            if (rand == 1)
                StartCoroutine(AnimaAttack1());
            if (rand == 2)
                StartCoroutine(AnimaAttack2());
            yield return new WaitForSeconds(4f);

            if (tempHp == hpSteap)
            {
                Shadow.SetActive(false);

                ///// после атаки уходит на отдых 
                SwitchLine(0);
                beeAnimator.SetInteger("Sleep", 0);


                ///// Определяем сколько раз заспавнятся монстры и спавним их соответсвенно
                rand = Random.Range(3, 8);
                for (int i = 0; i <= rand; i++)
                {
                    int line = car.GetLine();
                    SpawnMonster(car.GetLine());
                    yield return new WaitForSeconds(2f);
                }

                yield return new WaitForSeconds(2f);
                beeAnimator.SetInteger("Sleep", 1);
                yield return new WaitForSeconds(0.5f);
                beeAnimator.SetInteger("Sleep", -1);
            }
        }
    }
    private void Start()
    {

        ObjPosition Objpos = gameObject.AddComponent<ObjPosition>();
        Objpos.SetParametr(false, -14.5f, 0,0, 0.5f);


        gameObject.GetComponent<BossDamage>().OnDamage += minusHP;


        // init for bossCore
        init(
            GameObject.FindGameObjectWithTag("CarP").transform,
            GameObject.FindGameObjectWithTag("Scripts").GetComponent<ControllCar>(),
            new float[] { -4.155f, -3.448f, -2.807f },
            new float[] { -0.69f, 0f, 0.63f },
            "BeeEnemy",
            transform.Find("Colider").GetComponent<BoxCollider2D>(),
            GameObject.Find("BossCanvas").GetComponent<BossShow>()
            );

        //init Hp
        initHP(0, 12, 4);

        //hp and color for HPBar
        bossShow.allhp = hp;
        bossShow.SetColor(new Color(0.735849f, 0.7041574f, 0.3713955f), new Color(0.990566f, 0.8839075f, 0.2196066f));


        StartCoroutine(Behavor());
    }





    // for the following - init Iboss
    public IEnumerator AnimDie() 
    {

        GameObject.FindGameObjectWithTag("Scripts").GetComponent<BossChoise>().BossDIE();
        bossShow.Hide();
        beeAnimator.SetBool("Die", true);
        BossAttackColider.enabled = false;
        Shadow.SetActive(false);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);

    }
    public void minusHP() 
    {
        // тут что то случается 
        CoreEffect.Create_effect("bossHit", -0.3500004f, -0.51f, beeAnimator.gameObject.transform);
        CoreEffect.Create_effect("bossHit", -1.51f, 0.66f, beeAnimator.gameObject.transform);
        CoreEffect.Create_effect("bossHit", -1.52f, 2.1f, beeAnimator.gameObject.transform);
        CoreEffect.Create_effect("bossHit", 0.3f, 1.03f, beeAnimator.gameObject.transform);

        // тут проверяется на хп
        if (CHminusHP()) 
        {
            StopAllCoroutines();
            StartCoroutine(AnimDie());
        }
    }
    public void init(Transform carP, ControllCar car, float[] PosYEnemy, float[] PosYBoss, string namemonster, BoxCollider2D BossColider, BossShow bossShow)
    {
        this.PosYEnemy = PosYEnemy;
        this.carP = carP;
        this.car = car;
        this.namemonster = namemonster;
        this.PosYBoss = PosYBoss;
        this.BossColider = BossColider;
        this.bossShow = bossShow;
    }

    public void initHP(int tempHp, int hp, int hpSteap)
    {
        this.tempHp = tempHp;
        this.hp = hp;
        this.hpSteap = hpSteap;
    }
}
