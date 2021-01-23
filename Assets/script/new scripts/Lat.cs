﻿using System;
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
        InvokeTime = 11 - (GameObject.Find("Car").transform.GetChild(0).GetComponent<Car>().lvl / 7);
        Vault_data.singleton.CreateMontersList(new List<int> { Convert.ToInt32(GameObject.FindGameObjectWithTag("Background").name) });   // запрашиваем запитсь монстров ответственных за текущую карту 
        Invoke("SpawnWhat", 5);
    }

    float InvokeTime = 10f;  // max 10 min 2
    int chance = 70;
    int Line2_content = 0;     // зжанятость верхней линии 
    bool one_prop = false;           // нельзя две бомбы и два пита

    void SpawnWhat()                    // что спавним ??
    {
        if (!pause)
        {
            Line2_content = 0;          // обнуляем линии
            one_prop = false;                // обнуляем яму

            InvokeTime = InvokeTime - 0.5f >= 1 ? InvokeTime - 0.5f : 1;      // уменьшаем частоту вызова 
            chance = chance - 3 >= 10 ? chance - 3 : 10;                        // а так же шанс 

            Debug.Log("spawn time =" + InvokeTime + " chace = " + chance);

            if (UnityEngine.Random.Range(0, 100) >= chance)                     // если повезло заспавнить 
            {

                Invoke(GetSpanwMethodsingle(),0);

                if (CarShoot.singleton.ammo >= 1)
                {
                    if (UnityEngine.Random.Range(0, 200) >= chance / 4)
                    {
                        Invoke(GetSpanwMethodMulti(), 0);
                    }
                }
            }
            else if (InvokeTime >= 6)                                       // или же наша частоты вызовов еще не разогналась
            {
                Spawn_pit();

                if (UnityEngine.Random.Range(0, 100) >= 50)             // и возможно спавним монстра
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
        Invoke("SpawnWhat", InvokeTime);                //invoke repead не поддерживает динамическое значение (((
    }


    #region spawn method
    string[] ListMethodMulti = { "Spawn_monster", "Spawn_pit", "Spawn_orda", "Spawn_bomb" };        // лист методов для второй линии 

    string[] ListMethodSingle = { "Spawn_monster", "Spawn_pit", "Spawn_bomb" };         // лист методов если занята одна линия 
    

    string GetSpanwMethod(string[] tempMass) {
        string method = tempMass[UnityEngine.Random.Range(0, tempMass.Length)];

        //if () {
        //    method = GetSpanwMethod(tempMass);
        //}
        return method;
    }

    string GetSpanwMethodsingle()
    {
        return GetSpanwMethod(ListMethodSingle);
    }

    string GetSpanwMethodMulti()
    {
        return GetSpanwMethod(ListMethodMulti);
    }

    #endregion



    #region Spawn
    void Spawn_pit()
    {
        int plus = 0;
        if (one_prop)
        {
            plus = 8;
        }
        one_prop = true;
        if ((UnityEngine.Random.Range(0, 100) >= 50 && Line2_content != 2) || Line2_content == 1)       // если выпал шанс и линия 2 не занята или же если линия 1 занята (спавнить куда то же надо)
        {
            Line2_content = 2;
            generate("lat/pit1", transform.position.x + 15+plus, -3.635f,0);
        }
        else
        {
            Line2_content = 1;
            generate("lat/pit1", transform.position.x + 15+plus, -4.554f,0);
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

    void Spawn_orda() {
        generate("lat/orda", transform.position.x + 20, 0f,0);
    }

    void Spawn_bomb()
    {
        int plus = 0;
        if (one_prop) {
            plus = 8;
        }
        one_prop = true;
        if ((UnityEngine.Random.Range(0, 101) >= 50 && Line2_content != 2) || Line2_content == 1)
        {
            Line2_content = 2;
            generate("lat/Bomb", transform.position.x + 15+plus, 0f,0);
        }
        else
        {
            Line2_content = 1;
            generate("lat/Bomb", transform.position.x + 15+plus, -1f,0);

        }
    }

    #endregion






    int GetRandomMonster() 
    {
        return Vault_data.singleton.GetRandomMonster();
    }



    GameObject generate(string path, float posX, float posY, int layout)
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>(path));
        temp.transform.position = new Vector3(posX, posY, 0);
        if(layout != 0)
            temp.GetComponent<SpriteRenderer>().sortingOrder = layout;      // нужно что бы поставить например монстра вверх или вниз 
        return temp;

    }
}
