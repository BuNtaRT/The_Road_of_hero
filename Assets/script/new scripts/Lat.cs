using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lat : MonoBehaviour
{
    #region pause
    bool pause = true;
    void PauseCar(bool pause) {
        this.pause = pause;
    }
    #endregion

    void Start()
    {
        UI.singleton.onPaused += PauseCar;
        StartupFirst.singleton.CreateMontersList(new List<int> { Convert.ToInt32(GameObject.FindGameObjectWithTag("Background").name) });   // запрашиваем запитсь монстров ответственных за текущую карту 
        InvokeRepeating("SpawnWhat", 5, InvokeTime);
    }

    float InvokeTime = 10f;  // max 10 min 2
    int chance = 70;
    int Line2_content = 0;     // зжанятость верхней линии 
    void SpawnWhat()
    {
        if (!pause)
        {
            Line2_content = 0;
            InvokeTime = InvokeTime - 0.28f >= 2 ? InvokeTime - 0.28f : 2;
            chance = chance - 2 >= 10 ? chance - 2 : 10;

            Debug.Log("spawn time =" + InvokeTime + " chace = " + chance);

            if (UnityEngine.Random.Range(0, 100) >= chance)
            {
                Spawn_monster();


                if (CarShoot.singleton.ammo >= 1)
                {
                    if (UnityEngine.Random.Range(0, 200) >= chance / 4)
                    {
                        Spawn_monster();
                    }
                }
            }
            else if (InvokeTime >= 6)
            {
                Spawn_pit();
                if (UnityEngine.Random.Range(0, 100) >= 50)
                {
                    if (CarShoot.singleton.ammo >= 1)
                    {
                        if (UnityEngine.Random.Range(0, 200) >= chance / 4)
                        {
                            Spawn_monster();
                        }
                    }
                }

                //Spawn_monster();
            }
        }
    }


    void Spawn_pit()
    {
        if ((UnityEngine.Random.Range(0, 100) >= 50 && Line2_content != 2) || Line2_content == 1)
        {
            Line2_content = 2;
            generate("lat/pit1", transform.position.x + 15, -3.635f,0);
        }
        else
        {
            Line2_content = 1;
            generate("lat/pit1", transform.position.x + 15, -4.554f,0);
        }
        

    }

    void Spawn_monster()
    {
        GameObject enemy;
        if ((UnityEngine.Random.Range(0, 100) >= 50 && Line2_content != 2) || Line2_content == 1)
        {
            enemy = generate("lat/enemy1", transform.position.x + 17, -2.044f,11);
            Line2_content = 2;
        }
        else
        {
            Line2_content = 1;
            enemy = generate("lat/enemy1", transform.position.x + 17, -3.13f,15);
        }

        enemy.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("enemy_controll/enemy_controll" + GetRandomMonster());

    }










    int GetRandomMonster() 
    {
        return StartupFirst.singleton.monsters[UnityEngine.Random.Range(0, StartupFirst.singleton.monsters.Count)];
    }



    GameObject generate(string path, float posX, float posY, int layout)
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>(path));
        temp.transform.position = new Vector3(posX, posY, 0);
        if(layout != 0)
            temp.GetComponent<SpriteRenderer>().sortingOrder = layout;
        return temp;
    }
}
