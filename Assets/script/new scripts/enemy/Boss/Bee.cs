using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    public Animator beeAnimator;
    public bool Atack = false, fly = false,randomFly = false,die= false;
    public bool Line0 = false, Line1 = false;
    public ParticleSystem AttackParticle;
    public BoxCollider2D BossAttackColider;
    public GameObject Shadow;

    int hp = 1;

    private void Update()
    {
        if (Atack) 
        {
            StartCoroutine(AnimaAttack0());
            Atack = false;
        }
        if (die) 
        {
            StartCoroutine(AnimDie());
            die = false;
        }
        if (Line0) 
        {
            Line0 = false;
            StartCoroutine(AnimaAttack1());
        }
        if (Line1)
        {
            Line1 = false;
            StartCoroutine(AnimaAttack2());
        }

    }





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

    IEnumerator AnimDie() 
    {
        GameObject.FindGameObjectWithTag("Scripts").GetComponent<BossChoise>().BossDIE();
        BossUIHP.Hide();
        beeAnimator.SetBool("Die", true);
        BossAttackColider.enabled = false;
        Shadow.SetActive(false);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);

    }

    ControllCar car;
    Transform carP;
    BossShow BossUIHP;

    private void Start()
    {
        carP = GameObject.FindGameObjectWithTag("CarP").transform;

        BossUIHP = GameObject.Find("BossCanvas").GetComponent<BossShow>();
        BossUIHP.allhp = hp;
        BossUIHP.SetColor(new Color(0.735849f, 0.7041574f, 0.3713955f),  new Color(0.990566f, 0.8839075f, 0.2196066f));
        temphp = 1;
        car =  GameObject.FindGameObjectWithTag("Scripts").GetComponent<ControllCar>();
        ObjPosition Objpos = gameObject.AddComponent<ObjPosition>();
        Objpos.SetParametr(false, -14.5f, 0,0, 0.5f);
        StartCoroutine(Behavor());
    }


    public BoxCollider2D BossCollider;
    void undie(bool val) 
    {
        if (val)
            BossCollider.enabled = false;
        else
            BossCollider.enabled = true;
    }

    public void minusHP() 
    {
        Debug.Log("minusHp");

        CoreEffect.Create_effect("bossHit", -0.3500004f, -0.51f, beeAnimator.gameObject.transform);
        CoreEffect.Create_effect("bossHit", -1.51f, 0.66f, beeAnimator.gameObject.transform);
        CoreEffect.Create_effect("bossHit", -1.52f, 2.1f, beeAnimator.gameObject.transform);
        CoreEffect.Create_effect("bossHit", 0.3f, 1.03f, beeAnimator.gameObject.transform);

        temphp--;
        if (temphp == 2)
            undie(true);
        BossUIHP.MInusHp();
        if (temphp == 0) 
        {
            if (hp - 4 < 0)
            {
                undie(true);
                StartCoroutine(AnimDie());

            }
            else
            {
                temphp += 4;
                hp -= 4;
                undie(true);
            }
        }
    }

    int temphp = 0;

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
                if (hp == 0)
                    StartCoroutine(AnimDie());

                int randLine = 0;
                do
                {
                    randLine = Random.Range(0, 3);
                } while (randLine == curLine);

                SwitchLine(randLine);
                yield return new WaitForSecondsRealtime(1.5f);
            }
            if (hp == 0)
                StartCoroutine(AnimDie());



            undie(true);


            //// после пчела встает на линию на которой игрок и проводит атаку
            SwitchLine(car.GetLine());
            yield return new WaitForSecondsRealtime(1f);
            rand = Random.Range(0, 3);

            Coroutine coranim;
            if (rand == 0)
                coranim = StartCoroutine(AnimaAttack0());
            if (rand == 1)
                coranim = StartCoroutine(AnimaAttack1());
            if (rand == 2)
                coranim = StartCoroutine(AnimaAttack2());
            yield return new WaitForSecondsRealtime(4f);

            if (temphp == 4)
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
                    SpawnMonster();
                    yield return new WaitForSecondsRealtime(2f);
                }

                yield return new WaitForSecondsRealtime(2f);
                beeAnimator.SetInteger("Sleep", 1);
                yield return new WaitForSecondsRealtime(0.5f);
                beeAnimator.SetInteger("Sleep", -1);
            }
        }
    }

    void SpawnMonster() 
    {
        int layout = 0;
        float y = 0;
        if (car.GetLine() == 2)
        {
            layout = 10;
            y = -2.77f;
        }
        else if (car.GetLine() == 1)
        {
            layout = 12;
            y = -3.464f;
        }
        else if (car.GetLine() == 0) 
        {
            layout = 15;
            y = -4.15f;
        }
        CoreGenerate.GenerateObj("Boss/enemy/BeeEnemy", carP.position.x + 30, y, layout);
    }

    int curLine = 1;
    void SwitchLine(int line) 
    {
        Debug.Log("SW LINE = " + line);
        curLine = line;

        if (line == 0)
            StartCoroutine(GoLine(-0.65f));
        else if (line == 1)
            StartCoroutine(GoLine(0));
        else if (line == 2)
            StartCoroutine(GoLine(0.6f));
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
