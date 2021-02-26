using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCar : CoreBoss, IBossCore
{
    public Animator ShadowAnimator;
    public BoxCollider2D BossAttackColider;
    public ParticleSystem TrailP;
    SpriteRenderer shadoCarSP;

    //dev
    public bool die = false;
    public bool Line0 = false;

    void Update() 
    {
        if (die) 
        {
            die = false;
            StopAllCoroutines();
            StartCoroutine(AnimDie());
        }
        if (Line0) 
        {
            Line0 = false;
            StartCoroutine(AttackLine0());
        }
    }


    IEnumerator AttackLine0() 
    {
        bool en = true;
        for (int i = 0; i < 5; i++) 
        {
            en = !en;
            shadoCarSP.enabled = en;
            yield return new WaitForSeconds(0.2f);
        }
        TrailP.Stop();

        int LineBoss = Random.Range(0, 3);
        for (int i = 0; i <= 2; i++)
        {
            if (i != LineBoss)
                SpawnMonster(i);
        }

        GameObject trueBoss = SpawnMonster(LineBoss);

        trueBoss.transform.Find("Particle System").gameObject.SetActive(false);

        trueBoss.AddComponent<FakeShadow>().BD = gameObject.GetComponent<BossDamage>();

        yield return new WaitForSeconds(5f);
        shadoCarSP.enabled = true;
        TrailP.Play();

    }

    IEnumerator Behavor() 
    {
        while (true) 
        {
            yield return new WaitForSeconds(1);

        }
        yield return new WaitForSeconds(0);
    }


    private void Start()
    {
        ObjPosition Objpos = gameObject.AddComponent<ObjPosition>();
        Objpos.SetParametr(false, 0/*-14f*/, 0, 0, 0.5f);

        shadoCarSP = transform.Find("Sprite").GetComponent<SpriteRenderer>();

        var main = TrailP.main;
        main.customSimulationSpace = Camera.main.transform;

        gameObject.GetComponent<BossDamage>().OnDamage += minusHP;

        // init for bossCore
        init(
            GameObject.FindGameObjectWithTag("CarP").transform,
            GameObject.FindGameObjectWithTag("Scripts").GetComponent<ControllCar>(),
            new float[] { -4.394f, -3.688f, -3.037f },
            new float[] { -0.74f, 0f, 0.61f },
            "ShadowCarEnemy",
            transform.Find("BossHeart").GetComponent<BoxCollider2D>(),
            GameObject.Find("BossCanvas").GetComponent<BossShow>()
            );
        //init Hp
        initHP(3,9,3);


        //hp and color for HPBar
        bossShow.allhp = hp;
        bossShow.SetColor(new Color(0.3207547f, 0f, 0.02482465f), new Color(0.9150943f, 0.9150943f, 0.9150943f));

        StartCoroutine(Behavor());
    }


    public IEnumerator AnimDie()
    {
        TrailP.Stop();
        CoreEffect.Create_effect("DieShadow", 6.48f, -3.838f, transform);
        //GameObject.FindGameObjectWithTag("Scripts").GetComponent<BossChoise>().BossDIE();
        bossShow.Hide();
        BossAttackColider.enabled = false;

        yield return new WaitForSeconds(0.6f);
        shadoCarSP.enabled = false;
        yield return new WaitForSeconds(6f);
        Destroy(gameObject);
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

    public void minusHP()
    {
        // тут что то случается 
        //CoreEffect.Create_effect("bossHit", -0.3500004f, -0.51f, beeAnimator.gameObject.transform);
        //CoreEffect.Create_effect("bossHit", -1.51f, 0.66f, beeAnimator.gameObject.transform);
        //CoreEffect.Create_effect("bossHit", -1.52f, 2.1f, beeAnimator.gameObject.transform);
        //CoreEffect.Create_effect("bossHit", 0.3f, 1.03f, beeAnimator.gameObject.transform);

        // тут проверяется на хп
        if (CHminusHP())
        {
            StopAllCoroutines();
            StartCoroutine(AnimDie());
        }
    }
}
