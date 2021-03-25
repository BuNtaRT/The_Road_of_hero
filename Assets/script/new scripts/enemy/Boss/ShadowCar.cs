using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCar : CoreBoss, IBossCore
{
    public BoxCollider2D BossAttackColider;
    public ParticleSystem TrailP;
    public ParticleSystem DamageP;
    SpriteRenderer shadoCarSP;

    //dev
    public bool die = false;
    public bool Line0 = false;
    public bool Line1 = false;

    //void Update() 
    //{
    //    if (die) 
    //    {
    //        die = false;
    //        StopAllCoroutines();
    //        StartCoroutine(AnimDie());
    //    }
    //    if (Line0) 
    //    {
    //        Line0 = false;
    //        StartCoroutine(AttackLine0());
    //    }
    //    if (Line1)
    //    {
    //        Line1 = false;
    //        StartCoroutine(AttackLine1());
    //    }
    //}


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

    IEnumerator AttackLine1() 
    {
        bool en = true;
        for (int i = 0; i < 5; i++)
        {
            en = !en;
            shadoCarSP.enabled = en;
            yield return new WaitForSeconds(0.2f);
        }
        shadoCarSP.enabled = true;
        TrailP.Stop();


        Vector3 StartV = shadoCarSP.transform.localPosition, EndV = new Vector3(StartV.x-22,StartV.y,StartV.z);
        float Speed = 1f;
        for (float time = 0; time < Speed; time += Time.deltaTime)
        {
            float progress = time / Speed;
            if(progress >= 0.6f)
                BossAttackColider.enabled = true;
            shadoCarSP.transform.localPosition = Vector3.Lerp(StartV, EndV, progress);
            yield return null;
        }
        BossAttackColider.enabled = false;

        yield return new WaitForSeconds(0.5f);

        shadoCarSP.transform.localPosition = StartV;
        TrailP.Play();
        yield return new WaitForSeconds(0.2f);

    }

    IEnumerator Behavor() 
    {
        while (true) 
        {
            yield return new WaitForSeconds(1);

            //делаем свитч по линиям 
            int rand = Random.Range(2, 5);
            undie(false);
            for (int i = 0; i <= rand; i++)
            {
                int randLine = 0;
                do
                {
                    randLine = Random.Range(0, 3);
                } while (randLine == curLine);

                SwitchLine(randLine);
                yield return new WaitForSeconds(1.2f);
            }

            undie(true);

            SwitchLine(car.GetLine());

            yield return new WaitForSeconds(0.5f);
            rand = Random.Range(0, 2);

            if (rand == 0)
                StartCoroutine(AttackLine0());
            if (rand == 1)
                StartCoroutine(AttackLine1());

            yield return new WaitForSeconds(4.5f);

        }
    }


    private void Start()
    {
        ObjPosition Objpos = gameObject.AddComponent<ObjPosition>();
        Objpos.SetParametr(false, -14f, 0, 0, 0.5f);

        shadoCarSP = transform.Find("Sprite").GetComponent<SpriteRenderer>();

        var main = TrailP.main;
        main.customSimulationSpace = Camera.main.transform;

        main = DamageP.main;
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
        initHP(0,6,1);


        //hp and color for HPBar
        bossShow.allhp = hp;
        bossShow.SetColor(new Color(0.3207547f, 0f, 0.02482465f), new Color(0.9150943f, 0.9150943f, 0.9150943f));

        StartCoroutine(Behavor());
    }


    public IEnumerator AnimDie()
    {
        TrailP.Stop();
        GameObject.FindGameObjectWithTag("Scripts").GetComponent<BossChoise>().BossDIE();
        CoreEffect.Create_effect("DieShadow", 6.48f, -3.838f, transform);
        //GameObject.FindGameObjectWithTag("Scripts").GetComponent<BossChoise>().BossDIE();
        bossShow.Hide();
        BossAttackColider.enabled = false;


        Color StartC = new Color(0, 0, 0, 0.5f);
        Color EndPos = new Color(0, 0, 0, 0f);
        float Speed = 0.5f;
        for (float time = 0; time < Speed; time += Time.deltaTime)
        {
            float progress = time / Speed;
            shadoCarSP.color = Color.Lerp(StartC, EndPos, progress);
            yield return null;
        }

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
        CoreAudio.Create_audio_eff("expl");
        DamageP.Play();

        // тут проверяется на хп
        if (CHminusHP())
        {
            StopAllCoroutines();
            StartCoroutine(AnimDie());
        }
    }
}
